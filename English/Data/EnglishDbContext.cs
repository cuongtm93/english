using English.Data;
using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;

public class EnglishDbContext : DbContext
{
    public EnglishDbContext()
    {

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySQL("Server=localhost;Database=english;Uid=root;Pwd=wcufVYF5kH8c3hr;CharSet=utf8;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }

    //entities
    public DbSet<Lesson> Lessons { get; set; }
}
