using code_quests.Core.DTOs;
using code_quests.Core.entities;
using System.Security.Claims;

namespace code_quests.Core.Interfaces
{
    public interface IUserApp
    {
        Task<ResponseDTO> Regisiter_User(Regisiter_DTO _DTO);
        Task<ResponseDTO> LogIn_User(LogIn_DTO _DTO);
        Task<UserApp> GetCurrentUser(ClaimsPrincipal claims);
    }

}
