using System.IO;

namespace GameLauncher.Infrastructures
{
    public static class JsonLoader 
    {        
        public static JsonNode LoadJson(string jsonFilePath)
        {
            var jsonText = TextFileLoader.GetAllLine(jsonFilePath);

            return JsonNode.Parse(jsonText);
        }
    }
}
