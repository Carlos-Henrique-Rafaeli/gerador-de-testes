using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Dominio.ModuloTeste;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace GeradorDeTestes.Infraestrutura.Orm.ModuloTeste;

public class RepositorioTesteEmOrm : RepositorioBaseEmOrm<Teste>, IRepositorioTeste
{
    public RepositorioTesteEmOrm(GeradorDeTestesDbContext contexto) : base(contexto)
    {
    }

    public override Teste? SelecionarRegistroPorId(Guid idRegistro)
    {
        return registros
            .Include(t => t.Questoes)
            .ThenInclude(q => q.Alternativas)
            .Include(t => t.Questoes)
            .ThenInclude(q => q.Materia)
            .Include(t => t.Disciplina)
            .Include(t => t.Materia)
            .FirstOrDefault(x => x.Id.Equals(idRegistro));
    }

    public override List<Teste> SelecionarRegistros()
    {
        return registros
            .Include(t => t.Questoes)
            .ThenInclude(q => q.Materia)
            .Include(t => t.Disciplina)
            .Include(t => t.Materia)
            .ToList();
    }
}
