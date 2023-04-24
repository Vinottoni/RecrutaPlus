using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace RecrutaPlus.Application.ViewModels
{
    public class OfficeViewModel
    {
        [Display(Name = "Código")]
        public int cargoId { get; set; }

        [Display(Name = "Nome")]
        [Required]
        public string nome { get; set; }

        [Display(Name = "Descrição")]
        [Required]
        public string descricao { get; set; }

        [Display(Name = "Salário")]
        [Required]
        public decimal salario { get; set; }

        //Default
        public DateTime Cadastro { get; set; }
        public string CadastradoPor { get; set; }
        public DateTime Edicao { get; set; }
        public string EditadoPor { get; set; }
        public byte[] VersionStamp { get; set; }
        public Guid GuidStamp { get; set; }
    }
}
