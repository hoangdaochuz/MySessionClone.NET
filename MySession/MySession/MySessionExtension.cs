namespace MySession.MySession;

public static class MySessionExtension
{
    public const string MySessionCookiesName = "MY_SESSION_ID";
    public static ISession GetSession(this HttpContext context)
    {
        var sessionContainer = context.RequestServices.GetRequiredService<MySessionScopedContainer>();
       
        if (sessionContainer.Session != null)
        {
            return sessionContainer.Session;
        }
        else
        {
            string? sessionId = context.Request.Cookies[MySessionCookiesName];
            if (IsValidSessionId(sessionId))
            {
                var session = context.RequestServices.GetRequiredService<IMySessionStorage>().Get(sessionId);
                context.Response.Cookies.Append(MySessionCookiesName, session.Id);
                return session;
            }
            else
            {
                var session = context.RequestServices.GetRequiredService<IMySessionStorage>().Create();
                context.Response.Cookies.Append(MySessionCookiesName, session.Id);
                return session;
            }
        }
    }

    public static bool IsValidSessionId(string sessionId)
    {
        return !string.IsNullOrEmpty(sessionId) && Guid.TryParse(sessionId, out var _);
    }
}