namespace PushNotification.Models
{
    public class PushNotificationModel
    {
        public string Endpoint { get; set; }
        public string P256dh { get; set; }
        public string Auth { get; set; }
        public string body { get; set; }
        public string title { get; set; }
        public string? icon { get; set; }
    }


}
