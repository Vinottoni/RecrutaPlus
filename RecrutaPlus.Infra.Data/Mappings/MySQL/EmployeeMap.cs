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
            builder.HasKey(k => k.funcionarioId);

            //Property
            builder.Property(e => e.funcionarioId)
                .HasColumnName("funcionarioId")
                .HasColumnType(MySQLDataTypes.INT());

            builder.Property(e => e.nome)
                .HasColumnName("nome")
                .IsRequired()
                .HasColumnType(MySQLDataTypes.VARCHAR(255));

            builder.Property(e => e.cpf)
                .HasColumnName("cpf")
                .IsRequired()
                .HasColumnType(MySQLDataTypes.VARCHAR(11));

            builder.Property(e => e.email)
                .HasColumnName("email")
                .IsRequired()
                .HasColumnType(MySQLDataTypes.VARCHAR(255));

            builder.Property(e => e.telefone)
                .HasColumnName("telefone")
                .IsRequired()
                .HasColumnType(MySQLDataTypes.VARCHAR(20));

            builder.Property(e => e.dataNascimento)
                .HasColumnName("data_nascimento")
                .IsRequired()
                .HasColumnType(MySQLDataTypes.DATE());

            builder.Property(e => e.endereco)
                .HasColumnName("endereco")
                .IsRequired()
                .HasColumnType(MySQLDataTypes.VARCHAR(255));

            builder.Property(e => e.rg)
                .HasColumnName("rg")
                .IsRequired()
                .HasColumnType(MySQLDataTypes.VARCHAR(45));

            builder.Property(e => e.genero)
                .HasColumnName("genero")
                .IsRequired()
                .HasColumnType(MySQLDataTypes.CHAR(2));

            builder.Property(e => e.cep)
                .HasColumnName("cep")
                .IsRequired()
                .HasColumnType(MySQLDataTypes.VARCHAR(20));

            builder.Property(e => e.educacao)
                .HasColumnName("educacao")
                .IsRequired()
                .HasColumnType(MySQLDataTypes.VARCHAR(255));

            builder.Property(e => e.status)
                .HasColumnName("status")
                .IsRequired()
                .HasColumnType(MySQLDataTypes.VARCHAR(255));

            builder.Property(e => e.bairro)
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
            builder.HasIndex(d => d.cargoId);

            //FK
            builder.HasOne(d => d.Office)
                .WithMany(p => p.Employees)
                .HasForeignKey(d => d.cargoId);
        }
    }
}
