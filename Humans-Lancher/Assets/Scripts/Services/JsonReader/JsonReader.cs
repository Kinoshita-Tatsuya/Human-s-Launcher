using System.IO;

namespace HumansLancher.Services.Json
{
    public static class JsonReader 
    {        
        public static JsonNode ReadJson(string jsonFilePath)
        {
            string jsonText = File.ReadAllText(jsonFilePath);

            JsonNode json = JsonNode.Parse(jsonText);

            return json;
        }
    }
}
