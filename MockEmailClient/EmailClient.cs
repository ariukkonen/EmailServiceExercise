using System;
using System.Threading.Tasks;

namespace MockEmailClient
{
    // DO NOT MODIFY
    public class EmailClient : IEmailClient
    {        
        /* maxConnections and Connections are meant to simulate
         a limited resource */

        private readonly int _maximumNumberOfConnections;
        private int _numberOfConnections;

        /* Chance of failure and random are meant to reproduce
         an unexpected error in the Close() method

        In reality, some close methods like FileStream.Close 
        can throw due to not enough space on the hard disk */

        private readonly int _chanceOfFailure;
        private readonly Random _random;

        public EmailClient(int maximumNumberOfConnections, int chanceOfFailure)
        {
            _maximumNumberOfConnections = maximumNumberOfConnections;
            _chanceOfFailure = chanceOfFailure;
            _numberOfConnections = 0;
            _random = new Random();
        }

        public async Task SendEmail(string to, string body)
        {
            await SimulateSendingEmail();
        }

        private async Task SimulateSendingEmail()
        {
            _numberOfConnections++;

            if (_numberOfConnections >= _maximumNumberOfConnections)
            {
                throw new Exception("Connected Failed");
            }

            await Task.Delay(5000);
        }

        public void Close()
        {
            _numberOfConnections--;
            if (_random.Next(_chanceOfFailure) == _chanceOfFailure - 1)
            {
                throw new Exception("Unexpected Error");
            }
        }
    }
}
