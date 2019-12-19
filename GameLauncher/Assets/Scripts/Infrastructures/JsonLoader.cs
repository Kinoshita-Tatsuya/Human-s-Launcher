using System.IO;

namespace GameLauncher.Infrastructures
{
    public static class JsonLoader 
    {        
        public static JsonNode LoadJson(string jsonFilePath)
        {
            var jsonText = TextFileLoader.Get(jsonFilePath);

            return JsonNode.Parse(jsonText);
        }
    }
}
