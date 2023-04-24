﻿using GMPP.MainApi.Models.Dtos;
using GMPP.MainApi.Services.IServices;
using MailKit.Net.Smtp;
using MailKit.Security;
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
            emailMessage.To.Add(new MailboxAddress("Пользователь", StaticDetails.AdresseeEmail));
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
                    await client.ConnectAsync("smtp.mail.ru", 2525, false);

                    await client.AuthenticateAsync(StaticDetails.LoginEmail, StaticDetails.PasswordEmail);
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
