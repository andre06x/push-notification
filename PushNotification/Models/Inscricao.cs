using System.ComponentModel.DataAnnotations.Schema;

namespace PushNotification.Models
{
    public class Inscricao
    {
        [Column("id")]
        public int id { get; set; }

        [Column("aparelho")]
        public Guid aparelho { get; set; }

        [Index(IsUnique = true)]
        [Column("endpoint")]
        public string endpoint { get; set; }

        [Column("p26dh")]
        public string p26dh { get; set; }

        [Column("auth")]
        public string auth { get; set; }


        [Column("usuario_id")]
        public int? usuario_id { get; set; }

        [ForeignKey("usuario_id")]
        public virtual Usuario? Usuario { get; set; }

    }
}
