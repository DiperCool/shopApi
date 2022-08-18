namespace CleanArchitecture.Application.Common.Interfaces;
public interface IEmailSender
{
    Task Send(string? content, string? subject,string? to);
    string? GetMail();
}
