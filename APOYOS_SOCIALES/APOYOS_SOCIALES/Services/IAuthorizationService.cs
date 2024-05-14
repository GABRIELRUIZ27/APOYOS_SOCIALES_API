using System.Threading.Tasks;
using APOYOS_SOCIALES.DTOs;

namespace APOYOS_SOCIALES.Services 
{
    public interface IAuthorizationService
    {
        Task<AppUserAuthDTO> ValidateUser(AppUserDTO dto);
        Task Logout(int userId);
    }
}