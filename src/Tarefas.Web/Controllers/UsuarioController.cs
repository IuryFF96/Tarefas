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
        var usuarioDTO = _mapper.Map<UsuarioDTO>(usuario);

        if(!ModelState.IsValid)
        {
          return View();
        }

        _usuarioDAO.CriarUsuario(usuarioDTO);

        return View();
      }

      public IActionResult Index()
      {
        return View();
      }
      
    [HttpPost]
      public IActionResult Index(LoginViewModel usuario)
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

          var claims = new List<Claim>
          {
            new Claim(ClaimTypes.Name,user.Nome),
            new Claim(ClaimTypes.Email,user.Email)
          };

          var claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);

          var authProperts = new AuthenticationProperties
          {
            ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
            IsPersistent = true,
            RedirectUri = "/Home" 
          };

          HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,new ClaimsPrincipal(claimsIdentity),authProperts);
          return LocalRedirect(authProperts.RedirectUri);
        }
        return View();
      }
  }
}