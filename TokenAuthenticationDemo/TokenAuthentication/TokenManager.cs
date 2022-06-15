namespace TokenAuthenticationDemo.TokenAuthentication
{
    public class TokenManager : ITokenManager
    {
        private List<Token> _tokens;

        public TokenManager()
        {
            _tokens = new List<Token>();
            _tokens.Add(new Token
            {
                Value = "ac625d06-ec80-11ec-8ea0-0242ac120002",
                AppId = 1,
                AppName = "WeatherForcastWebApp",
                DateCreated = DateTime.Now
            });
        }

        public bool Authenticate(string userName, string password)
        {
            if (!string.IsNullOrEmpty(userName) &&
                !string.IsNullOrEmpty(password) &&
                userName.ToLower() == "admin" &&
                password == "password")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Token NewToken(int appId, string appName)
        {
            var token = new Token
            {
                Value = Guid.NewGuid().ToString(),
                AppId = appId,
                AppName = appName,
                DateCreated = DateTime.Now
            };
            _tokens.Add(token);
            return token;
        }

        public bool VerifyToken(int appId, string token)
        {
            var tokenItem = _tokens.Where(t => t.AppId == appId).FirstOrDefault();

            if (tokenItem != null)
            {
                if (tokenItem.Value == token)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
