﻿using GeradorDeTestes.Dominio.ModuloQuestao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeradorDeTestes.Infraestrutura.Orm.ModuloQuestao;

public class MapeadorQuestao : IEntityTypeConfiguration<Questao>
{
    public void Configure(EntityTypeBuilder<Questao> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.HasOne(x => x.Materia)
            .WithMany(x => x.Questoes);

        builder.Property(x => x.Enunciado)
            .HasMaxLength(300)
            .IsRequired();

        builder.HasMany(x => x.Alternativas)
            .WithOne(x => x.Questao);
    }
}
