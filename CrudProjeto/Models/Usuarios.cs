using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;//tabela MySQL -> usuarios

namespace CrudProjeto.Models;

[Table("usuarios")]
public class Usuarios
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Nome é um campo obrigatório")]
    [MaxLength(50, ErrorMessage = "Nome tem no maximo 50 caracteres")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Email é um campo obrigatório")]
    [MaxLength(100, ErrorMessage = "Email tem no maximo 100 caracteres")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "Senha é um campo obrigatório")]
    [MaxLength(50, ErrorMessage = "Senha tem no maximo 50 caracteres")]
    public string? Senha { get; set; }
    public DateTime DataCadastro { get; set; }
}
