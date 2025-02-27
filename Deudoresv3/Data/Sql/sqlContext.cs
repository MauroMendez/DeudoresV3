﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Deudoresv3.Data.Sql
{
    public partial class sqlContext : DbContext
    {
        public sqlContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=217.160.15.235;Initial Catalog=InterERPv3TestETL;User ID=sa;Password=Root.inter2020!;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        }


        public sqlContext(DbContextOptions<sqlContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Rdeudores> Rdeudores { get; set; }
        public virtual DbSet<Rdeudoresdiario> Rdeudoresdiario { get; set; }
        public virtual DbSet<ViewDeudores> ViewDeudores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rdeudores>(entity =>
            {
                entity.HasKey(e => e.Rd)
                    .HasName("PK__RDeudore__321537CC2EBDB948");

                entity.ToTable("RDeudores");

                entity.Property(e => e.Rd).HasColumnName("RD");

                entity.Property(e => e.Rdfecreg)
                    .HasColumnType("datetime")
                    .HasColumnName("RDFecreg");

                entity.Property(e => e.Rdtimefin)
                    .HasColumnType("datetime")
                    .HasColumnName("RDTimefin");

                entity.Property(e => e.Rdtimereg)
                    .HasColumnType("datetime")
                    .HasColumnName("RDTimereg");
            });

            modelBuilder.Entity<Rdeudoresdiario>(entity =>
            {
                entity.HasKey(e => e.Rdid)
                    .HasName("PK__RDeudore__458CE898C8128093");

                entity.ToTable("RDeudoresdiario");

                entity.Property(e => e.Rdid).HasColumnName("RDId");

                entity.Property(e => e.Beca)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Carrera)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Div)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Estatus)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Rdaluid).HasColumnName("RDAluid");

                entity.Property(e => e.Rdamaterno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("RDAMaterno");

                entity.Property(e => e.Rdapaterno)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("RDAPaterno");

                entity.Property(e => e.Rdbcid).HasColumnName("RDBcid");

                entity.Property(e => e.Rdbedesc)
                    .HasMaxLength(70)
                    .IsUnicode(false)
                    .HasColumnName("RDBedesc");

                entity.Property(e => e.Rdbeinscrip)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("RDBeinscrip");

                entity.Property(e => e.Rdbeparcialidades)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("RDBEParcialidades");

                entity.Property(e => e.Rdcarid).HasColumnName("RDCARID");

                entity.Property(e => e.Rdconceptosdesc)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("RDConceptosdesc");

                entity.Property(e => e.Rdconvenio)
                    .HasColumnType("datetime")
                    .HasColumnName("RDConvenio");

                entity.Property(e => e.Rdconvfin)
                    .HasColumnType("datetime")
                    .HasColumnName("RDConvfin");

                entity.Property(e => e.Rdconvinicio)
                    .HasColumnType("datetime")
                    .HasColumnName("RDConvinicio");

                entity.Property(e => e.Rddivision).HasColumnName("RDDivision");

                entity.Property(e => e.Rdemail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("RDEmail");

                entity.Property(e => e.RdemailT)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("RDEmailT");

                entity.Property(e => e.Rdestatus).HasColumnName("RDEstatus");

                entity.Property(e => e.Rdgrado)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("RDGrado");

                entity.Property(e => e.Rdgrupo)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("RDGrupo");

                entity.Property(e => e.RdnoConceptos).HasColumnName("RDNoConceptos");

                entity.Property(e => e.Rdnocontrol)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("RDNOControl");

                entity.Property(e => e.Rdnombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("RDNombre");

                entity.Property(e => e.Rdpedindiente)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("RDPedindiente");

                entity.Property(e => e.Rdtelefono)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("RDTelefono");

                entity.Property(e => e.Rdtutor)
                    .HasMaxLength(120)
                    .IsUnicode(false)
                    .HasColumnName("RDTutor");
            });

            modelBuilder.Entity<ViewDeudores>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ViewDeudores");

                entity.Property(e => e.AlApm)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("AL_APM");

                entity.Property(e => e.AlApp)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("AL_APP");

                entity.Property(e => e.AlCorreoInst)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Al_CorreoInst");

                entity.Property(e => e.AlId).HasColumnName("AL_ID");

                entity.Property(e => e.AlMatricula)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("Al_Matricula");

                entity.Property(e => e.AlNombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("AL_Nombre");

                entity.Property(e => e.AlSemestre).HasColumnName("AL_Semestre");

                entity.Property(e => e.AlStatusActual).HasColumnName("AL_StatusActual");

                entity.Property(e => e.CarreraClave).HasMaxLength(50);

                entity.Property(e => e.DcpcDescripcion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("DCPC_Descripcion");

                entity.Property(e => e.GaCorreoAlternativo)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("GA_CorreoAlternativo")
                    .IsFixedLength();

                entity.Property(e => e.GaTelefonoTutor)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("GA_TelefonoTutor")
                    .IsFixedLength();

                entity.Property(e => e.Idcarrera).HasColumnName("IDCarrera");

                entity.Property(e => e.NivelId).HasColumnName("Nivel_ID");

                entity.Property(e => e.NivelNombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Nivel_Nombre")
                    .IsFixedLength();

                entity.Property(e => e.Pendiente).HasColumnType("numeric(38, 2)");

                entity.Property(e => e.SlDescripcion)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("SL_Descripcion");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}