using System.Text;
using AgroOrganizer.Models.Entities.Mail;

namespace AgroOrganizer.Services.Mail;

public static class MailTemplate
{
    public static MailTemplateEntity UserCreatedTemplate(string username, string password, string url)
    {
        var sb = new StringBuilder();
        sb.AppendLine("<div>Здравейте,</div>");
        sb.AppendLine($"<p></p>");
        sb.AppendLine("<div>Имате създаден потребителски акаунт за достъп до Agro Organizer на Дерменджиеви Агро.</div>");
        sb.AppendLine($"<p></p>");
        sb.AppendLine($"<div>Достъп до Agro Organizer: <a href=\"{url}\">линк</a>.</div>");
        sb.AppendLine($"<div>Потребителско име: <strong>{username}</strong></div>");
        sb.AppendLine($"<div>Парола: <strong>{password}</strong></div>");
        sb.AppendLine($"<p></p>");
        sb.AppendLine($"<div>С уважение,</div>");
        sb.AppendLine($"<div>Дерменджиеви Агро</div>");
        
        return new MailTemplateEntity("AO//Създаден потребителски акаунт в Agro Organizer", sb.ToString());
    }

    public static MailTemplateEntity ResetPasswordTemplate(string firstname, string lastname, string password, string url)
    {
        var sb = new StringBuilder();
        sb.AppendLine("<div>Здравейте,</div>");
        sb.AppendLine($"<p></p>");
        sb.AppendLine($"<div>Вашата парола за достъп до потребителски акаунт до Agro Organizer на Дерменджиеви Агро е сменена.</div>");
        sb.AppendLine($"<p></p>");
        sb.AppendLine($"<div>Достъп до Agro Organizer: <a href=\"{url}\">линк</a>.</div>");
        sb.AppendLine($"<div>Потребителско име: <strong>{firstname + " " + lastname}</strong></div>");
        sb.AppendLine($"<div>Парола: <strong>{password}</strong></div>");
        sb.AppendLine($"<p></p>");
        sb.AppendLine($"<div>С уважение,</div>");
        sb.AppendLine($"<div>Дерменджиеви Агро</div>");

        return new MailTemplateEntity("AO//Генерирана нова парола за достъп до Agro Organizer на Дерменджиеви Агро", sb.ToString());
    }
}