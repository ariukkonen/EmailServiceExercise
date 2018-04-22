using System.Threading.Tasks;
using EmailService.Contracts;
using EmailService.Service;
using Microsoft.Extensions.Logging;
using MockEmailClient;
using Moq;
using Xunit;
using System.Collections.Generic;
using System.Linq;

namespace EmailServiceTests
{
    public class EmailingServiceTests
    {
        private readonly EmailingService _sut;
        private readonly EmailingService _sut2;
        private readonly Mock<IEmailClient> _mockClient;
        private readonly IEmailClient _mockClient2;
        private readonly Mock<ILogger<EmailingService>> _mockLogger;

        public EmailingServiceTests()
        {
            _mockClient = new Mock<IEmailClient>();
            _mockClient2 = new EmailClient(100, 10);
            _mockLogger = new Mock<ILogger<EmailingService>>();
            _sut = new EmailingService(_mockClient.Object, _mockLogger.Object);
            _sut2 = new EmailingService(_mockClient2, _mockLogger.Object);

        }

        [Fact]
        public async void Should_Send_Emails_Simultaneously()
        {
            var email = new Email { To = "George", Body = "Very Important!" };
            var tasks = new[] { _sut.SendEmail(email), _sut.SendEmail(email), _sut.SendEmail(email), _sut.SendEmail(email) };

            await Task.WhenAll(tasks);

            _mockClient.Verify(a => a.SendEmail(email.To, email.Body), Times.Exactly(4));
        }
        [Fact]
        public async void Should_Send_Emails_SimultaneouslyWithConcreteClient()
        {
            var email = new Email { To = "George", Body = "Very Important!" };
            var tasks = new[] { _sut2.SendEmail(email), _sut2.SendEmail(email), _sut2.SendEmail(email), _sut2.SendEmail(email)};

            var results = await Task.WhenAll(tasks);
            Assert.Equal(4, results.Length);
            Assert.True(!results.ToList().Contains("Failure."));
        }

    }
}
