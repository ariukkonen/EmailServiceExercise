using System.Threading.Tasks;

namespace MockEmailClient
{
    public interface IEmailClient
    {
        Task SendEmail(string to, string body);
        void Close();
    }
}