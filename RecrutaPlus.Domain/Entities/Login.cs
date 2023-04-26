using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RecrutaPlus.Domain.Validators;

namespace RecrutaPlus.Domain.Entities
{
    public class Login : Entity
    {
        public int usuarioId { get; set; }

        public string username { get; set; }

        public string password { get; set; }


        //Default
        public DateTime Cadastro { get; set; }
        public string CadastradoPor { get; set; }
        public DateTime Edicao { get; set; }
        public string EditadoPor { get; set; }
        public long VersionStamp { get; set; } //public byte[]? VersionStamp { get; set; }
        [NotMapped]
        public Guid GuidStamp { get; set; }

        public virtual IList<Employee> Employees { get; set; }

        public override bool IsValid() //Aqui ainda tem refências para criar em Services
        {
            ValidationResult = new LoginValidator().Validate(this);
            return ValidationResult.IsValid;
        }

    }
}
