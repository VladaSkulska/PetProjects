using System.Text.Json;

namespace HDrezka.Utilities
{
    public class JsonFileManager
    {
        private const string JSON_FILE_NAME = "data.json";

        public async Task SaveToJsonFileAsync<T>(T data)
        {
            FileMode fileMode = File.Exists(JSON_FILE_NAME) ? FileMode.Append : FileMode.Create;
            using (FileStream fs = new FileStream(JSON_FILE_NAME, fileMode))
            {
                await JsonSerializer.SerializeAsync<T>(fs, data);
            }
        }

        public async Task<T> LoadFromJsonFileAsync<T>()
        {
            using (FileStream fs = File.OpenRead(JSON_FILE_NAME))
            {
                return await JsonSerializer.DeserializeAsync<T>(fs);
            }
        }
    }
}