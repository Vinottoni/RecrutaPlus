using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using RecrutaPlus.Domain.Entities;
using RecrutaPlus.Infra.Data.Mappings.DataTypes;

namespace RecrutaPlus.Infra.Data.Mappings.MySQL
{
    public class OfficeMap : IEntityTypeConfiguration<Office>
    {
        public void Configure(EntityTypeBuilder<Office> builder)
        {
            //Table
            builder.ToTable("cargos");

            //PK
            builder.HasKey(k => k.CargoId);

            //Property
            builder.Property(e => e.CargoId)
                .HasColumnName("cargoId")
                .HasColumnType(MySQLDataTypes.INT());

            builder.Property(e => e.Nome)
                .HasColumnName("nome")
                .IsRequired()
                .HasColumnType(MySQLDataTypes.VARCHAR(255));

            builder.Property(e => e.Descricao)
                .HasColumnName("descricao")
                .HasColumnType(MySQLDataTypes.TEXT());

            builder.Property(e => e.Salario)
                .HasColumnName("salario")
                .IsRequired()
                .HasColumnType(MySQLDataTypes.DECIMAL());

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
            //builder.HasIndex(d => d.Descricao);

        }
    }
}
