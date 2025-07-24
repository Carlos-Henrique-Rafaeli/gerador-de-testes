using GeradorDeTestes.Aplicacao;
using GeradorDeTestes.WebApp.Extensions;
using GeradorDeTestes.WebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GeradorDeTestes.WebApp.Controllers;


    [Route("disciplinas")]
public class DisciplinaController : Controller
{
    private readonly DisciplinaAppService disciplinaAppService;

    public DisciplinaController(DisciplinaAppService disciplinaAppService)
    {
        this.disciplinaAppService = disciplinaAppService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var resultado = disciplinaAppService.SelecionarTodos();

        if (resultado.IsFailed)
            return RedirectToAction("Home/Index");

        var registros = resultado.Value;

        var visualizarVM = new VisualizarDisciplinaViewModel(registros);

        var existeNotificacao = TempData.TryGetValue(nameof(NotificacaoViewModel), out var valor);

        if (existeNotificacao && valor is string jsonString)
        {
            var notificacaoVm = JsonSerializer.Deserialize<NotificacaoViewModel>(jsonString);

            ViewData.Add(nameof(NotificacaoViewModel), notificacaoVm);
        }


        return View(visualizarVM);
    }

    [HttpGet("Cadastrar")]
    public IActionResult Cadastrar()
    {
        var cadastarVM = new CadastrarDisciplinaViewModel();

        return View(cadastarVM);
    }

    [HttpPost("cadastrar")]
    [ValidateAntiForgeryToken]
    public IActionResult Cadastrar(CadastrarDisciplinaViewModel cadastrarVM)
    {
        var entidade = cadastrarVM.ParaEntidade();

        var resultado = disciplinaAppService.Cadastrar(entidade);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError("CadastroUnico", resultado.Errors[0].Message);

            return View(cadastrarVM);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("editar/{id:guid}")]
    public IActionResult Editar(Guid id)
    {
        var resultado = disciplinaAppService.SelecionarPorId(id);

        if (resultado.IsFailed)
            return RedirectToAction(nameof(Index));

        var registroSelecionado = resultado.Value;

        var editarVM = new EditarDisciplinaViewMode(
            registroSelecionado.Id,
            registroSelecionado.Nome
        );

        return View(editarVM);
    }

    [HttpPost("editar/{id:guid}")]
    public IActionResult Editar(Guid id, EditarDisciplinaViewMode editarVM)
    {
        var entidadeEditada = editarVM.ParaEntidade();

        var resultado = disciplinaAppService.Editar(id, entidadeEditada);

        if (resultado.IsFailed)
        {
            ModelState.AddModelError("CadastroUnico", resultado.Errors[0].Message);

            return View(editarVM);
        }

        return RedirectToAction(nameof(Index));
    }

    [HttpGet("excluir/{id:guid}")]
    public ActionResult Excluir(Guid id)
    {
        var resultado = disciplinaAppService.SelecionarPorId(id);

        if (resultado.IsFailed)
            return RedirectToAction(nameof(Index));

        var registroSelecionado = resultado.Value;

        var excluirVM = new ExcluirDisciplinaViewModel(registroSelecionado.Id, registroSelecionado.Nome);

        return View(excluirVM);
    }

    [HttpPost("excluir/{id:guid}")]
    public ActionResult ExcluirConfirmado(Guid id)
    {
        var resultado = disciplinaAppService.Excluir(id);

        if (resultado.IsFailed)
        {
            foreach (var erro in resultado.Errors)
            {
                if (erro.Metadata["TipoErro"].ToString() == "RegistroDuplicado")
                {
                    var notificacaoJson = NotificacaoViewModel.GerarNotificacaoSerializada(
                        erro.Message,
                        erro.Reasons[0].Message
                    );

                    TempData.Add(nameof(NotificacaoViewModel), notificacaoJson);

                    break;
                }
                else
                {
                    return RedirectToAction("Home/Erro");
                }
            }
        }

        return RedirectToAction(nameof(Index));
    }
}


 

