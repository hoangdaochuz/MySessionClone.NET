namespace MySession.MySession;

public class FileSessionStorageEngine: ISessionStorageEngine
{
    private readonly string _storagePath;
    
    public FileSessionStorageEngine(string storagePath)
    {
        _storagePath = storagePath;
    }
    
    public async Task<Dictionary<string, byte[]>> LoadAsync(string id, CancellationToken cancellationToken = default)
    {
        string filePath = Path.Combine(_storagePath, id);
        if(!File.Exists(filePath))
        {
            return [];
        }
        FileStream fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read);
        using var streamReader = new StreamReader(fileStream);
        string json = await streamReader.ReadToEndAsync(cancellationToken);
        return System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, byte[]>>(json)??[];
    }

    public async Task CommitAsync(string id, Dictionary<string, byte[]> data, CancellationToken cancellationToken = default)
    {
        string filePath = Path.Combine(_storagePath, id);
        using FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        using var streamWriter = new StreamWriter(fileStream);
        await streamWriter.WriteAsync(System.Text.Json.JsonSerializer.Serialize(data));
    }
}