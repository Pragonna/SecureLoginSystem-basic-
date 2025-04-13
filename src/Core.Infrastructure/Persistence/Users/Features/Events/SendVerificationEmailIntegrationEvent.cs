using Core.Domain.Entities;
using EventBus.Events;

namespace Core.Infrastructure.Persistence.Users.Features.Events
{
    public record SendVerificationEmailIntegrationEvent(string toEmail, SecurityDetails otp) : IntegrationEvent
    {
    }
}
