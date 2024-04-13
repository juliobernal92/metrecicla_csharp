using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MetReciclaWebFinalOK.Models;

namespace MetReciclaWebFinalOK.Models;

public partial class MetrecicladbContext : DbContext
{
    public MetrecicladbContext()
    {
    }

    public MetrecicladbContext(DbContextOptions<MetrecicladbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chatarra> Chatarras { get; set; }

    public virtual DbSet<DetallesCompra> DetallesCompras { get; set; }

    public virtual DbSet<DetallesVentum> DetallesVenta { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<LocalVenta> LocalVenta { get; set; }

    public virtual DbSet<PrecioVenta> PrecioVenta { get; set; }

    public virtual DbSet<TicketCompra> TicketCompras { get; set; }

    public virtual DbSet<TicketVenta> TicketVenta { get; set; }

    public virtual DbSet<TotalCompra> TotalCompras { get; set; }

    public virtual DbSet<TotalVenta> TotalVenta { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Vendedor> Vendedores { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=DESKTOP-V71J5VC\\SQLEXPRESS;Database=metrecicladb;User Id=sa;Password=123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chatarra>(entity =>
        {
            entity.HasKey(e => e.Idchatarra).HasName("PK__Chatarra__D638065F86B9BD46");

            entity.ToTable("Chatarra");

            entity.Property(e => e.Idchatarra).HasColumnName("idchatarra");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Preciocompra).HasColumnName("preciocompra");
        });

        modelBuilder.Entity<DetallesCompra>(entity =>
        {
            entity.HasKey(e => e.Iddetallecompra).HasName("PK__Detalles__B52B00194EA2BF9D");

            entity.ToTable("DetallesCompra");

            entity.Property(e => e.Iddetallecompra).HasColumnName("iddetallecompra");
            entity.Property(e => e.Cantidad)
                .HasColumnType("decimal(10, 1)")
                .HasColumnName("cantidad");
            entity.Property(e => e.Idchatarra).HasColumnName("idchatarra");
            entity.Property(e => e.Idticketcompra).HasColumnName("idticketcompra");
            entity.Property(e => e.Subtotal).HasColumnName("subtotal");

            entity.HasOne(d => d.IdchatarraNavigation).WithMany(p => p.DetallesCompras)
                .HasForeignKey(d => d.Idchatarra)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__DetallesC__idcha__534D60F1");

            entity.HasOne(d => d.IdticketcompraNavigation).WithMany(p => p.DetallesCompras)
                .HasForeignKey(d => d.Idticketcompra)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__DetallesC__idtic__5441852A");
        });

        modelBuilder.Entity<DetallesVentum>(entity =>
        {
            entity.HasKey(e => e.Iddetallesventa).HasName("PK__Detalles__AB48070A01960696");

            entity.Property(e => e.Iddetallesventa).HasColumnName("iddetallesventa");
            entity.Property(e => e.Cantidad)
                .HasColumnType("decimal(10, 1)")
                .HasColumnName("cantidad");
            entity.Property(e => e.Idprecioventa).HasColumnName("idprecioventa");
            entity.Property(e => e.Idticketventa).HasColumnName("idticketventa");
            entity.Property(e => e.Subtotal).HasColumnName("subtotal");

            entity.HasOne(d => d.IdprecioventaNavigation).WithMany(p => p.DetallesVenta)
                .HasForeignKey(d => d.Idprecioventa)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__DetallesV__idpre__5CD6CB2B");

            entity.HasOne(d => d.IdticketventaNavigation).WithMany(p => p.DetallesVenta)
                .HasForeignKey(d => d.Idticketventa)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__DetallesV__idtic__5DCAEF64");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Cedula).HasName("PK__Empleado__415B7BE4C2BA102E");

            entity.Property(e => e.Cedula)
                .ValueGeneratedNever()
                .HasColumnName("cedula");
            entity.Property(e => e.Apellido)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Direccion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<LocalVenta>(entity =>
        {
            entity.HasKey(e => e.Idlocalventa).HasName("PK__LocalVen__C077D90FB6C569C1");

            entity.Property(e => e.Idlocalventa).HasColumnName("idlocalventa");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<PrecioVenta>(entity =>
        {
            entity.HasKey(e => e.Idprecioventa).HasName("PK__PrecioVe__044B0396F94D9B3F");

            entity.Property(e => e.Idprecioventa).HasColumnName("idprecioventa");
            entity.Property(e => e.Idchatarra).HasColumnName("idchatarra");
            entity.Property(e => e.Idlocalventa).HasColumnName("idlocalventa");
            entity.Property(e => e.Precioventa).HasColumnName("precioventa");

            entity.HasOne(d => d.IdchatarraNavigation).WithMany(p => p.PrecioVenta)
                .HasForeignKey(d => d.Idchatarra)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__PrecioVen__idcha__49C3F6B7");

            entity.HasOne(d => d.IdlocalventaNavigation).WithMany(p => p.PrecioVenta)
                .HasForeignKey(d => d.Idlocalventa)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__PrecioVen__idloc__4AB81AF0");
        });

        modelBuilder.Entity<TicketCompra>(entity =>
        {
            entity.HasKey(e => e.Idticketcompra).HasName("PK__TicketCo__0B4E4D3C45959182");

            entity.ToTable("TicketCompra");

            entity.Property(e => e.Idticketcompra).HasColumnName("idticketcompra");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.Idusuario).HasColumnName("idusuario");
            entity.Property(e => e.Idvendedor).HasColumnName("idvendedor");

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.TicketCompras)
                .HasForeignKey(d => d.Idusuario)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__TicketCom__idusu__5070F446");

            entity.HasOne(d => d.IdvendedorNavigation).WithMany(p => p.TicketCompras)
                .HasForeignKey(d => d.Idvendedor)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__TicketCom__idven__4F7CD00D");
        });

        modelBuilder.Entity<TicketVenta>(entity =>
        {
            entity.HasKey(e => e.Idticketventa).HasName("PK__TicketVe__08BB9424313EB259");

            entity.Property(e => e.Idticketventa).HasColumnName("idticketventa");
            entity.Property(e => e.Fecha)
                .HasColumnType("datetime")
                .HasColumnName("fecha");
            entity.Property(e => e.Idusuario).HasColumnName("idusuario");

            entity.HasOne(d => d.IdusuarioNavigation).WithMany(p => p.TicketVenta)
                .HasForeignKey(d => d.Idusuario)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__TicketVen__idusu__59FA5E80");
        });

        modelBuilder.Entity<TotalCompra>(entity =>
        {
            entity.HasKey(e => e.Idtotalcompra).HasName("PK__TotalCom__DD75A605332BB097");

            entity.ToTable("TotalCompra");

            entity.Property(e => e.Idtotalcompra).HasColumnName("idtotalcompra");
            entity.Property(e => e.Idticketcompra).HasColumnName("idticketcompra");
            entity.Property(e => e.Total).HasColumnName("total");

            entity.HasOne(d => d.IdticketcompraNavigation).WithMany(p => p.TotalCompras)
                .HasForeignKey(d => d.Idticketcompra)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__TotalComp__idtic__571DF1D5");
        });

        modelBuilder.Entity<TotalVenta>(entity =>
        {
            entity.HasKey(e => e.Idtotalventa).HasName("PK__TotalVen__4A4094DF162958F4");

            entity.Property(e => e.Idtotalventa).HasColumnName("idtotalventa");
            entity.Property(e => e.Idticketventa).HasColumnName("idticketventa");
            entity.Property(e => e.Totalventa).HasColumnName("totalventa");

            entity.HasOne(d => d.IdticketventaNavigation).WithMany(p => p.TotalVenta)
                .HasForeignKey(d => d.Idticketventa)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__TotalVent__idtic__60A75C0F");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Idusuario).HasName("PK__Usuarios__080A97432E940204");

            entity.Property(e => e.Idusuario).HasColumnName("idusuario");
            entity.Property(e => e.Cedula).HasColumnName("cedula");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("contraseña");

            entity.HasOne(d => d.CedulaNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.Cedula)
                .HasConstraintName("FK__Usuarios__cedula__4316F928");
        });

        modelBuilder.Entity<Vendedor>(entity =>
        {
            entity.HasKey(e => e.Idvendedor).HasName("PK__Vendedor__8F89222D79A50214");

            entity.Property(e => e.Idvendedor).HasColumnName("idvendedor");
            entity.Property(e => e.Direccion)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public DbSet<MetReciclaWebFinalOK.Models.LoginModel>? LoginModel { get; set; }

    public DbSet<MetReciclaWebFinalOK.Models.ChatarraVMRest>? ChatarraVMRest { get; set; }

    public DbSet<MetReciclaWebFinalOK.Models.VendedorViewModel>? VendedorViewModel { get; set; }
}
