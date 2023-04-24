using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace RecrutaPlus.Application.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "Código")]
        public int usuarioId { get; set; }

        [Display(Name = "FuncionarioId")]
        public int funcionarioId { get; set; }

        [Display(Name = "Nome do Usuário")]
        [Required]
        public string username { get; set; }

        [Display(Name = "Senha")]
        [Required]
        public string password { get; set; }

        //Default
        public DateTime Cadastro { get; set; }
        public string CadastradoPor { get; set; }
        public DateTime Edicao { get; set; }
        public string EditadoPor { get; set; }
        public byte[] VersionStamp { get; set; }
        public Guid GuidStamp { get; set; }
    }
}
