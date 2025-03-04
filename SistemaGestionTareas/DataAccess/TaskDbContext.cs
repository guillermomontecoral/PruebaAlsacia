using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options) : base(options)
        {
        }
        public DbSet<Domain.Entities.Task> Tasks { get; set; }
        public DbSet<Domain.Entities.User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Entities.Task>().HasKey(x => x.Id);
            modelBuilder.Entity<Domain.Entities.User>().HasKey(x => x.Id);

            modelBuilder.Entity<Domain.Entities.User>().HasData(
                new Domain.Entities.User
                {
                    Id = 1,
                    Email = "prueba@prueba.com",
                    Password = "Abcd1234_"
                });

            modelBuilder.Entity<Domain.Entities.Task>().HasData(
                new Domain.Entities.Task
                {
                    Id = 1,
                    Title = "Prueba 1",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut in lectus id purus finibus euismod id eu arcu. Vivamus sit amet metus eget ipsum interdum eleifend quis sed leo. In purus magna, luctus eu urna et, feugiat gravida risus. Aenean eleifend magna ante, in elementum nunc laoreet sed. Aliquam posuere quis ex vel tempor.",
                    ExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                    Status = Domain.Enums.TaskStatus.Completed,
                    UserId = 1
                },
                new Domain.Entities.Task
                {
                    Id = 2,
                    Title = "Prueba 2",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut in lectus id purus finibus euismod id eu arcu. Vivamus sit amet metus eget ipsum interdum eleifend quis sed leo. In purus magna, luctus eu urna et, feugiat gravida risus. Aenean eleifend magna ante, in elementum nunc laoreet sed. Aliquam posuere quis ex vel tempor.",
                    ExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddDays(18)),
                    Status = Domain.Enums.TaskStatus.Created,
                    UserId = 1
                },
                new Domain.Entities.Task
                {
                    Id = 3,
                    Title = "Prueba 3",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut in lectus id purus finibus euismod id eu arcu. Vivamus sit amet metus eget ipsum interdum eleifend quis sed leo. In purus magna, luctus eu urna et, feugiat gravida risus. Aenean eleifend magna ante, in elementum nunc laoreet sed. Aliquam posuere quis ex vel tempor.",
                    ExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(1)),
                    Status = Domain.Enums.TaskStatus.Created,
                    UserId = 1
                },
                new Domain.Entities.Task
                {
                    Id = 4,
                    Title = "Prueba 4",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut in lectus id purus finibus euismod id eu arcu. Vivamus sit amet metus eget ipsum interdum eleifend quis sed leo. In purus magna, luctus eu urna et, feugiat gravida risus. Aenean eleifend magna ante, in elementum nunc laoreet sed. Aliquam posuere quis ex vel tempor.",
                    ExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddDays(10)),
                    Status = Domain.Enums.TaskStatus.InProgress,
                    UserId = 1
                },
                new Domain.Entities.Task
                {
                    Id = 5,
                    Title = "Prueba 5",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut in lectus id purus finibus euismod id eu arcu. Vivamus sit amet metus eget ipsum interdum eleifend quis sed leo. In purus magna, luctus eu urna et, feugiat gravida risus. Aenean eleifend magna ante, in elementum nunc laoreet sed. Aliquam posuere quis ex vel tempor.",
                    ExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(3)),
                    Status = Domain.Enums.TaskStatus.Created,
                    UserId = 1
                }
                );
        }
    }
}
