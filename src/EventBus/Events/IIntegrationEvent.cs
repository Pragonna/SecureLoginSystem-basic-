using Newtonsoft.Json;

namespace EventBus.Events;

public interface IIntegrationEvent
{
}

public record IntegrationEvent : IIntegrationEvent
{
    [JsonProperty]
    public Guid Id{ get; private set; }
    [JsonProperty]
    public DateTime CreatedDate{ get; private set; }
    public IntegrationEvent()
    {
        Id = Guid.NewGuid();
        CreatedDate = DateTime.UtcNow;
    }
    [Newtonsoft.Json.JsonConstructor]
    public IntegrationEvent(Guid id,DateTime dateTime)
    {
        Id = id;
        CreatedDate = dateTime;
    }
}
