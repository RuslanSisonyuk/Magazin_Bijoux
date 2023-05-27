using Magazin_Bijoux.Models;
using System.Threading.Tasks;

public interface IMailService
{
    Task SendEmailAsync(MailRequest mailRequest);
    Task SendWelcomeEmailAsync(WelcomeRequest request);
    Task SendPurchaseEmailAsync(PurchaseRequest request);
}