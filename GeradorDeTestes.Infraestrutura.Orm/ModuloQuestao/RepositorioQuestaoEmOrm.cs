using GeradorDeTestes.Dominio.ModuloMateria;
using GeradorDeTestes.Dominio.ModuloQuestao;
using GeradorDeTestes.Infraestrutura.Orm.Compartilhado;
using Microsoft.EntityFrameworkCore;

namespace GeradorDeTestes.Infraestrutura.Orm.ModuloQuestao;

public class RepositorioQuestaoEmOrm : RepositorioBaseEmOrm<Questao>, IRepositorioQuestao
{
    public RepositorioQuestaoEmOrm(GeradorDeTestesDbContext contexto) : base(contexto) { }

    public List<Questao> SelecionarQuestoesPorDisciplinaESerie(Guid disciplinaId, Serie serie, int quantidadeQuestoes)
    {
        return registros
            .Include(q => q.Alternativas)
            .Include(q => q.Materia)
            .ThenInclude(m => m.Disciplina)
            .Where(x => x.Materia.Disciplina.Id.Equals(disciplinaId))
            .Where(x => x.Materia.Serie.Equals(serie))
            .Take(quantidadeQuestoes)
            .ToList();
    }

    public List<Questao> SelecionarQuestoesPorMateria(Guid materiaId, int quantidadeQuestoes)
    {
        return registros
            .Include(q => q.Alternativas)
            .Include(q => q.Materia)
            .Where(x => x.Materia.Id.Equals(materiaId))
            .Take(quantidadeQuestoes)
            .ToList();
    }

    public override Questao? SelecionarRegistroPorId(Guid idRegistro)
    {
        return registros
            .Include(x => x.Alternativas)
            .Include(x => x.Materia)
            .FirstOrDefault(x => x.Id.Equals(idRegistro));
    }

    public override List<Questao> SelecionarRegistros()
    {
        return registros
            .Include(x => x.Alternativas)
            .Include(x => x.Materia)
            .ToList();
    }
}
