using GMPP.MainApi.Models.Dtos;
using GMPP.MainApi.Services.IServices;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Linq.Expressions;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace GMPP.MainApi.Services
{
    /// <summary>
    /// Сервис отправки отклика на вакансию на электронную почту
    /// </summary>
    public class ApplyForJobService : IApplyForJobService
    {
        private readonly IConfiguration _config;
        private readonly string _adminLoginEmail;
        private readonly string _adminPassEmail;

        public ApplyForJobService(IConfiguration config)
        {
            _config = config;
            _adminLoginEmail = config["AuthEmail"];
            _adminPassEmail = config["AuthPath"];
        }

        /// <summary>
        /// Отправить отклик на Email
        /// </summary>
        /// <param name="applyForJob"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> SendResponse(ApplyForJobDto applyForJob)
        {
            using var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Администрация сайта GMPP", _adminLoginEmail));
            emailMessage.To.Add(new MailboxAddress("Пользователь", _config["AddresseeEmail"]));
            emailMessage.Subject = "Отклик";
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = applyForJob.TextResponsd,
            };

            try
            {
                using (var client = new SmtpClient())
                {
                    //Сейчас используется порт, который не поддерживает не SSL, не TLS
                    //В будущем стоит арендовать или воспользоваться бесплатной версией собственного SMTP сервера
                    //Это безопаснее и позволит избежать пробелм с тем, что письма не доходят или попадают в спам
                    await client.ConnectAsync(StaticDetails.SmtpServerMailRu, StaticDetails.SmtpPortMailRu, false);
                    //Строка ниже работает только с отклчюченным антивирусом, т.к. с включенным не проходит проверка сертификата SSL
                    //await client.ConnectAsync("smtp.mail.ru", 465, true);

                    await client.AuthenticateAsync(_adminLoginEmail, _adminPassEmail);
                    await client.SendAsync(emailMessage);

                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Не удалось отправить отклик. Message - {ex.Message}");
            }

            return true;
        }
    }
}
