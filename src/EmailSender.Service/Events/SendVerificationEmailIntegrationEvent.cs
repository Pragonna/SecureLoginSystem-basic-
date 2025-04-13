using Core.Domain.Entities;
using EventBus.Events;

namespace EmailSender.Service.Events
{
    public record SendVerificationEmailIntegrationEvent(string toEmail, SecurityDetails otp) : IntegrationEvent
    {
    }
}
