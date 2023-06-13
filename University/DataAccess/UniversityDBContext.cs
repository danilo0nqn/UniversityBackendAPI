using Microsoft.EntityFrameworkCore;
using University.Models.DataModels;

namespace University.DataAccess
{
    public class UniversityDBContext: DbContext
    {
        private readonly ILoggerFactory _loggerFactory;
        public UniversityDBContext(DbContextOptions<UniversityDBContext> options, ILoggerFactory loggerFactory) : base(options) 
        {
            _loggerFactory = loggerFactory;
        }

        //Add DbSets (Tables of our Data base)
        public DbSet<User>? Users { get; set; }
        public DbSet<Student>? Students { get; set; }
        public DbSet<Course>? Courses { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<Chapter>? Chapter { get; set; }

        //Configuring DBContext to add Loggers to Entity Framework

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var logger = _loggerFactory.CreateLogger<UniversityDBContext>();
            //In this way we can log every time we query for something, making the logs to long.
            //optionsBuilder.LogTo(d => logger.Log(LogLevel.Information, d, new[] { DbLoggerCategory.Database.Name }));
            //optionsBuilder.EnableSensitiveDataLogging();

            //We can modify so it's only filtered to LogLevel.Warning instead
            optionsBuilder.LogTo(d => logger.Log(LogLevel.Warning, d, new[] { DbLoggerCategory.Database.Name }), LogLevel.Warning)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();

            optionsBuilder.LogTo(d => logger.Log(LogLevel.Error, d, new[] { DbLoggerCategory.Database.Name }), LogLevel.Error)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();

            optionsBuilder.LogTo(d => logger.Log(LogLevel.Critical, d, new[] { DbLoggerCategory.Database.Name }), LogLevel.Critical)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
        }
    }
}
