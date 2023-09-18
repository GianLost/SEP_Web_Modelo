using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using SEP_Web.Keys;

namespace SEP_Web.Models
{
    [Table("Users")]
    public class Users
    {
        [Key, Required(ErrorMessage = "O campo id é obrigatório!")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o MASP!")]
        [Range(0, 999999, ErrorMessage = "Informe um MASP válido")]
        public int? Masp { get; set; }

        [Required(ErrorMessage = "Informe seu nome completo!")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Informe um nome de login!")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Informe uma senha!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Informe um e-mail!")]
        [EmailAddress(ErrorMessage = "O e-mail informado é inválido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Informe um telefone!")]
        [Phone(ErrorMessage = "O número de tefone é inválido!")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Informe seu cargo!")]
        public string PublicOffice {get; set; }

        [Required(ErrorMessage = "Informe o tipo de usuário!")]
        public UserTypeEnum? UserType {get; set; }

        public bool ValidPassword(string pass)
        {
            return Password == pass;
        }
    }
}