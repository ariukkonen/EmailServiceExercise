﻿using System.Threading.Tasks;
using EmailService.Contracts;
using EmailService.Service;
using Microsoft.AspNetCore.Mvc;

namespace EmailService.Controllers
{
    [Route("api/[controller]")]
    public class EmailController : Controller
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        // POST api/email
        [HttpPost]
        public async Task<string> Post([FromBody] Email email)
        {
            await _emailService.SendEmail(email);
            var response = $"Sent an email to {email.To}";
            return response;
        }
    }
}
