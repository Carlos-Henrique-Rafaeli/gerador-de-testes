using GeradorDeTestes.Dominio.ModuloDisciplina;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace GeradorDeTestes.Infraestrutura.Orm.ModuloDisciplina;

public class RepositorioDisciplinaEmOrm : RepositorioBaseEmOrm<Disciplina>, IRepositorioDisciplina
{
    public RepositorioDisciplinaEmOrm(GeradorDeTestesDbContext contexto) : base(contexto)
    {
    }

    public override Disciplina? SelecionarRegistroPorId(Guid idRegistro)
    {
        return registros
            .Include(x => x.Materias)
            .FirstOrDefault(x => x.Id.Equals(idRegistro));
    }

    public override List<Disciplina> SelecionarRegistros()
    {
        return registros
            .Include(x => x.Materias)
            .ToList();
    }
}
