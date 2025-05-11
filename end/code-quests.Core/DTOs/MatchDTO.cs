using code_quests.Core.Enum;

namespace code_quests.Core.DTOs
{
    public class MatchDTO
    {
        public int ID { get; set; }
        public string title { get; set; }
        public string competition { get; set; }
        public DateTime date { get; set; }
        public Status status { get; set; }
        public string link { get; set; }
    }
}
