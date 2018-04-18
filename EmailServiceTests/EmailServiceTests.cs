using System.Threading.Tasks;
using EmailService.Contracts;
using EmailService.Service;
using Microsoft.Extensions.Logging;
using MockEmailClient;
using Moq;
using Xunit;

namespace EmailServiceTests
{
    public class EmailingServiceTests
    {
        private readonly EmailingService _sut;
        private readonly Mock<IEmailClient> _mockClient;
        private readonly Mock<ILogger<EmailingService>> _mockLogger;

        public EmailingServiceTests()
        {
            _mockClient = new Mock<IEmailClient>();
            _mockLogger = new Mock<ILogger<EmailingService>>();
            _sut = new EmailingService(_mockClient.Object, _mockLogger.Object);
        }

        [Fact]
        public async void Should_Send_Emails_Simultaneously()
        {
            var email = new Email { To = "George", Body = "Very Important!" };
            var tasks = new[] { _sut.SendEmail(email), _sut.SendEmail(email), _sut.SendEmail(email), _sut.SendEmail(email) };

            await Task.WhenAll(tasks);

            _mockClient.Verify(a => a.SendEmail(email.To, email.Body), Times.Exactly(4));
        }

        //TODO: More tests!
    }
}
