using System.Net;
using System.Net.Mail;
using AgroOrganizer.Models.Dtos.MailModel;
using AgroOrganizer.Services.Mail.Interfaces;
using Serilog;

namespace AgroOrganizer.Services.Mail;

public class MailService : IMailService
{
    private readonly IConfiguration _configuration;

    public MailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> SendMail(MailModel mailModel)
    {
        try
        {
            var message = BuildMessage(mailModel);

            return await Send(message, mailModel.MailTo);
        }
        catch (Exception e)
        {
            Log.Error(e, "Error sending mail");
            return false;
        }
    }

    public async Task<bool> SendMailWithAttachment(MailModel mailModel, IFormFileCollection files)
    {
        try
        {
            var message = BuildMessage(mailModel);

            foreach (var file in files)
            {
                using var ms = new MemoryStream();
                await file.CopyToAsync(ms);

                var attachment = new Attachment(
                    new MemoryStream(ms.ToArray()),
                    file.FileName
                );

                message.Attachments.Add(attachment);
            }

            return await Send(message, mailModel.MailTo);
        }
        catch (Exception e)
        {
            Log.Error(e, "Error sending mail with attachment");
            return false;
        }
    }

    private MailMessage BuildMessage(MailModel mailModel)
    {
        return new MailMessage
        {
            Subject = mailModel.Subject,
            Body = mailModel.Body,
            IsBodyHtml = true
        };
    }

    private async Task<bool> Send(MailMessage message, string toEmail)
    {
        var fromAddr = _configuration["WebMail:FromAddress"];
        var fromName = _configuration["WebMail:FromDisplayName"];
        var password = _configuration["WebMail:FromPassword"];

        if (string.IsNullOrWhiteSpace(fromAddr) ||
            string.IsNullOrWhiteSpace(fromName) ||
            string.IsNullOrWhiteSpace(password))
        {
            Log.Error("Mail configuration is missing");
            return false;
        }

        var from = new MailAddress(fromAddr, fromName);
        var to = new MailAddress(toEmail);

        using var smtp = new SmtpClient
        {
            Host = _configuration["WebMail:Host"] ?? "smtp.gmail.com",
            Port = int.Parse(_configuration["WebMail:Port"] ?? "587"),
            EnableSsl = true,
            Credentials = new NetworkCredential(from.Address, password)
        };

        message.From = from;
        message.To.Add(to);

        await smtp.SendMailAsync(message);

        return true;
    }
}