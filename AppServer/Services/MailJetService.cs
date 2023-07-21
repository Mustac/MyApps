using Mailjet.Client;
using Mailjet.Client.Resources;
using Mailjet.Client.TransactionalEmails;

namespace MyAppServer.Services
{
    public class MailJetService
    {
        public async Task<bool> SendAsync(string to, string subject, string htmlMessage)
        {
            try
            {
                MailjetClient client = new MailjetClient(
                   Environment.GetEnvironmentVariable("MailJetApiKey"),
                   Environment.GetEnvironmentVariable("MailJetSecretKey"));

                MailjetRequest request = new MailjetRequest
                {
                    Resource = Send.Resource
                };

                // construct your email with builder
                var email = new TransactionalEmailBuilder()
                       .WithFrom(new SendContact("mustac.marijan@gmail.com"))
                       .WithSubject(subject)
                       .WithHtmlPart(htmlMessage)
                       .WithTo(new SendContact(to))
                       .Build();

                // invoke API to send email
                var response = await client.SendTransactionalEmailAsync(email);

                if(response.Messages.FirstOrDefault().Status == "success")
                    return true;

                return false;

            }
            catch
            {
                return false;
            }

        }
    }
}
