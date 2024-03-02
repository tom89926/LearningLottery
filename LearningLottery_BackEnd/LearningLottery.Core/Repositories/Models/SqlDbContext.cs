using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Text;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LearningLottery.Core.Repositories.Models;

public partial class SqlDbContext : DbContext
{
    public SqlDbContext(DbContextOptions<SqlDbContext> context) : base(context)
    {
 
    }

    public DbSet<User> User { get; set; } = null!;

    public override int SaveChanges()
    { 
        try {
            return base.SaveChanges();
        }
        catch (DbEntityValidationException vex) {
            var exception = HandleDbEntityValidationException(vex);
            Console.WriteLine("Error: " + exception);
            return -1;
            //throw exception;
        }
        catch (DbUpdateException dbu) {
            var exception = HandleDbUpdateException(dbu);
            Console.WriteLine("Error: " + exception);
            return -1;
            //throw exception;
        }
        catch (Exception exception) {
            Console.WriteLine("UnKnown Exception: " + exception);
        }

        return 0;
    }

    private Exception HandleDbUpdateException(DbUpdateException dbu)
    {
        var builder = new StringBuilder("A DbUpdateException was caught while saving changes. ");

        try
        {
            foreach (var result in dbu.Entries)
            {
                builder.AppendFormat("Type: {0} was part of the problem. ", result.Entity.GetType().Name);
            }
        }
        catch (Exception e)
        {
            builder.Append("Error parsing DbUpdateException: " + e.ToString());
        }

        string message = builder.ToString();
        return new Exception(message, dbu);
    }

    private Exception HandleDbEntityValidationException(DbEntityValidationException dbv)
    {
        var builder = new StringBuilder("A DbEntityValidationException was caught while saving changes. ");
        foreach (var eve in dbv.EntityValidationErrors)
        {
            Debug.WriteLine(@"Entity of type ""{0}"" in state ""{1}"" 
                   has the following validation errors:",
                eve.Entry.Entity.GetType().Name, 
                eve.Entry.State);
            foreach (var ve in eve.ValidationErrors)
            {
                Debug.WriteLine(@"- Property: ""{0}"", Error: ""{1}""",
                    ve.PropertyName, ve.ErrorMessage);
            }
        }
        string message = builder.ToString();
        return new Exception(message, dbv);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // modelBuilder.Entity<Positions>().HasKey(vf=> new {vf.Location, vf.Position});
    }
    
}