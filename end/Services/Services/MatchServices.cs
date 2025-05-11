using code_quests.Core.entities;
using code_quests.Core.Interfaces;

namespace Services.Services
{
    public class MatchServices : IMatchServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserApp _userServices;

        public MatchServices(IUnitOfWork unitOfWork, IUserApp userServices)
        {
            _unitOfWork = unitOfWork;
            _userServices = userServices;
        }
        public IReadOnlyList<MatchEntity> GetAllMatches()
        {
            IReadOnlyList<MatchEntity> matches = _unitOfWork.Repository<MatchEntity>().GetAll().ToList().AsReadOnly();
            return matches;
        }
    }
}
