using System;
using Microsoft.AspNetCore.Mvc;
using Tarefas.Web.Models;
using Tarefas.DTO;
using Tarefas.DAO;
using AutoMapper;

namespace Tarefas.Web.Controllers
{
    public class TarefaController : Controller
 {  
    private readonly IMapper _mapper;
    public TarefaController (IMapper mapper)
    {
      _mapper = mapper;
    }     
    public IActionResult Criar()
      {
        return View();
      }

    [HttpPost]
      public IActionResult Criar(TarefaViewModel tarefa)
      {
        var tarefaDTO = _mapper.Map<TarefaDTO>(tarefa); 

        if(!ModelState.IsValid)
        {
          return View();
        }

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
          listaDeTarefa.Add(_mapper.Map<TarefaViewModel>(tarefaDTO));
        }

        return View(listaDeTarefa);
      }
      public IActionResult Detalhar(int Id)
      {
         var tarefaDAO = new TarefaDAO();
         var tarefaDTO = tarefaDAO.Detalhar(Id);
         var tarefaViewModel = _mapper.Map<TarefaViewModel>(tarefaDTO); 

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
         var tarefaViewModel = _mapper.Map<TarefaViewModel>(tarefaDTO);
         
         return View(tarefaViewModel);
      }

      [HttpPost]
      public IActionResult Editar(TarefaViewModel tarefa)
      {
        var tarefaDTO = _mapper.Map<TarefaDTO>(tarefa); 

        if(!ModelState.IsValid)
        {
          return View();
        }

        var tarefaDAO = new TarefaDAO();
        tarefaDAO.Editar(tarefaDTO);

        return RedirectToAction("Listar");
      }
  }
}
