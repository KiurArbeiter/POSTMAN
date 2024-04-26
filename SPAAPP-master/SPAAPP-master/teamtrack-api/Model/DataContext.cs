using Microsoft.EntityFrameworkCore;

namespace teamtrack_api.Model;

public class DataContext: DbContext {
    public DataContext(DbContextOptions<DataContext> options): base(options) {}
    public DbSet<Event>? EventList {get; set;}
    public DbSet<EventPeople>? EventPeopleList {get; set;}
    public DbSet<People>? PeopleList {get; set;}
    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Event>().Property(p => p.Id).HasIdentityOptions(startValue: 1);
        modelBuilder.Entity<Event>().HasData(
            new Event{
                Id = 1,
                Type = "Treening",
                Date = new DateTime(2023, 12, 15, 12, 42, 0, DateTimeKind.Utc),
                Description = "Jooks",
                Location = "Tartu",
                Attendance = true,
            },
            new Event{
                Id = 2,
                Type = "Võistlus",
                Date = new DateTime(2024, 12, 15, 12, 42, 0, DateTimeKind.Utc),
                Description = "Jooks",
                Location = "Tallinn",
                Attendance = false,
            },
            new Event{
                Id = 3,
                Type = "Võistlus",
                Date = new DateTime(2025, 12, 15, 12, 42, 0, DateTimeKind.Utc),
                Description = "Jooks",
                Location = "Pariis",
                Attendance = true,
            },
            new Event{
                Id = 4,
                Type = "Treening",
                Date = new DateTime(2023, 12, 20, 12, 42, 0, DateTimeKind.Utc),
                Description = "Ujumine",
                Location = "Pärnu",
                Attendance = true,
            },
            new Event{
                Id = 5,
                Type = "Võistlus",
                Date = new DateTime(2024, 12, 22, 12, 42, 0, DateTimeKind.Utc),
                Description = "Ujumine",
                Location = "Saaremaa",
                Attendance = true,
            },
            new Event{
                Id = 6,
                Type = "Võistlus",
                Date = new DateTime(2025, 12, 22, 12, 42, 0, DateTimeKind.Utc),
                Description = "Ujumine",
                Location = "Inglismaa",
                Attendance = true,
            },
            new Event{
                Id = 7,
                Type = "Treening",
                Date = new DateTime(2030, 12, 22, 12, 42, 0, DateTimeKind.Utc),
                Description = "Pohlakorjelus",
                Location = "Järvamaa",
                Attendance = true,
            }
            
        );
        modelBuilder.Entity<EventPeople>().Property(p => p.Id).HasIdentityOptions(startValue: 1);
        modelBuilder.Entity<EventPeople>().HasData(
            new EventPeople{
                Id = 1,
                EventId = "1",
                PersonId = "3",
            },
            new EventPeople{
                Id = 2,
                EventId = "2",
                PersonId = "3",
            },
            new EventPeople{
                Id = 3,
                EventId = "3",
                PersonId = "3",
            }


        );
        modelBuilder.Entity<People>().Property(p => p.Id).HasIdentityOptions(startValue: 1);
        modelBuilder.Entity<People>().HasData(
            new People{
                Id = 1,
                Username = "Maarek",
                Email = "maarek@gmail.dumb",
            },
            new People{
                Id = 2,
                Username = "Indus",
                Email = "indrus@gmail.dumb",
            },
            new People{
                Id = 3,
                Username = "Leemus",
                Email = "leemusarek@gmail.dumb",
            }
        );
    }
}
