using Api.Commons.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Api.Services.Mail;
public class MailService : IMailService
{
    public void SendEmail(string email, string subject, string messageBody)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("Skill Mapper Team", "donotreply@contrader.it"));
        message.To.Add(new MailboxAddress("", email));
        message.Subject = subject;
        message.Body = new TextPart("plain") { Text = messageBody };

        using var client = new SmtpClient();
        client.Connect("localhost", 2525, SecureSocketOptions.None);
        client.Send(message);
        client.Disconnect(true);
    }

    public string CustomizeEmail(EmailTemplate  emailTemplate)
    {
            string path = Path.Combine(Environment.CurrentDirectory, @"EmailTemplates/", emailTemplate.TemplateName);

            string htmlTemplate = File.ReadAllText(path);

            var customEmail= htmlTemplate
            .Replace("{BaseUrl}", emailTemplate.BaseLinkUrl)
            .Replace("{Id}", emailTemplate.Id);

            //File.WriteAllText(path, customEmail);

        return customEmail;
    }
}
