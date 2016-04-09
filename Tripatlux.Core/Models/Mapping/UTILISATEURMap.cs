using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Tripatlux.Core.Models.Mapping
{
    public class UTILISATEURMap : EntityTypeConfiguration<UTILISATEUR>
    {
        public UTILISATEURMap()
        {
            // Primary Key
            this.HasKey(t => t.ID);

            // Properties
            this.Property(t => t.EMAIL_LOGIN)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.MOT_DE_PASSE)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.EMAIL_NOTIFICATION)
                .HasMaxLength(50);

            this.Property(t => t.SMS_NOTIFICATION)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.NOM)
                .HasMaxLength(50);

            this.Property(t => t.PRENOM)
                .HasMaxLength(50);

            this.Property(t => t.ID_ASP_NET_USER)
                .HasMaxLength(128);

            // Table & Column Mappings
            this.ToTable("UTILISATEUR", "trip");
            this.Property(t => t.ID).HasColumnName("ID");
            this.Property(t => t.EMAIL_LOGIN).HasColumnName("EMAIL_LOGIN");
            this.Property(t => t.MOT_DE_PASSE).HasColumnName("MOT_DE_PASSE");
            this.Property(t => t.EMAIL_NOTIFICATION).HasColumnName("EMAIL_NOTIFICATION");
            this.Property(t => t.SMS_NOTIFICATION).HasColumnName("SMS_NOTIFICATION");
            this.Property(t => t.NOM).HasColumnName("NOM");
            this.Property(t => t.PRENOM).HasColumnName("PRENOM");
            this.Property(t => t.CREATION).HasColumnName("CREATION");
            this.Property(t => t.DERNIERE_CONNEXION).HasColumnName("DERNIERE_CONNEXION");
            this.Property(t => t.EST_VALIDE).HasColumnName("EST_VALIDE");
            this.Property(t => t.VALIDATION_LE).HasColumnName("VALIDATION_LE");
            this.Property(t => t.PHOTO).HasColumnName("PHOTO");
            this.Property(t => t.ID_ASP_NET_USER).HasColumnName("ID_ASP_NET_USER");

            // Relationships
            this.HasOptional(t => t.AspNetUser)
                .WithMany(t => t.UTILISATEURs)
                .HasForeignKey(d => d.ID_ASP_NET_USER);

        }
    }
}
