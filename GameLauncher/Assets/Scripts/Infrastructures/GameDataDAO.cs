namespace GameLauncher.Infrastructures
{
    public static class GameDataDAO
    {
        public static JsonNode Get()
        {
            return JsonLoader.LoadJson(JSON_FILE_PATH)["FilePaths"]; 
        }

        public static string Get(string path)
        {
            return TextFileLoader.Get(path);
        }

        private const string JSON_FILE_PATH = "ExternalFiles/FilePaths.json";
    }
}
