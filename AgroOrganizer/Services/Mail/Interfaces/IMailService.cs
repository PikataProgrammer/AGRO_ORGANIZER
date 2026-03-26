using AgroOrganizer.Models.Dtos.MailModel;

namespace AgroOrganizer.Services.Mail.Interfaces;

public class IMailService
{
    Task<bool> SendMail(MailModel mailModel);
    Task<bool> SendMailWithAttachment(MailModel mailModel, IFormFileCollection files);
}