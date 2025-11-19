using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Configurations
{
    public class PromptConfiguration : IEntityTypeConfiguration<Prompt>
    {
        public void Configure(EntityTypeBuilder<Prompt> builder)
        {
            builder.ToTable("Prompts");

            builder.HasKey(p => p.Id); //Primary Key olarak tanımlamak için kullanılır.

            builder.Property(p => p.Title).IsRequired().HasMaxLength(250); // zorunlu olsun ve maksimum 250 karaktere izin versin
            builder.Property(p => p.Description).HasMaxLength(2000); //Maksimum 2000 karktere izin versin
            builder.Property(p => p.Content).IsRequired();//Zorunlu olsun
            builder.Property(p => p.Price).HasColumnType("decimal(18,2)").HasDefaultValue(0);//Kolonun tipi sql serverdaki decimal(18,2) tipinde olsun ve eğer kayıt esnasında veri yazılmazsa default olarak 0 değeri ile kaydetsin.


            builder.HasIndex(p => p.Title); //Prompt tablosundaki title kolonunu indexliyoruz(sıralıyoruz). Böylelikle daha hızlı bir şekilde title kolonunda arama yapabiliyoruz.
        }
    }
}
