using CrudProjeto.Data;
using CrudProjeto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrudProjeto.Controllers;

[ApiController]//controler api
[Route("api/usuarios")]//end point entre [] pega o nome do arquivo

public class UsuariosController : ControllerBase
{
    private readonly AppDbContext _context;//cirando variavel que pega o banco de dados

    public UsuariosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> AddUsuarios([FromBody] Usuarios usuario)//criando usuario
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        usuario.DataCadastro = DateTime.Now;//TODOS NOVOS USUARIOS VAO RECEBER DATETIME.NOW

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        return Created("Usuario adicionado", usuario);

    }

    [HttpGet]
    public async Task<IActionResult> GetUsuarios()
    {
        var usuarios = await _context.Usuarios.ToListAsync();
        return Ok(usuarios);
    }

    [HttpGet("{id}")]//get por id
    public async Task<IActionResult> GetUsuariosPorId(int id)
    {
        var usuarios = await _context.Usuarios.FindAsync(id);

        if(usuarios == null)
        {
            return NotFound("(ID) - Usuario nao encontrado");
        }

        return Ok(usuarios);
    }

    [HttpPut("{id}")] //fazer put (modificacao) por id //update
    public async Task<IActionResult> PutUsuarios(int id, [FromBody] UsuarioUpdateDTO usuarioAtualizado)
    {
        var usuarioAtual = _context.Usuarios.Find(id);
        if(usuarioAtual == null)
        {
            return NotFound("(ID) Nao encontrado");
        }

        usuarioAtual.Name = usuarioAtualizado.Name;
        usuarioAtual.Email = usuarioAtualizado.Email;

        if (!string.IsNullOrEmpty(usuarioAtualizado.Senha))
        {
            usuarioAtual.Senha = usuarioAtualizado.Senha;
        }

        try
        {
            _context.Entry(usuarioAtual).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {

        }

        return NoContent();
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUsuarios(int id)//delete por id
    {
        var usuario = _context.Usuarios.Find(id);//pega objeto do id dentro do contexto do BD

        if (usuario is not null)//verifica se o id existe || se nao e nulo
        {
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        } else
        {
            return NotFound("(ID) - Usuario nao encontrado");
        }

            return Ok("Usuario deletado do banco de dados");
    }
}
