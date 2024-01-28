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

        //
        [Column("namePlatform")]
        public string? namePlatform { get; set; }

        [Column("versionPlatform")]
        public string? versionPlatform { get; set; }
        
        [Column("layoutPlatform")]
        public string? layoutPlatform { get; set; }
        
        [Column("preleasePlatform")]
        public string? preleasePlatform { get; set; }
       
        [Column("osPlatform")]
        public string? osPlatform { get; set; }
        
        [Column("manufacturerPlatform")]
        public string? manufacturerPlatform { get; set; }

        [Column("productPlatform")]
        public string? productPlatform { get; set; }

        [Column("descriptionPlatform")]
        public string? descriptionPlatform { get; set; }

        [Column("uaPlatform")]
        public string? uaPlatform { get; set; }
       

        [Column("createdAt")]
        public DateTime? createdAt { get; set; }

        [Column("updatedAt")]
        public DateTime? updatedAt { get; set; }

        [Column("widthScreen")]
        public string? widthScreen { get; set; }

        [Column("heightScreen")]
        public string? heightScreen { get; set; }

        [Column("usuario_id")]
        public int? usuario_id { get; set; }

        [ForeignKey("usuario_id")]
        public virtual Usuario? Usuario { get; set; }


        public void CriarData()
        {
            createdAt = DateTime.UtcNow;
        }

        public void AtualizarData()
        {
            updatedAt = updatedAt ?? DateTime.UtcNow;
        }
    }
}
