using System;
namespace Inmobiliaria.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Inmobiliaria.Models
{
    [Key]
    [Display(Name = "Código")]
    public int Id { get; set; }
    [Required]
    public string Nombre { get; set; }
    [Required]
    public string Apellido { get; set; }
    [Required, EmailAddress]
    public string Email { get; set; }
    [Required, DataType(DataType.Password)]
    public string Clave { get; set; }
    public string Avatar { get; set; }
    [NotMapped]//Para EF
    public IFormFile AvatarFile { get; set; }
    //[NotMapped]//Para EF
    //public byte[] AvatarFileContent { get; set; }
    //[NotMapped]//Para EF
    //public string AvatarFileName { get; set; }
    public int Rol { get; set; }
    [NotMapped]//Para EF
    public string RolNombre => Rol > 0 ? ((enRoles)Rol).ToString() : "";


}