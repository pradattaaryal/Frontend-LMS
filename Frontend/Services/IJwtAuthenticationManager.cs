namespace Frontend.Services
{
    public interface IJwtAuthenticationManager 
    { 
        string Authenticate(string username, string password);
    }
}
