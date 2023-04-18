using GMPP.MainApi.Models.Dtos;
using GMPP.MainApi.Services.IServices;
using MailKit.Net.Smtp;
using MimeKit;
using System.Linq.Expressions;

namespace GMPP.MainApi.Services
{
    /// <summary>
    /// Сервис отправки отклика на вакансию на электронную почту
    /// </summary>
    public class ApplyForJobService : IApplyForJobService
    {
        public ApplyForJobService()
        {

        }

        /// <summary>
        /// Отправить отклик на Email
        /// </summary>
        /// <param name="applyForJob"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> SendResponsd(ApplyForJobDto applyForJob)
        {
            using var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта GMPP", StaticDetails.LoginEmail));
            emailMessage.To.Add(new MailboxAddress("", StaticDetails.AdresseeEmail));
            emailMessage.Subject = "Отклик";
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = applyForJob.TextResponsd,
            };

            try
            {
                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.mail.ru", 465, false);

                    await client.AuthenticateAsync(StaticDetails.LoginEmail, StaticDetails.PasswordEmail);
                    await client.SendAsync(emailMessage);

                    await client.DisconnectAsync(true);
                }
            }
            catch
            {
                throw new Exception("Не удалось отправить отклик");
            }

            return true;
        }
    }
}
