using Library.Domains;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace Library.Context
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> dbContextOptions) : base(dbContextOptions) { }

        public DbSet<Libro> Libros { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        // REEMPLAZADO POR CONFIG EN PROGRAM.CS
    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //    {
    //        optionsBuilder.UseSqlServer
    //("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Library;Integrated Security=True;");
    //    }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data
            var autorId = new Guid("00000000-0000-0000-0000-000000000001");
            var generoId = 1; // Id identity

            //modelBuilder.Entity<Pais>().HasData(
            //    new Pais { Id = 1, Nombre = "Argentina" }
            //);

            modelBuilder.Entity<Autor>().HasData(
                new Autor
                {
                    Id = autorId,
                    Nombre = "Mateo",
                    FechaNacimiento = new DateTime(1986, 7, 20),
                    PaisId = "032",
                    Email = "mateo@mail.com"
                }
            );
            modelBuilder.Entity<Genero>().HasData(
                 new Genero { Id = generoId, Nombre = "Ficción" }
            );
            modelBuilder.Entity<Libro>().HasData(
                 new Libro
                 {
                     Id = new Guid("00000000-0000-0000-0000-000000000002"),
                     Titulo = "El Gran Libro",
                     ISBN = "123-4567890123",
                     FechaPublicacion = new DateTime(2020, 1, 1),
                     AutorId = autorId,
                     GeneroId = generoId
                 }
            );
            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = new Guid("00000000-0000-0000-0000-000000000001"),
                    Nombre = "MateoBuraschi",
                    Password = "123456",
                    Rol = "Admin",
                    Email = "mateo@mail.com"
                }
            );

            // Identity
            modelBuilder.Entity<Genero>()
                .Property(genero => genero.Id)
                .ValueGeneratedOnAdd();

            //modelBuilder.Entity<Pais>()
            //    .Property(pais => pais.Id)
            //    .ValueGeneratedOnAdd();

            // Configuración de la relación uno a muchos entre Autor y Libro
            modelBuilder.Entity<Autor>()
                .HasMany(autor => autor.Libros)
                .WithOne(libro => libro.Autor)
                .HasForeignKey(libro => libro.AutorId);

            // Configuración de la relación uno a muchos entre Genero y Libro
            modelBuilder.Entity<Genero>()
                .HasMany(genero => genero.Libros)
                .WithOne(libro => libro.Genero)
                .HasForeignKey(libro => libro.GeneroId);

            // Configuración de la relación uno a muchos entre Pais y Autor
            //modelBuilder.Entity<Pais>()
            //    .HasMany(pais => pais.Autores)
            //    .WithOne(autor => autor.Pais)
            //    .HasForeignKey(autor => autor.PaisId);

            // Ignorar la propiedad Pais en la entidad Autor
            modelBuilder.Entity<Autor>()
                .Ignore(a => a.Pais);

        }
    }
}
