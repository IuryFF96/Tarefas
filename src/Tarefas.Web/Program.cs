using Tarefas.DAO;
using Tarefas.DTO;
using Tarefas.Web.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var config = new AutoMapper.MapperConfiguration(c => {
    c.CreateMap<TarefaViewModel, TarefaDTO>().ReverseMap();
    c.CreateMap<UsuarioViewModel, UsuarioDTO>().ReverseMap();
    c.CreateMap<LoginViewModel, UsuarioDTO>().ReverseMap();
});
IMapper mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddTransient<ITarefaDAO,TarefaDAO>();
builder.Services.AddTransient<IUsuarioDAO,UsuarioDAO>();
builder.Services.AddSingleton<IDataBaseBootstrap, DataBaseBootstrap>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x=>x.LoginPath="/Usuario");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Services.GetService<IDataBaseBootstrap>()!.Setup();

app.Run();
