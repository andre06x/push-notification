using Microsoft.EntityFrameworkCore;
using PushNotification.Configuracao.Map;
using PushNotification.Models;

namespace PushNotification.Configuracao
{
        public class Contexto : DbContext
        {
            public Contexto(DbContextOptions<Contexto> options) : base(options)
            {
                Database.EnsureCreated();
            }

            public DbSet<Usuario> Usuario { get; set; }
            public DbSet<Inscricao> Inscricao { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.ApplyConfiguration(new InscricaoMap());
                modelBuilder.ApplyConfiguration(new UsuarioMap());

                base.OnModelCreating(modelBuilder);
            }

        
    }
}
