using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace RecrutaPlus.Application.ViewModels
{
    public class EmployeeFilter
    {
        public int? funcionarioId { get; set; }
        public int? cargoId { get; set; }

        public string nome { get; set; }

        public string rg { get; set; }

        public string cpf { get; set; }

        public string email { get; set; }

        public string telefone { get; set; }

        public DateOnly? dataNascimento { get; set; }

        public string genero { get; set; }

        public string cep { get; set; }

        public string endereco { get; set; }

        public string bairro { get; set; }

        public string educacao { get; set; }

        public string status { get; set; }

        //Default
        public DateTime? Cadastro { get; set; }
        public string CadastradoPor { get; set; }
        public DateTime? Edicao { get; set; }
        public string EditadoPor { get; set; }
        public long? VersionStamp { get; set; } //public byte[]? VersionStamp { get; set; }
        public Guid? GuidStamp { get; set; }


        public virtual OfficeFilter OfficeFilter { get; set; }
        public virtual LoginFilter LoginFilter { get; set; }
        public virtual IList<OfficeFilter> OfficeViewModels { get; set; }

    }
}
