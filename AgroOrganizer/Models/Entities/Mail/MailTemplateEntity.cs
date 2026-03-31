namespace AgroOrganizer.Models.Entities.Mail;

public class MailTemplateEntity
{
    public string Subject { get; private set; }
    public string Body { get; private set; }

    public MailTemplateEntity(string subject, string body)
    {
        Subject = subject;
        Body = body;
    }
}

    
