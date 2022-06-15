namespace TokenAuthenticationDemo.TokenAuthentication
{
    public interface ITokenManager
    {
        bool Authenticate(string userName, string password);
        Token NewToken(int appId, string appName);
        bool VerifyToken(int appId, string token);
    }
}