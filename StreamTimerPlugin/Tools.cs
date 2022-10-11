namespace Loupedeck.StreamTimerPlugin
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;

    static class Tools
    {

        static readonly HttpClient client = new HttpClient();

        public static async Task<Boolean> PostAsync(String path)
        {
            try
            {
                HttpResponseMessage response = await client.PostAsync(Globals.BASE_URL + path, null);
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException e)
            {
                return false;
            }
        }

        public static void LoadAuthSettings()
        {
            var pluginDataDirectory = Globals.PluginDirectory;
            if (IoHelpers.EnsureDirectoryExists(pluginDataDirectory))
            {
                var filePath = Path.Combine(pluginDataDirectory, "auth.txt");

                if(File.Exists(filePath))
                {
                    //load value into globals
                    using (var streamReader = new StreamReader(filePath))
                    {
                        // Write data
                        Globals.UserUUID = streamReader.ReadLine();
                        Globals.IsAuthed = true;
                    }
                }
            }
        }

        public static void WriteToSettings(String uuidVal)
        {
            var pluginDataDirectory = Globals.PluginDirectory;
            if (IoHelpers.EnsureDirectoryExists(pluginDataDirectory))
            {
                var filePath = Path.Combine(pluginDataDirectory, "auth.txt");
                using (var streamWriter = new StreamWriter(filePath, false))
                {
                    // Write data
                    streamWriter.WriteLine(uuidVal);
                }
            }
        }
    }
}
