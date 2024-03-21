using System;
using Microsoft.AspNetCore.Mvc;
using Tarefas.Web.Models;
using Tarefas.DTO;
using Tarefas.DAO;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Tarefas.Web.Controllers
{
  [Authorize]
    public class AgendaController : Controller
 {  
    private readonly int _usuarioid;
    private readonly IMapper _mapper;
    private readonly IAgendaDAO _agendaDAO;
    
    public AgendaController (IMapper mapper, IAgendaDAO agendaDAO, IHttpContextAccessor httpContextAccessor)
    {
      _mapper = mapper;
      _agendaDAO = agendaDAO;
      _usuarioid = int.Parse(httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.PrimarySid));
    }     
    public IActionResult CriarAgenda()
      {
        ViewBag.UsuarioId = _usuarioid;
        return View();
      }

      [HttpPost]
      public IActionResult CriarAgenda(AgendaViewModel agenda)
      {
        ViewBag.UsuarioId = _usuarioid;
        if(ModelState.IsValid)
        {
          var agendaDTO = _mapper.Map<AgendaDTO>(agenda); 
          AgendaDTO agendas;
          try
          {
            agendas = _agendaDAO.ValidarAgenda(agendaDTO);
          }
          catch(Exception ex)
          {
            ModelState.AddModelError(String.Empty,ex.Message);
            return View();
          }
          if (agendas != null)
		      {
            ModelState.AddModelError("agenda.invalida", "Agenda já disponível na data e hora selecionada.");
            return View();
		      }
        
        _agendaDAO.CriarAgenda(agendaDTO);

        return RedirectToAction("ListarAgenda");
      }
        return View();
      }
      public IActionResult ListarAgenda()
      {
        var listaDeAgendasDTO = _agendaDAO.ConsultarAgenda(_usuarioid);
        var listaDeAgenda = new List<AgendaViewModel>();
        foreach (var agendaDTO in listaDeAgendasDTO)
        {
          listaDeAgenda.Add(_mapper.Map<AgendaViewModel>(agendaDTO));
        }

        return View(listaDeAgenda);
      }
      public IActionResult DetalharAgenda(int Id)
      {        
         var agendaDTO = _agendaDAO.DetalharAgenda(Id, _usuarioid);
         var agendaViewModel = _mapper.Map<AgendaViewModel>(agendaDTO);

         return View(agendaViewModel);
      }
      public IActionResult ExcluirAgenda(int Id)
      {
         _agendaDAO.ExcluirAgenda(Id, _usuarioid);

         return RedirectToAction("ListarAgenda");
      }
      public IActionResult EditarAgenda(int Id)
      {    
         var agendaDTO = _agendaDAO.DetalharAgenda(Id, _usuarioid);
         var agendaViewModel = _mapper.Map<AgendaViewModel>(agendaDTO);
         
         return View(agendaViewModel);
      }

      [HttpPost]
      public IActionResult EditarAgenda(AgendaViewModel agenda)
      {
        if(ModelState.IsValid)
        {
          var agendaDTO = _mapper.Map<AgendaDTO>(agenda); 
          AgendaDTO agendas;
          try
          {
            agendas = _agendaDAO.ValidarEditAgenda(agendaDTO);
          }
          catch(Exception ex)
          {
            ModelState.AddModelError(String.Empty,ex.Message);
            return View();
          }
          if (agendas != null)
		      {
            ModelState.AddModelError("agenda.invalida", "Agenda já disponível na data e hora selecionada.");
            return View();
		      }
        
        _agendaDAO.EditarAgenda(agendaDTO);

        return RedirectToAction("ListarAgenda");
      }
        return View();
      }
  }
}
