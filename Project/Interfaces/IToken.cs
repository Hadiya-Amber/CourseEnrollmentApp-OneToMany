namespace Restuarant_Management.Interfaces
{
    public interface IToken
    {
        string GenerateToken(string userId, string username, string role);
    }

}
