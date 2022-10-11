namespace Loupedeck.StreamTimerPlugin
{
    using System;
    using System.Threading.Tasks;

    internal class StopTimerControlCommand : PluginDynamicCommand
    {
        private Boolean isMissingAuth = false;

        public StopTimerControlCommand() : base("Stop Timer", "Stops the Timer", "Controls")
        {

        }

        async void SendToggleStop(String uuid) => await Tools.PostAsync("action/" + uuid + "/stop");

        protected override void RunCommand(String actionParameter)
        {

            if (Globals.IsAuthed == true)
            {
                this.SendToggleStop(Globals.UserUUID);
            }
            else
            {
                //set temp state alerting theres an issue
                this.isMissingAuth = true;
                this.ActionImageChanged();

                Task.Delay(5000).ContinueWith(t => {
                    this.isMissingAuth = false;
                    this.ActionImageChanged();
                });
            }
        }

        protected override BitmapImage GetCommandImage(String actionParameter, PluginImageSize imageSize)
        {
            return this.isMissingAuth == true
                ? EmbeddedResources.ReadImage("Loupedeck.StreamTimerPlugin.Resources.timer_no_auth.png")
                : EmbeddedResources.ReadImage("Loupedeck.StreamTimerPlugin.Resources.timer_stop.png");
        }
    }
}
