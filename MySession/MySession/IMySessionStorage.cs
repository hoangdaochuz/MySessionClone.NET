namespace MySession.MySession;

public interface IMySessionStorage
{
    public ISession Get(string sessionId);
    public ISession Create();

}