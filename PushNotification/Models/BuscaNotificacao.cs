namespace PushNotification.Models
{
    public class BuscaNotificacao
    {
        public int? usuario_id { get; set; }
        public int? inscricao_id { get; set; }
        public Guid? aparelho_id { get; set; }

        public string? title { get; set; }
        public string? body { get; set; }
        public string? icon { get; set; }

    }
}
