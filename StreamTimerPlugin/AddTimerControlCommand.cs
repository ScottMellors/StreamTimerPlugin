namespace Loupedeck.StreamTimerPlugin
{
    using System;
    using System.Threading.Tasks;

    internal class AddTimerControlCommand : PluginDynamicCommand
    {
        private Boolean isMissingAuth = false;

        public AddTimerControlCommand() : base("Add Minutes", "Adds a set amount of minutes to the timer", "Controls")
        {
            this.MakeProfileAction("text;Minutes to Add"); //Could there be a Number input here?
        }
        async void SendBumpValue(String uuid, Int32 bumpValue)
        {
            await Tools.PostAsync("action/" + uuid + "/bump/" + bumpValue.ToString());
        }

        protected override void RunCommand(String actionParameter)
        {
            if (Int32.TryParse(actionParameter, out var value))
            {
                if (Globals.IsAuthed == true)
                {
                    this.SendBumpValue(Globals.UserUUID, value);
                }
                else
                {
                    //set temp state alerting theres an issue
                    this.isMissingAuth = true;
                    this.ActionImageChanged();

                    Task.Delay(5000).ContinueWith(t =>
                    {
                        this.isMissingAuth = false;
                        this.ActionImageChanged();
                    });
                }
            }
            else
            {
                this.ActionImageChanged();
            }
        }

        protected override BitmapImage GetCommandImage(String actionParameter, PluginImageSize imageSize)
        {

            if (this.isMissingAuth == true)
            {
                return EmbeddedResources.ReadImage("Loupedeck.StreamTimerPlugin.Resources.timer_no_auth.png");
            }
            else
            {
                using (var bitmapBuilder = new BitmapBuilder(imageSize))
                {
                    bitmapBuilder.SetBackgroundImage(EmbeddedResources.ReadImage("Loupedeck.StreamTimerPlugin.Resources.timer_bump.png"));

                    if (Int32.TryParse(actionParameter, out var value))
                    {
                        bitmapBuilder.DrawText(actionParameter, BitmapColor.Black, 30);
                    }
                    else
                    {
                        bitmapBuilder.DrawText("ERROR!", BitmapColor.Black, 30);
                    }
                    return bitmapBuilder.ToImage();
                }

            }
        }
    }
}
