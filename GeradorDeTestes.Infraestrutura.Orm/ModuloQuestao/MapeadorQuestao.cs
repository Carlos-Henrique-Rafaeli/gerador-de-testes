using GeradorDeTestes.Dominio.ModuloQuestao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeradorDeTestes.Infraestrutura.Orm.ModuloQuestao;

public class MapeadorQuestao : IEntityTypeConfiguration<Questao>
{
    public void Configure(EntityTypeBuilder<Questao> builder)
    {
        builder.Property(q => q.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(q => q.Enunciado)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(q => q.UtilizadaEmTeste)
            .IsRequired();

        builder.HasOne(q => q.Materia)
            .WithMany(m => m.Questoes)
            .IsRequired();

        builder.HasMany(q => q.Alternativas)
            .WithOne(a => a.Questao);
    }
}

