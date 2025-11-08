using System.ComponentModel.DataAnnotations;

namespace CrudProjeto.Models;

public class UsuarioUpdateDTO
{
    [Required]
    public int Id { get; set; }

    [Required(ErrorMessage = "Nome é um campo obrigatório")]
    [MaxLength(50)]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Email é um campo obrigatório")]
    [MaxLength(100)]
    public string? Email { get; set; }

    [MaxLength(50)]
    public string? Senha { get; set; }

}
