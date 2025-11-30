namespace TaskManagerApi.Services
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _http;

        public UserContext(IHttpContextAccessor http)
        {
            _http = http;
        }

        public string UserId =>
            _http.HttpContext?.User?.FindFirst("uid")?.Value
            ?? throw new UnauthorizedAccessException("User ID not found");
    }
}
