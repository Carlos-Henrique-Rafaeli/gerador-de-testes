using GeradorDeTestes.Aplicacao.ModuloMateria;
using GeradorDeTestes.Aplicacao.ModuloQuestao;
using GeradorDeTestes.Dominio.ModuloQuestao;
using GeradorDeTestes.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;

namespace GeradorDeTestes.WebApp.Controllers;

[Route("questoes")]
public class QuestaoController : Controller
{
    private readonly QuestaoAppService questaoAppService;
    private readonly MateriaAppService materiaAppService;

    public QuestaoController(QuestaoAppService questaoAppService, MateriaAppService materiaAppService)
    {
        this.questaoAppService = questaoAppService;
        this.materiaAppService = materiaAppService;
    }


    [HttpGet]
    public IActionResult Index()
    {
        var resultado = questaoAppService.SelecionarTodos();

        if (resultado.IsFailed)
        {
            foreach (var erro in resultado.Errors)
            {
                var notificacaoJson = NotificacaoViewModel.GerarNotificacaoSerializada(
                    erro.Message,
                    erro.Reasons[0].Message
                );

                TempData.Add(nameof(NotificacaoViewModel), notificacaoJson);
                break;
            }

            return RedirectToAction("erro", "home");
        }

        var visualizarVM = new VisualizarQuestoesViewModel(resultado.ValueOrDefault);

        var existeNotificacao = TempData.TryGetValue(nameof(NotificacaoViewModel), out var valor);

        if (existeNotificacao && valor is string jsonString)
        {
            var notificacaoVm = JsonSerializer.Deserialize<NotificacaoViewModel>(jsonString);

            ViewData.Add(nameof(NotificacaoViewModel), notificacaoVm);
        }

        return View(visualizarVM);
    }

    [HttpGet("cadastrar")]
    public IActionResult Cadastrar()
    {
        var materias = materiaAppService.SelecionarTodos().ValueOrDefault;

        var cadastrarVM = new CadastrarQuestaoViewModel(materias);

        return View(cadastrarVM);
    }

    [HttpPost("cadastrar")]
    [ValidateAntiForgeryToken]
    public IActionResult Cadastrar(CadastrarQuestaoViewModel cadastrarVM)
    {
        var materias = materiaAppService.SelecionarTodos().ValueOrDefault;

        var entidade = CadastrarQuestaoViewModel.ParaEntidade(cadastrarVM, materias);

        var resultado = questaoAppService.Cadastrar(entidade);

        if (resultado.IsFailed)
        {
            foreach (var erro in resultado.Errors)
            {
                if (erro.Metadata["TipoErro"].ToString() == "RegistroDuplicado")
                {
                    ModelState.AddModelError("CadastroUnico", erro.Reasons[0].Message);
                    break;
                }
            }

            cadastrarVM.MateriasDisponiveis = materias
                .Select(d => new SelectListItem(d.Nome, d.Id.ToString()))
                .ToList();

            return View(cadastrarVM);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("editar/{id:guid}")]
    public IActionResult Editar(Guid id)
    {
        var resultado = questaoAppService.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            foreach (var erro in resultado.Errors)
            {
                var notificacaoJson = NotificacaoViewModel.GerarNotificacaoSerializada(
                    erro.Message,
                    erro.Reasons[0].Message
                );

                TempData.Add(nameof(NotificacaoViewModel), notificacaoJson);
                break;
            }

            return RedirectToAction(nameof(Index));
        }

        var materias = materiaAppService.SelecionarTodos().ValueOrDefault;

        var registro = resultado.Value;

        var editarVm = new EditarQuestaoViewModel(
            registro.Id,
            registro.Enunciado,
            registro.Materia.Id,
            registro.Alternativas,
            materias
        );

        return View(editarVm);
    }

