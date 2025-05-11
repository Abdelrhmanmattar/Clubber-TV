namespace code_quests.Core.Interfaces
{
    public interface ISignalRService
    {
        Task SendMessageAll(string message);
        Task SendMessageUser(string UserID, string message);
    }
}
