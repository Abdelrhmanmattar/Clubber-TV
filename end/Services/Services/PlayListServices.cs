using code_quests.Core.DTOs;
using code_quests.Core.entities;
using code_quests.Core.Interfaces;
using System.Net;
using System.Security.Claims;

namespace Services.Services
{
    public class PlayListServices : IPlayListServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserApp _userServices;
        private readonly ISignalRService _signalRService;

        public PlayListServices(IUnitOfWork unitOfWork, IUserApp userServices, ISignalRService signalRService)
        {
            _unitOfWork = unitOfWork;
            _userServices = userServices;
            _signalRService = signalRService;
        }

        public async Task<IReadOnlyList<MatchDTO>> getPlayList(ClaimsPrincipal claimsPrincipal)
        {
            var user = await _userServices.GetCurrentUser(claimsPrincipal);
            if (user == null)
            {
                return null;
            }
            var values = _unitOfWork.Repository<Playlist>()
                .FindAllbySpec(p => p.userID == user.Id, p => p.match)
                .Select(p => new MatchDTO
                {
                    ID = p.matchID,
                    title = p.match?.title,
                    competition = p.match?.competition,
                    date = p.match.date,
                    link = p.match.link,
                    status = p.match.status
                }).ToList().AsReadOnly();
            return values;
        }

        public async Task<ResponseDTO> AddMatch2Playlist(ClaimsPrincipal claimsPrincipal, int matchID)
        {
            var matchentity = _unitOfWork.Repository<MatchEntity>().GetByID(matchID);

            var user = await _userServices.GetCurrentUser(claimsPrincipal);

            if (matchentity == null || user == null)
            {
                return new ResponseDTO(HttpStatusCode.NotFound, "User or Match not found");
            }

            var plValue = new Playlist() { userID = user.Id, matchID = matchentity.ID };
            var res = _unitOfWork.Repository<Playlist>().AddEntity(plValue);
            try
            {
                _unitOfWork.SaveChanges();
                await _signalRService.SendMessageUser(user.Id, $"{matchentity.ID} is added to playlist");
            }
            catch (Exception ex)
            {
                return new ResponseDTO(HttpStatusCode.BadRequest, $"Error : {ex.Message}");
            }


            return (res == true) ?
                new ResponseDTO(HttpStatusCode.OK, $"Match {matchentity.title} is Added")
                :
                new ResponseDTO(HttpStatusCode.NoContent, "Error happen");
        }

        public async Task<ResponseDTO> remMatchfromPlaylist(ClaimsPrincipal claimsPrincipal, int matchID)
        {
            var matchentity = _unitOfWork.Repository<MatchEntity>().GetByID(matchID);

            var user = await _userServices.GetCurrentUser(claimsPrincipal);

            var pylist = _unitOfWork.Repository<Playlist>()
                .GetbySpec(p => (p.matchID == matchentity.ID && p.userID == user.Id), null);

            if (matchentity == null || user == null || pylist == null)
            {
                return new ResponseDTO(HttpStatusCode.NotFound, "User or Match not found");
            }

            var res = _unitOfWork.Repository<Playlist>().DeleteEntity(pylist);
            _unitOfWork.SaveChanges();
            await _signalRService.SendMessageUser(user.Id, $"{matchentity.ID} is removed from playlist");
            return (res == true) ?
                new ResponseDTO(HttpStatusCode.OK, $"Match {matchentity.title} is Deleted")
                :
                new ResponseDTO(HttpStatusCode.NoContent, "Error happen");
        }

    }
}
