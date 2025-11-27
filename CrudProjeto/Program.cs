using CrudProjeto.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// Add services to the container.
builder.Services.AddControllers(); // <-- MUDANÇA 1: Mais adequado para API do que AddControllersWithViews()

// Adicionar suporte ao Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString,
    ServerVersion.AutoDetect(connectionString)));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy =>
        {
            // <-- MUDANÇA 2: Adicione a URL do seu frontend do RENDER aqui
            policy.WithOrigins("http://localhost:5173", "https://crud-online-usuarios.onrender.com")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

// <-- MUDANÇA 3: Mover Swagger para FORA do IF
// Habilita o Swagger em TODOS os ambientes (Development e Production)
app.UseSwagger();
app.UseSwaggerUI();

if (app.Environment.IsDevelopment())
{
    // O 'if' agora pode ficar vazio
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// <-- MUDANÇA 4: O CORS deve vir ANTES da Autorização e dos Controllers
app.UseCors("AllowReactApp");

app.UseAuthorization();

// <-- MUDANÇA 5: ESSENCIAL para suas rotas de API [ApiController] funcionarem
app.MapControllers();

// A linha abaixo é para MVC, não precisamos dela para a API
// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();