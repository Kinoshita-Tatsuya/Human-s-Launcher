﻿namespace GameLauncher.Infrastructures
{
    public static class GameDataDAO
    {
        public static JsonNode Get()
        {
            return JsonLoader.LoadJson(JSON_FILE_PATH)["FilePaths"];               
        }

        private const string JSON_FILE_PATH = "Assets/Resources/FilePaths.json";
    }
}