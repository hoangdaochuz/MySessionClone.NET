namespace MySession.MySession;

public interface ISessionStorageEngine
{
    public Task<Dictionary<string,byte[]>> LoadAsync(string id, CancellationToken cancellationToken = default);
    public Task CommitAsync(string id, Dictionary<string,byte[]> data, CancellationToken cancellationToken = default);
}