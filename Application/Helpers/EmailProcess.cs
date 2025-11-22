using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public class EmailProcess
    {
         IConfiguration _configuration;
         SmtpClient _smtpClient;
        //public  int Port { get; } = 587;
        //public  string Password { get; } = "iche nrep avnp ldnf";
        //public  string Host { get; } = "smtp.gmail.com";
        //public  string User { get; } = "dersimizyazilimmail@gmail.com";
        //public  bool EnableSSL { get; } = true;

        public EmailProcess(IConfiguration configuration)
        {
            _configuration = configuration;

            //SMTP: Simple Mail Transfer Protocol

            //HTTP: Hyper Text Transfer Protocol
            _smtpClient = new SmtpClient
            {
                Port = int.Parse(configuration["Email:Port"]),
                Host = configuration["Email:Host"],
                Credentials = new NetworkCredential(configuration["Email:User"], configuration["Email:Password"]),
                EnableSsl = bool.Parse(configuration["Email:EnableSSL"])
            };
        }
        public async Task SendEmail(string subject,string message, bool isHtml = true, params string[] emailAddresses)
        {
            try
            {
                for (int i = 0; i < emailAddresses.Length; i++)
                {
                    var mailMessage = new MailMessage(_configuration["Email:User"], emailAddresses[i], subject, message);
                    mailMessage.IsBodyHtml= isHtml;
                    await _smtpClient.SendMailAsync(mailMessage);
                }
            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }
}
