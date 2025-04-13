using EventBus.Events;

namespace test.unit.tests.Events
{
    public record SenderIntegrationEvent(string message) : IntegrationEvent
    {
    }
}
