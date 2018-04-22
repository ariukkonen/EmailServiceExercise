using System;
using System.Threading;
using System.Threading.Tasks;
using EmailService.Contracts;
using Microsoft.Extensions.Logging;
using MockEmailClient;

namespace EmailService.Service
{
    public class EmailingService : IEmailService
    {
        private readonly ILogger<EmailingService> _logger;
        private readonly IEmailClient _emailClient;
        //private static readonly SemaphoreSlim SemaphoreSlim = new SemaphoreSlim(1,1);

        public EmailingService(IEmailClient emailClient, ILogger<EmailingService> logger)
        {
            _emailClient = emailClient;
            _logger = logger;
        }

        public async Task<string> SendEmail(Email email)
        {
            _logger.LogInformation($"Sending email to {email.To}");
           //await SemaphoreSlim.WaitAsync();
            try
            {
                await _emailClient.SendEmail(email.To, email.Body);
                return "Success!";
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error sending email to {email.To}");
                return "Failure.";
            }
            finally
            {
                ReleaseClient();
                //SemaphoreSlim.Release();
            }
        }
        private void ReleaseClient()
        {
            try
            {
                _emailClient.Close();
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Unexpected Error thrown while Closing _emailClient.");
            }
        }
    }
}
