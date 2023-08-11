using System;
using Microsoft.AspNetCore.Mvc;
using Tarefas.Web.Models;

namespace Tarefas.Web.Controllers
{
    public class TarefaController : Controller
 {
        public List<TarefaViewModel> listaDeTarefas = new List<TarefaViewModel>();

        public TarefaController()
        {
         var tarefa1 = new TarefaViewModel();
         tarefa1.Id = 1;
         tarefa1.Descricao = "Comprar pão de sal";
         tarefa1.Titulo = "Pão";

         var tarefa2 = new TarefaViewModel();
         tarefa2.Id = 2;
         tarefa2.Descricao = "Comprar suco de uva";
         tarefa2.Titulo = "Suco";

         listaDeTarefas.Add(tarefa1);
         listaDeTarefas.Add(tarefa2);
        }

        public IActionResult Criar()
      {
        return View();
      }

      [HttpPost]
      public IActionResult Criar(TarefaViewModel tarefa)
      {
        return View(); 
      }
      public IActionResult Listar()
      {
        return View(listaDeTarefas); 
      }
      public IActionResult Detalhar(int Id)
      {
         var tarefa = listaDeTarefas.FirstOrDefault(f => f.Id ==Id);
         return View(tarefa);
      }
  }
}