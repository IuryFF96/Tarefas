using System;
using Microsoft.AspNetCore.Mvc;
using Tarefas.Web.Models;
using Tarefas.DTO;
using Tarefas.DAO;

namespace Tarefas.Web.Controllers
{
    public class TarefaController : Controller
 {       
    public IActionResult Criar()
      {
        return View();
      }

      [HttpPost]
      public IActionResult Criar(TarefaViewModel tarefa)
      {
        var tarefaDTO = new TarefaDTO 
        {
          Titulo = tarefa.Titulo,
          Descricao = tarefa.Descricao,
          Status = tarefa.Status
        };

    var tarefaDAO = new TarefaDAO();
    tarefaDAO.Criar(tarefaDTO);

    return RedirectToAction("Listar");
      }
      public IActionResult Listar()
      {
        var tarefaDAO = new TarefaDAO();
        var listaDeTarefasDTO = tarefaDAO.Consultar();
        var listaDeTarefa = new List<TarefaViewModel>();
        foreach (var tarefaDTO in listaDeTarefasDTO)
        {
          listaDeTarefa.Add(new TarefaViewModel()
          {
            Id = tarefaDTO.Id,
            Titulo = tarefaDTO.Titulo,
            Descricao = tarefaDTO.Descricao,
            Status = tarefaDTO.Status
          });
        }

    return View(listaDeTarefa);
      }
      public IActionResult Detalhar(int Id)
      {
         var tarefaDAO = new TarefaDAO();
         var tarefaDTO = tarefaDAO.Detalhar(Id);
         var tarefaViewModel = new TarefaViewModel()
         {
          Id = tarefaDTO.Id,
          Titulo = tarefaDTO.Titulo,
          Descricao = tarefaDTO.Descricao,
          Status = tarefaDTO.Status
         };
         return View(tarefaViewModel);
      }
      public IActionResult Excluir(int Id)
      {
         var tarefaDAO = new TarefaDAO();
         tarefaDAO.Excluir(Id);

         return RedirectToAction("Listar");
      }
      public IActionResult Editar(int Id)
      {
         var tarefaDAO = new TarefaDAO();
         var tarefaDTO = tarefaDAO.Detalhar(Id);
         var tarefaViewModel = new TarefaViewModel()
         {
          Id = tarefaDTO.Id,
          Titulo = tarefaDTO.Titulo,
          Descricao = tarefaDTO.Descricao,
          Status = tarefaDTO.Status
         };
         return View(tarefaViewModel);
      }
      [HttpPost]
      public IActionResult SalvarEdicao(TarefaViewModel tarefa)
      {
        var tarefaDTO = new TarefaDTO() 
        {
          Id = tarefa.Id,
          Titulo = tarefa.Titulo,
          Descricao = tarefa.Descricao,
          Status = tarefa.Status
        };

        var tarefaDAO = new TarefaDAO();
        tarefaDAO.Editar(tarefaDTO);

        return RedirectToAction("Listar");
      }
  }
}
