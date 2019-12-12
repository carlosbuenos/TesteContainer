namespace Infra.Pessoas.EventBus.Producer
{
    public interface IPublisherQueue
    {
        void Send();
        void SendResponse();
    }
}
