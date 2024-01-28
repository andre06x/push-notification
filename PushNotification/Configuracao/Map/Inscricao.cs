using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using PushNotification.Models;

namespace PushNotification.Configuracao.Map
{
    public class InscricaoMap : IEntityTypeConfiguration<Inscricao>
    {
        public void Configure(EntityTypeBuilder<Inscricao> builder)
        {
            builder.HasKey(x => x.id);
            builder.Property(x => x.id).ValueGeneratedOnAdd();

            builder.Property(x => x.aparelho).IsRequired();
            builder.Property(x => x.endpoint).IsRequired();
            builder.Property(x => x.auth).IsRequired();
            builder.Property(x => x.p26dh).IsRequired();

            builder.Property(x => x.namePlatform);
            builder.Property(x => x.versionPlatform);
            builder.Property(x => x.layoutPlatform);
            builder.Property(x => x.preleasePlatform);
            builder.Property(x => x.osPlatform);
            builder.Property(x => x.manufacturerPlatform);
            builder.Property(x => x.productPlatform);
            builder.Property(x => x.descriptionPlatform);
            builder.Property(x => x.uaPlatform);
            builder.Property(x => x.createdAt);
            builder.Property(x => x.updatedAt);

            builder.Property(x => x.heightScreen);
            builder.Property(x => x.widthScreen);


            builder.HasIndex(x => x.endpoint).IsUnique();

            builder.Property(x => x.usuario_id);
            builder.HasOne(x => x.Usuario)
               .WithMany()  
               .HasForeignKey(x => x.usuario_id)
               .OnDelete(DeleteBehavior.Restrict);  
        }
    }
}
