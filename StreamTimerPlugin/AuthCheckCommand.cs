namespace Loupedeck.StreamTimerPlugin
{
    using System;

    internal class AuthCheckCommand : PluginDynamicCommand
    {
        public AuthCheckCommand() : base("Check Auth", "Creates or Authorises the plugin with StreamTimer.io", "Basic")
        {
            this.MakeProfileAction("text;Timer UUID");
        }

        protected override void RunCommand(String actionParameter)
        {
            Globals.UserUUID = actionParameter;

            //Save values to local file
            Tools.WriteToSettings(Globals.UserUUID);
            this.ActionImageChanged();
        }

        protected override BitmapImage GetCommandImage(String actionParameter, PluginImageSize imageSize)
        {
            return Globals.IsAuthed == true
                ? EmbeddedResources.ReadImage("Loupedeck.StreamTimerPlugin.Resources.timer_authd.png")
                : EmbeddedResources.ReadImage("Loupedeck.StreamTimerPlugin.Resources.timer_auth.png");
        }
    }
}
