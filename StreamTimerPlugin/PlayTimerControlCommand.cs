namespace Loupedeck.StreamTimerPlugin
{
    using System;
    using System.Threading.Tasks;

    internal class PlayTimerControlCommand : PluginDynamicCommand
    {
        private Boolean isCurrentlyPlaying = false;
        private Boolean isMissingAuth = false;

        public PlayTimerControlCommand() : base("Play/Pause Timer", "Toggles the timers play state", "Controls")
        {

        }

        async void SendTogglePlay(String uuid)
        {
                var success = await Tools.PostAsync("action/" + uuid + "/play");

                if (success == true)
                {
                    //flip current state
                    this.isCurrentlyPlaying = !this.isCurrentlyPlaying;
                    this.ActionImageChanged();
                }
        }

        protected override void RunCommand(String actionParameter) { 
            
            if(Globals.IsAuthed == true)
            {
                this.SendTogglePlay(Globals.UserUUID);
            } else
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
                : this.isCurrentlyPlaying == true
                    ? EmbeddedResources.ReadImage("Loupedeck.StreamTimerPlugin.Resources.timer_pause.png")
                    : EmbeddedResources.ReadImage("Loupedeck.StreamTimerPlugin.Resources.timer_play.png");
        }

    }
}
