using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace RecrutaPlus.Application.ViewModels
{
    public class EmployeeViewModel
    {
        [Display(Name = "Código")]
        public int funcionarioId { get; set; }

        [Display(Name = "CargoId")]
        public int cargoId { get; set; }

        [Display(Name = "Nome")]
        [Required]
        public string nome { get; set; }

        [Display(Name = "RG")]
        [Required]
        public string rg { get; set; }

        [Display(Name = "CPF")]
        [Required]
        public string cpf { get; set; }

        [Display(Name = "Email")]
        [Required]
        public string email { get; set; }

        [Display(Name = "Telefone")]
        [Required]
        public string telefone { get; set; }

        [Display(Name = "Data de Nascimento")]
        [Required]
        public DateOnly dataNascimento { get; set; }

        [Display(Name = "Gênero")]
        [Required]
        public string genero { get; set; }

        [Display(Name = "CEP")]
        [Required]
        public string cep { get; set; }

        [Display(Name = "Endereço")]
        [Required]
        public string endereco { get; set; }

        [Display(Name = "Bairro")]
        [Required]
        public string bairro { get; set; }

        [Display(Name = "Educação")]
        [Required]
        public string educacao { get; set; }

        [Display(Name = "Status")]
        [Required]
        public string status { get; set; }

        //Default
        public DateTime Cadastro { get; set; }
        public string CadastradoPor { get; set; }
        public DateTime Edicao { get; set; }
        public string EditadoPor { get; set; }
        public byte[] VersionStamp { get; set; }
        public Guid GuidStamp { get; set; }
    }
}
