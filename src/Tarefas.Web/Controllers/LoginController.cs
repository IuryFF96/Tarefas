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
    public class LoginController : Controller
 {  
    private readonly IMapper _mapper;
    private readonly IUsuarioDAO _usuarioDAO;
    
    public LoginController (IMapper mapper, IUsuarioDAO usuarioDAO)
    {
      _mapper = mapper;
      _usuarioDAO = usuarioDAO;
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
            user = _usuarioDAO.ValidarLogin(usuarioDTO);
          }
          catch(Exception ex)
          {
            ModelState.AddModelError(String.Empty,ex.Message);
            return View();
          }
          if (user == null)
		      {
            ModelState.AddModelError("login.invalido", "Email ou senha inválido.");
            return View();
		      }

          var claims = new List<Claim>
          {
            new Claim(ClaimTypes.Name,user.Nome),
            new Claim(ClaimTypes.Email,user.Email),
            new Claim(ClaimTypes.PrimarySid,user.Id.ToString())
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
      public IActionResult Sair()
      {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return LocalRedirect("/Login");
      }
  }
}