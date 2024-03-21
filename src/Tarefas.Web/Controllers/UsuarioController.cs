using System;
using Microsoft.AspNetCore.Mvc;
using Tarefas.Web.Models;
using Tarefas.DTO;
using AutoMapper;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace Tarefas.Web.Controllers
{
    public class UsuarioController : Controller
 {  
    private readonly IMapper _mapper;
    private readonly IUsuarioDAO _usuarioDAO;
    
    public UsuarioController (IMapper mapper, IUsuarioDAO usuarioDAO)
    {
      _mapper = mapper;
      _usuarioDAO = usuarioDAO;
    }     
    public IActionResult CriarUsuario()
      {
        return View();
      }

    [HttpPost]
      public IActionResult CriarUsuario(UsuarioViewModel usuario)
      {
        if(ModelState.IsValid)
        {
          var usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);
          UsuarioDTO user;
          try
          {
            user = _usuarioDAO.ValidarUsuario(usuarioDTO);
          }
          catch(Exception ex)
          {
            ModelState.AddModelError(String.Empty,ex.Message);
            return View();
          }
          if (user != null)
		      {
            ModelState.AddModelError("cadastro.invalido", "Email já existe.");
            return View();
		      }
        
        _usuarioDAO.CriarUsuario(usuarioDTO);

        return RedirectToAction("Index","Home");
      }
        return View();
      }
  }
  
}