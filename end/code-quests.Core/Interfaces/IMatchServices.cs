using code_quests.Core.entities;

namespace code_quests.Core.Interfaces
{
    public interface IMatchServices
    {
        IReadOnlyList<MatchEntity> GetAllMatches();
    }

}
