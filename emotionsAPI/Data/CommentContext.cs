using emotionsAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace emotionsAPI.Data
{
    public class CommentContext : DbContext
    {

        public CommentContext(DbContextOptions<CommentContext> options) : base(options)
        {
            try
            {
                var dbCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (dbCreator != null)
                {
                    if (!dbCreator.CanConnect())
                    {
                        dbCreator.Create();

                    }
                    if (!dbCreator.HasTables())
                    {
                        dbCreator.CreateTables();
                    }
                }
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.Message);
            }
        }

        public DbSet<Comment> Comment { get; set; }

        
    }
}
