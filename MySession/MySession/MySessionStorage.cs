namespace MySession.MySession;

public class MySessionStorage: IMySessionStorage
{
    private readonly ISessionStorageEngine _engine;
    /// <summary>
    /// {
    /// [id]:[session]
    /// }
    /// </summary>
    /// <param name="engine"></param>
    
    private readonly Dictionary<string, ISession> _sessions = new Dictionary<string, ISession>();
    
    public MySessionStorage(ISessionStorageEngine engine)
    {
        _engine = engine;
    }
    
    public ISession Get(string sessionId)
    {
        ISession? existSession = _sessions.ContainsKey(sessionId) ? _sessions[sessionId] : null;
        if (existSession == null) return Create();
        return existSession;
    }

    public ISession Create()
    {
        ISession newSession = new MySession(Guid.NewGuid().ToString("N"),_engine);
        _sessions[newSession.Id] = newSession;
        return newSession;
    }
}