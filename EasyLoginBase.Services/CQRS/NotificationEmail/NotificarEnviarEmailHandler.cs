using EasyLoginBase.Services.Services.Email;
using MediatR;

namespace EasyLoginBase.Services.CQRS.NotificationEmail;

public class NotificarEnviarEmailHandler : INotificationHandler<EmailNotification>
{
    private readonly IEmailService _emailService;
    public NotificarEnviarEmailHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }
    public async Task Handle(EmailNotification notification, CancellationToken cancellationToken)
    {
        await _emailService.EnviarEmailAsync(notification.EmailDto);
    }
}
