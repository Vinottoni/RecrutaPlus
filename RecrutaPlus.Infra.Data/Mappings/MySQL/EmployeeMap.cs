using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecrutaPlus.Domain.Entities;
using RecrutaPlus.Infra.Data.Mappings.DataTypes;

namespace RecrutaPlus.Infra.Data.Mappings.MySQL
{
    public class EmployeeMap : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            //Table
            builder.ToTable("funcionarios");

            //PK
            builder.HasKey(k => k.FuncionarioId); //ajusta aqui tambem

            //Property
            builder.Property(e => e.FuncionarioId)
                .HasColumnName("funcionarioId")
                .HasColumnType(MySQLDataTypes.INT());

            builder.Property(e => e.Nome)
                .HasColumnName("nome")
                .IsRequired()
                .HasColumnType(MySQLDataTypes.VARCHAR(255));

            builder.Property(e => e.CPF)
                .HasColumnName("cpf")
                .IsRequired()
                .HasColumnType(MySQLDataTypes.VARCHAR(11));

            builder.Property(e => e.Email)
                .HasColumnName("email")
                .IsRequired()
                .HasColumnType(MySQLDataTypes.VARCHAR(255));

            builder.Property(e => e.Telefone)
                .HasColumnName("telefone")
                .IsRequired()
                .HasColumnType(MySQLDataTypes.VARCHAR(20));

            builder.Property(e => e.DataNascimento)
                .HasColumnName("data_nascimento")
                .IsRequired()
                .HasColumnType(MySQLDataTypes.DATE());

            builder.Property(e => e.Endereco)
                .HasColumnName("endereco")
                .IsRequired()
                .HasColumnType(MySQLDataTypes.VARCHAR(255));

            builder.Property(e => e.RG)
                .HasColumnName("rg")
                .IsRequired()
                .HasColumnType(MySQLDataTypes.VARCHAR(45));

            builder.Property(e => e.Genero)
                .HasColumnName("genero")
                .IsRequired()
                .HasColumnType(MySQLDataTypes.CHAR(2));

            builder.Property(e => e.CEP)
                .HasColumnName("cep")
                .IsRequired()
                .HasColumnType(MySQLDataTypes.VARCHAR(20));

            builder.Property(e => e.Educacao)
                .HasColumnName("educacao")
                .IsRequired()
                .HasColumnType(MySQLDataTypes.VARCHAR(255));

            builder.Property(e => e.Status)
                .HasColumnName("status")
                .IsRequired()
                .HasColumnType(MySQLDataTypes.VARCHAR(255));

            builder.Property(e => e.Bairro)
                .HasColumnName("bairro")
                .IsRequired()
                .HasColumnType(MySQLDataTypes.VARCHAR(255));

            builder.Property(e => e.Cadastro)
                .HasColumnName("Cadastro")
                .HasColumnType(MySQLDataTypes.DATETIME());

            builder.Property(e => e.CadastradoPor)
                .HasColumnName("CadastradoPor")
                .HasColumnType(MySQLDataTypes.VARCHAR(50));

            builder.Property(e => e.Edicao)
                .HasColumnName("Edicao")
                .HasColumnType(MySQLDataTypes.DATETIME());

            builder.Property(e => e.EditadoPor)
                .HasColumnName("EditadoPor")
                .HasColumnType(MySQLDataTypes.VARCHAR(50));

            builder.Property(e => e.VersionStamp)
                .HasColumnName("timeStamp")
                .IsRequired()
                .HasColumnType(MySQLDataTypes.DECIMAL());

            builder.Property(e => e.GuidStamp)
                .HasColumnName("GuidStamp")
                .IsRequired()
                .HasColumnType(MySQLDataTypes.CHAR(36));

            //Ignore
            builder.Ignore(i => i.ValidationResult);

            //AlternateKey
            builder.HasAlternateKey(a => a.GuidStamp);

            //Index
            builder.HasIndex(d => d.CargoId);

            //FK
            builder.HasOne(d => d.Office)
                .WithMany(p => p.Employees)
                .HasForeignKey(d => d.CargoId);
        }
    }
}
