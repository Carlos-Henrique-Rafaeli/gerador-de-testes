﻿namespace GeradorDeTestes.Dominio.Compartilhado;

public abstract class EntidadeBase<T>
{
    public Guid Id { get; set; }

    protected EntidadeBase()
    {
        Id = Guid.NewGuid();
    }

    public abstract void AtualizarRegistro(T registroEditado);
}
