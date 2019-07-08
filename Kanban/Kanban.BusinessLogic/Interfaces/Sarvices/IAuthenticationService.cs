using System.Threading.Tasks;

namespace Kanban.BusinessLogic.Interfaces.Sarvices
{
    public interface IAuthenticationService
    {
        string GenerateToken(string userLogin, string password);
    }
}
