using System.ComponentModel.DataAnnotations.Schema;

namespace PushNotification.Models
{
    public class Usuario
    {
        [Column("id")]

        public int id { get; set; }

        [Column("nome")]
        public string? nome { get; set; }
    }
}
