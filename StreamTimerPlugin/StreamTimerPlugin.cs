namespace Loupedeck.StreamTimerPlugin
{
    using System;

    public class StreamTimerPlugin : Plugin
    {
        public override Boolean UsesApplicationApiOnly => true;

        public override Boolean HasNoApplication => true;

        public override void Load()
        {
            this.Info.DisplayName = "StreamTimer.io Controller";
            this.Info.Homepage = "https://StreamTimer.io";

            this.Info.Icon16x16 = EmbeddedResources.ReadImage("Loupedeck.StreamTimerPlugin.Resources.Icon16x16.png");
            this.Info.Icon32x32 = EmbeddedResources.ReadImage("Loupedeck.StreamTimerPlugin.Resources.Icon32x32.png");
            this.Info.Icon48x48 = EmbeddedResources.ReadImage("Loupedeck.StreamTimerPlugin.Resources.Icon48x48.png");
            this.Info.Icon256x256 = EmbeddedResources.ReadImage("Loupedeck.StreamTimerPlugin.Resources.Icon256x256.png");

            //Globals.UserUUID = "Test";
            Globals.PluginDirectory = this.GetPluginDataDirectory();
            Tools.LoadAuthSettings();
        }

        public override void Unload()
        {
        }

        private void OnApplicationStarted(Object sender, EventArgs e)
        {
        }

        private void OnApplicationStopped(Object sender, EventArgs e)
        {
        }

        public override void RunCommand(String commandName, String parameter)
        {
        }

        public override void ApplyAdjustment(String adjustmentName, String parameter, Int32 diff)
        {
        }
    }
}
