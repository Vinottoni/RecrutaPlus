using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RecrutaPlus.Domain.Validators;

namespace RecrutaPlus.Domain.Entities
{
    public class Employee : Entity
    {
        public int funcionarioId { get; set; }

        public string nome { get; set; }

        public string rg { get; set; }

        public string cpf { get; set; }

        public string email { get; set; }

        public string telefone { get; set; }

        public DateOnly dataNascimento { get; set; }

        public string genero { get; set; }

        public string cep { get; set; }

        public string endereco { get; set; }

        public string bairro { get; set; }

        public string educacao { get; set; }

        public string status { get; set; }


        //Default
        public DateTime Cadastro { get; set; }
        public string CadastradoPor { get; set; }
        public DateTime Edicao { get; set; }
        public string EditadoPor { get; set; }
        public long VersionStamp { get; set; } //public byte[]? VersionStamp { get; set; }
        [NotMapped]
        public Guid GuidStamp { get; set; }

        public virtual IList<Office> Offices { get; set; }

        public override bool IsValid()  //Aqui ainda tem refências para criar em Services
        {
            ValidationResult = new EmployeeValidator().Validate(this);
            return ValidationResult.IsValid;
        }

    }
}
