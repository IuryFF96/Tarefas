using System;
using Microsoft.AspNetCore.Mvc;
using Tarefas.Web.Models;
using Tarefas.DTO;
using Tarefas.DAO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace Tarefas.Web.Controllers
{
  [Authorize]
    public class TarefaController : Controller
 {  
    private readonly IMapper _mapper;
    private readonly ITarefaDAO _tarefaDAO;
    
    public TarefaController (IMapper mapper, ITarefaDAO tarefaDAO)
    {
      _mapper = mapper;
      _tarefaDAO = tarefaDAO;
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

        _tarefaDAO.Criar(tarefaDTO);

        return RedirectToAction("Listar");

      }
      public IActionResult Listar()
      {
        var listaDeTarefasDTO = _tarefaDAO.Consultar();
        var listaDeTarefa = new List<TarefaViewModel>();
        foreach (var tarefaDTO in listaDeTarefasDTO)
        {
          listaDeTarefa.Add(_mapper.Map<TarefaViewModel>(tarefaDTO));
        }

        return View(listaDeTarefa);
      }
      public IActionResult Detalhar(int Id)
      {         
         var tarefaDTO = _tarefaDAO.Detalhar(Id);
         var tarefaViewModel = _mapper.Map<TarefaViewModel>(tarefaDTO); 

         return View(tarefaViewModel);
      }
      public IActionResult Excluir(int Id)
      {
         _tarefaDAO.Excluir(Id);

         return RedirectToAction("Listar");
      }
      public IActionResult Editar(int Id)
      {
         var tarefaDTO = _tarefaDAO.Detalhar(Id);
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

        _tarefaDAO.Editar(tarefaDTO);

        return RedirectToAction("Listar");
      }
  }
}
