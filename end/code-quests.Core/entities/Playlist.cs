namespace code_quests.Core.entities
{
    public class Playlist
    {
        public string userID { get; set; }
        public int matchID { get; set; }

        public MatchEntity match { get; set; }
        public UserApp userApp { get; set; }
    }
}
