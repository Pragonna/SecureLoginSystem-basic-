using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace EmailSender.Service
{
    public class EmailService : IEmailSender
    {
        public async Task SendEmailAsync(string toEmail, string subject, string otp)
        {
            var smtpSettings = EmailConfig.LoadFromEnv();
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("My Company", smtpSettings.User));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = subject;
            string bodyWithLink, footer;
            CreateMessageBody(otp);

            message.Body = new TextPart("html") { Text = CreateMessageBody(otp) };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(smtpSettings.Server, smtpSettings.Port, SecureSocketOptions.SslOnConnect);
                await client.AuthenticateAsync(smtpSettings.User, smtpSettings.Pass);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

        private string CreateMessageBody(string otp)
        {
            string bodyWithLink = string.Format(@"
    <html>
        <body style='font-family: Arial, sans-serif; background-color: #f4f4f9; margin: 0; padding: 0;'>
            <table role='presentation' style='width: 100%; background-color: #ffffff; padding: 20px;'>
                <tr>
                    <td style='text-align: center; padding-bottom: 20px;'>
                        <h1 style='color: #333333; font-family: ""Segoe UI"", Tahoma, Geneva, Verdana, sans-serif;'>Welcome to My Company!</h1>
                        <p style='color: #666666; font-family: ""Segoe UI"", Tahoma, Geneva, Verdana, sans-serif;'>We're excited to have you. Please click the link below to verify your account.</p>
                    </td>
                </tr>
                <tr>
                   <td style='text-align: center; padding: 20px 0;'>
                        <p style='background-color: #4CAF50; color: white; padding: 14px 25px; border-radius: 5px; font-size: 24px; font-weight: bold; font-family: ""Segoe UI"", Tahoma, Geneva, Verdana, sans-serif; text-align: center; display: inline-block; letter-spacing: 5px;'>
                            {0}
                        </p>
                    </td>
                </tr>
                <tr>
                    <td style='text-align: center; padding-top: 20px;'>
                        <div style='font-size: 12px; color: #777777;'>
                            <p style='margin: 5px;'>If this wasn't you, please disregard this message.</p>
                            <p style='margin: 5px; color: red; font-weight: bold;'>Beware of scams!</p>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style='text-align: center; padding-top: 30px;'>
                        <div style='font-size: 10px; color: #aaaaaa; background-color: #ffffff; padding: 15px; border-top: 1px solid #ddd;'>
                            <p>My Company | All rights reserved</p>
                        </div>
                    </td>
                </tr>
            </table>
        </body>
    </html>", otp);
            string footer = @"
    <div style='text-align: center; padding-top: 20px;'>
        <img src='' alt='Warning' style='width:20px; height:20px;' />
       <p style='color: blue; font-weight: bold;'>Information</p>
        <p>We only send this message when there is an attempt to access the account with the email registered on our website.</p>
    </div>";

            return bodyWithLink + footer;
        }
    }
}
