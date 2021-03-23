namespace IServices
{
    public interface IMessageService
    {
        void Send(string sender, string recipient, string content);
    }
}
