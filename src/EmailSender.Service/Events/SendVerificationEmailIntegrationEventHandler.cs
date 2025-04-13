using EventBus.Events;

namespace EmailSender.Service.Events;

public class SendVerificationEmailIntegrationEventHandler(IEmailSender emailSender) : IIntegrationEventHandler<SendVerificationEmailIntegrationEvent>
{
    public async Task Handle(SendVerificationEmailIntegrationEvent @event)
    {
        //Send OTP code to email
        await emailSender.SendEmailAsync(@event.toEmail, "OTP code", @event.otp.OTPCode);
        Console.WriteLine("Message has been sent successfully..");
    }
}
