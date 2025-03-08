using EasyLoginBase.Application.Dto.Email;
using MediatR;

namespace EasyLoginBase.Services.CQRS.NotificationEmail;

public class EmailNotification : INotification
{
    public EmailDto EmailDto { get; }

    public EmailNotification(EmailDto emailDto)
    {
        EmailDto = emailDto;
    }
}
