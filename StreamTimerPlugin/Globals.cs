namespace Loupedeck.StreamTimerPlugin
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    static class Globals
    {
        public static readonly String BASE_URL = "https://streamtimer.io/";

        public static String UserUUID { get; set; }

        public static Boolean IsAuthed { get; set; }
        public static String PluginDirectory { get; internal set; }
    }
}
