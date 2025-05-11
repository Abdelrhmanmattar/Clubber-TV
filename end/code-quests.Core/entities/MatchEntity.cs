using code_quests.Core.Enum;

namespace code_quests.Core.entities
{
    public class MatchEntity
    {
        public int ID { get; set; }
        public string title { get; set; }
        public string competition { get; set; }
        public DateTime date { get; set; }
        public Status status { get; set; } = Status.Replay;
        public string link { get; set; }
        public List<Playlist> Playlists { get; set; }
    }
}
