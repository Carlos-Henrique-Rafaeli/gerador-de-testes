using GeradorDeTestes.Dominio.ModuloQuestao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeradorDeTestes.Infraestrutura.Orm.ModuloQuestao;

public class MapeadorAlternativa : IEntityTypeConfiguration<Alternativa>
{
    public void Configure(EntityTypeBuilder<Alternativa> builder)
    {
        builder.Property(a => a.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(a => a.Letra)
            .IsRequired();

        builder.Property(a => a.Resposta)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(a => a.Correta)
            .IsRequired();

        builder.HasOne(a => a.Questao)
            .WithMany(q => q.Alternativas)
            .IsRequired();
    }
}