    [HttpPost("editar/{id:guid}")]
    [ValidateAntiForgeryToken]
    public IActionResult Editar(Guid id, EditarQuestaoViewModel editarVM)
    {
        var materias = materiaAppService.SelecionarTodos().ValueOrDefault;

        var entidadeEditada = EditarQuestaoViewModel.ParaEntidade(editarVM, materias);

        var resultado = questaoAppService.Editar(id, entidadeEditada);

        if (resultado.IsFailed)
        {
            foreach (var erro in resultado.Errors)
            {
                if (erro.Metadata["TipoErro"].ToString() == "RegistroDuplicado")
                {
                    ModelState.AddModelError("CadastroUnico", erro.Reasons[0].Message);
                    break;
                }
            }

            editarVM.MateriasDisponiveis = materias
                .Select(d => new SelectListItem(d.Nome, d.Id.ToString()))
                .ToList();

            return View(editarVM);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("excluir/{id:guid}")]
    public IActionResult Excluir(Guid id)
    {
        var resultado = questaoAppService.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            foreach (var erro in resultado.Errors)
            {
                var notificacaoJson = NotificacaoViewModel.GerarNotificacaoSerializada(
                    erro.Message,
                    erro.Reasons[0].Message
                );

                TempData.Add(nameof(NotificacaoViewModel), notificacaoJson);
                break;
            }

            return RedirectToAction(nameof(Index));
        }

        var registro = resultado.Value;

        var excluirVM = new ExcluirQuestaoViewModel(
            registro.Id,
            registro.Enunciado
        );

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:guid}")]
    [ValidateAntiForgeryToken]
    public IActionResult Excluir(Guid id, ExcluirQuestaoViewModel excluirVM)
    {
        var resultado = questaoAppService.Excluir(id);

        if (resultado.IsFailed)
        {
            foreach (var erro in resultado.Errors)
            {
                var notificacaoJson = NotificacaoViewModel.GerarNotificacaoSerializada(
                    erro.Message,
                    erro.Reasons[0].Message
                );

                TempData.Add(nameof(NotificacaoViewModel), notificacaoJson);
                break;
            }
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("detalhes/{id:guid}")]
    public IActionResult Detalhes(Guid id)
    {
        var resultado = questaoAppService.SelecionarPorId(id);

        if (resultado.IsFailed)
        {
            foreach (var erro in resultado.Errors)
            {
                var notificacaoJson = NotificacaoViewModel.GerarNotificacaoSerializada(
                    erro.Message,
                    erro.Reasons[0].Message
                );

                TempData.Add(nameof(NotificacaoViewModel), notificacaoJson);
                break;
            }

            return RedirectToAction(nameof(Index));
        }

        var detalhesVm = DetalhesQuestaoViewModel.ParaDetalhesVm(resultado.Value);

        return View(detalhesVm);
    }

    [HttpPost, Route("/questoes/cadastrar/adicionar-alternativa")]
    public IActionResult AdicionarAlternativa(
        CadastrarQuestaoViewModel cadastrarVM,
        AdicionarAlternativaQuestaoViewModel alternativaVM)
    {
        cadastrarVM.MateriasDisponiveis = materiaAppService
            .SelecionarTodos()
            .ValueOrDefault
            .Select(d => new SelectListItem(d.Nome, d.Id.ToString()))
            .ToList();

        if (cadastrarVM.AlternativasSelecionadas.Any(a => a.Resposta.Equals(alternativaVM.Resposta)))
        {
            ModelState.AddModelError(
                "CadastroUnico",
                "Já existe uma alternativa registrada com esta resposta."
            );

            return View(nameof(Cadastrar), cadastrarVM);
        }

        if (alternativaVM.Correta && cadastrarVM.AlternativasSelecionadas.Any(a => a.Correta))
        {
            ModelState.AddModelError(
                "CadastroUnico",
                "Já existe uma alternativa registrada como correta."
            );

            return View(nameof(Cadastrar), cadastrarVM);
        }

        if (string.IsNullOrEmpty(alternativaVM.Resposta))
        {
            ModelState.AddModelError(
                "CadastroUnico",
                "A resposta deve conter algum texto."
            );

            return View(nameof(Cadastrar), cadastrarVM);
        }

        cadastrarVM.AdicionarAlternativa(alternativaVM);

        return View(nameof(Cadastrar), cadastrarVM);
    }

    [HttpPost, Route("/questoes/cadastrar/remover-alternativa/{letra:alpha}")]
    public IActionResult RemoverAlternativa(char letra, CadastrarQuestaoViewModel cadastrarVM)
    {
        var alternativa = cadastrarVM.AlternativasSelecionadas
            .Find(a => a.Letra.Equals(letra));

        if (alternativa is not null)
            cadastrarVM.RemoverAlternativa(alternativa);

        cadastrarVM.MateriasDisponiveis = materiaAppService
            .SelecionarTodos()
            .ValueOrDefault
            .Select(d => new SelectListItem(d.Nome, d.Id.ToString()))
            .ToList();

        return View(nameof(Cadastrar), cadastrarVM);
    }

    [HttpPost, Route("/questoes/editar/{id:guid}/adicionar-alternativa")]
    public IActionResult AdicionarAlternativa(EditarQuestaoViewModel editarVM, AdicionarAlternativaQuestaoViewModel alternativaVM)
    {
        var resultado = questaoAppService.AdicionarAlternativaEmQuestao(
            editarVM.Id,
            alternativaVM.Resposta,
            alternativaVM.Correta
        );

        var materias = materiaAppService
            .SelecionarTodos()
            .ValueOrDefault
            .Select(d => new SelectListItem(d.Nome, d.Id.ToString()))
            .ToList();

        editarVM.MateriasDisponiveis = materias;

        if (resultado.IsFailed)
        {
            foreach (var erro in resultado.Errors)
            {
                if (erro.Metadata["TipoErro"].ToString() == "RegistroDuplicado")
                {
                    ModelState.AddModelError("CadastroUnico", erro.Reasons[0].Message);
                    break;
                }

                if (erro.Metadata["TipoErro"].ToString() == "ExcecaoInterna")
                {
                    ModelState.AddModelError("CadastroUnico", "A resposta deve conter algum texto.");
                    break;
                }
            }

            return View(nameof(Editar), editarVM);
        }

        editarVM.AdicionarAlternativa(alternativaVM);

        return View(nameof(Editar), editarVM);
    }

    [HttpPost, Route("/questoes/editar/{id:guid}/remover-alternativa/{letra:alpha}")]
    public IActionResult RemoverAlternativa(char letra, EditarQuestaoViewModel editarVm)
    {
        var resultado = questaoAppService.RemoverAlternativaDeQuestao(letra, editarVm.Id);

        editarVm.MateriasDisponiveis = materiaAppService
            .SelecionarTodos()
            .ValueOrDefault
            .Select(d => new SelectListItem(d.Nome, d.Id.ToString()))
            .ToList();

        if (resultado.IsFailed)
            return View(nameof(Editar), editarVm);

        var alternativa = editarVm.AlternativasSelecionadas
            .Find(a => a.Letra.Equals(letra));

        if (alternativa is not null)
            editarVm.RemoverAlternativa(alternativa);

        return View(nameof(Editar), editarVm);
    }

    [HttpGet("gerar-questoes/primeira-etapa")]
    public IActionResult PrimeiraEtapaGerar()
    {
        var materias = materiaAppService.SelecionarTodos().ValueOrDefault;

        var primeiraEtapaVm = new PrimeiraEtapaGerarQuestoesViewModel(materias);

        return View(primeiraEtapaVm);
    }

    [HttpPost("gerar-questoes/primeira-etapa")]
    public async Task<IActionResult> PrimeiraEtapaGerar(PrimeiraEtapaGerarQuestoesViewModel primeiraEtapaVm)
    {
        var materiaSelecionada = materiaAppService.SelecionarPorId(primeiraEtapaVm.MateriaId).ValueOrDefault;

        var resultado = await questaoAppService.GerarQuestoesDaMateria(materiaSelecionada, primeiraEtapaVm.QuantidadeQuestoes);

        if (resultado.IsFailed)
        {
            foreach (var erro in resultado.Errors)
            {
                var notificacaoJson = NotificacaoViewModel.GerarNotificacaoSerializada(
                    erro.Message,
                    erro.Reasons[0].Message
                );

                TempData.Add(nameof(NotificacaoViewModel), notificacaoJson);
                break;
            }

            return RedirectToAction(nameof(Index));
        }

        var segundaEtapavm = new SegundaEtapaGerarQuestoesViewModel(resultado.Value)
        {
            MateriaId = primeiraEtapaVm.MateriaId,
            Materia = materiaSelecionada.Nome
        };

        var jsonString = JsonSerializer.Serialize(segundaEtapavm);

        TempData.Clear();

        TempData.Add(nameof(SegundaEtapaGerarQuestoesViewModel), jsonString);

        return RedirectToAction(nameof(SegundaEtapaGerar));
    }

    [HttpGet("gerar-questoes/segunda-etapa")]
    public IActionResult SegundaEtapaGerar()
    {
        var existeViewModel = TempData.TryGetValue(nameof(SegundaEtapaGerarQuestoesViewModel), out var valor);

        if (!existeViewModel || valor is not string jsonString)
            return RedirectToAction(nameof(PrimeiraEtapaGerar));

        var segundaEtapaVm = JsonSerializer.Deserialize<SegundaEtapaGerarQuestoesViewModel>(jsonString);

        return View(segundaEtapaVm);
    }

    [HttpPost("gerar-questoes/segunda-etapa")]
    public IActionResult SegundaEtapaGerar(SegundaEtapaGerarQuestoesViewModel segundaEtapaVm)
    {
        var materias = materiaAppService.SelecionarTodos().ValueOrDefault;

        var materiaSelecionada = materiaAppService.SelecionarPorId(segundaEtapaVm.MateriaId).ValueOrDefault;

        List<Questao> questoesGeradas = SegundaEtapaGerarQuestoesViewModel.ObterQuestoesGeradas(segundaEtapaVm, materiaSelecionada);

        foreach (var questao in questoesGeradas)
        {
            var resultado = questaoAppService.Cadastrar(questao);

            if (resultado.IsFailed)
                return View(nameof(PrimeiraEtapaGerar));
        }

        return RedirectToAction(nameof(Index));
    }
}
