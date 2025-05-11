using code_quests.Core.DTOs;
using System.Security.Claims;

namespace code_quests.Core.Interfaces
{
    public interface IPlayListServices
    {
        Task<IReadOnlyList<MatchDTO>> getPlayList(ClaimsPrincipal claimsPrincipal);
        Task<ResponseDTO> AddMatch2Playlist(ClaimsPrincipal claimsPrincipal, int matchID);

        Task<ResponseDTO> remMatchfromPlaylist(ClaimsPrincipal claimsPrincipal, int matchID);
    }

}
