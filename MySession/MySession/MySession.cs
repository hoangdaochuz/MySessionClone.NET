using System.Diagnostics.CodeAnalysis;

namespace MySession.MySession;

public class MySession:ISession
{
    /// <summary>
    /// {
    ///     [key]:[value]
    /// }
    /// </summary>
    /// <param name="id"></param>
    /// <param name="engine"></param>
    private Dictionary<string,byte[]> _store = new Dictionary<string, byte[]>();

    public bool IsAvailable
    {
        get
        {
            LoadAsync(CancellationToken.None).Wait();
            return true;
        }
    }
    public string Id { get; set; }
    public IEnumerable<string> Keys => _store.Keys;
    private readonly ISessionStorageEngine _engine;
    
    public MySession(string id, ISessionStorageEngine engine)
    {
        Id = id;
        _engine = engine;
    }
    
    public async Task LoadAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        _store.Clear();
        Dictionary<string,byte[]> loadedData =   await _engine.LoadAsync(Id, cancellationToken);
        foreach (var entry in loadedData)
        {
            _store[entry.Key] = entry.Value;
        }
    }

    public async Task CommitAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        await _engine.CommitAsync(Id,_store, cancellationToken);
    }

    public bool TryGetValue(string key, [NotNullWhen(true)] out byte[]? value)
    {
        return _store.TryGetValue(key, out value);
    }

    public void Set(string key, byte[] value)
    {
        _store[key] = value;
    }

    public void Remove(string key)
    {
        _store.Remove(key);
    }

    public void Clear()
    {
        _store.Clear();
    }

    
}