

using Api.Commons.Models;

namespace Api.Services.Mail;

public interface IMailService
{
    public void SendEmail(string email, string subject, string messageBody);
    public string CustomizeEmail(EmailTemplate emailTemplate);
}
