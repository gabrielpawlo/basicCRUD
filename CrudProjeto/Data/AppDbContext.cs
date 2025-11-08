using Microsoft.EntityFrameworkCore;
using CrudProjeto.Models;


namespace CrudProjeto.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)//configurando banco
    {//transforma o banco na linguagem C#
    }

    public DbSet<Usuarios> Usuarios { get; set; }//"criando banco" usuarios e dentro de usuarios
    //cria uma tabela com o nome usuarios que vem do model
}
