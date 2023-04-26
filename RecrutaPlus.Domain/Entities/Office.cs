using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RecrutaPlus.Domain.Validators;

namespace RecrutaPlus.Domain.Entities
{
    public class Office : Entity
    {
        public int cargoId { get; set; }

        public string nome { get; set; }

        public string descricao { get; set; }

        public decimal salario { get; set; }


        //Default
        public DateTime Cadastro { get; set; }
        public string CadastradoPor { get; set; }
        public DateTime Edicao { get; set; }
        public string EditadoPor { get; set; }
        public long VersionStamp { get; set; } //public byte[]? VersionStamp { get; set; }
        [NotMapped]
        public Guid GuidStamp { get; set; }

        public override bool IsValid() //Aqui ainda tem refências para criar em Services
        {
            ValidationResult = new OfficeValidator().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
