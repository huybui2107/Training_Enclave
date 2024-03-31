namespace BE.Service
{
    public interface ITokenService
    {
        Task<string> GenerateToken(string username);
    }
}
