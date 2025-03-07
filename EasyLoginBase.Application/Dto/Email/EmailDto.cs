namespace EasyLoginBase.Application.Dto.Email;

public class EmailDto
{
    public required string EmailDestinatario { get; set; }
    public required string Assunto { get; set; }
    public required string Corpo { get; set; }

    public static EmailDto ConfirmacaoEmail(string emailDestinatario, string codigo)
    {
        string corpoEmail = $@"
            <h2>Confirmação de E-mail</h2>
            <p>Olá,</p>
            <p>Obrigado por se cadastrar! Para confirmar seu e-mail,utilize o código para confimar: {codigo}:</p>
           
            <p>Se você não se cadastrou, ignore este e-mail.</p>
            <br/>
            <p>Atenciosamente,<br/>Equipe Easy Login </p>";

        return new EmailDto
        {
            EmailDestinatario = emailDestinatario,
            Assunto = "Confirmação de E-mail - EasyLoginBase",
            Corpo = corpoEmail
        };
    }
}

