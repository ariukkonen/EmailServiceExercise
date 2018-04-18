using System.Threading.Tasks;
using EmailService.Contracts;

namespace EmailService.Service
{
    public interface IEmailService
    {
        Task<string> SendEmail(Email email);
    }
}