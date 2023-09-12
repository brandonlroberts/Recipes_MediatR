namespace Recipes_MediatR.Config
{
    public interface IEventAggregatorService
    {
        void Subscribe(string eventName, Action<object> eventHandler);
        void Unsubscribe(string eventName, Action<object> eventHandler);
        Task PublishAsync(string eventName);
        Task PublishAsync(string eventName, object eventData);
    }
}