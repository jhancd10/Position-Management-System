using PositionManagement.Domain.Entities;
using PositionManagement.Domain.Models;

namespace PositionManagement.Infrastructure.Data.Seed
{
    /// <summary>
    /// Provides seed data for the Position Management database context
    /// </summary>
    public static class PositionManagementDbContextSeed
    {
        // List of predefined departments
        private static readonly List<Department> _departments = [
            new Department { Id = Guid.NewGuid(), Name = "Technology" },
                new Department { Id = Guid.NewGuid(), Name = "Finance" },
                new Department { Id = Guid.NewGuid(), Name = "Human Resources" },
                new Department { Id = Guid.NewGuid(), Name = "Marketing" },
                new Department { Id = Guid.NewGuid(), Name = "Sales" }
        ];

        // List of predefined recruiters
        private static readonly List<Recruiter> _recruiters = [
            new Recruiter { Id = Guid.NewGuid(), Name = "Ana Perez",   Cellphone = "3001000001", Email = "ana.perez@example.com" },
                new Recruiter { Id = Guid.NewGuid(), Name = "Luis Gomez",  Cellphone = "3001000002", Email = "luis.gomez@example.com" },
                new Recruiter { Id = Guid.NewGuid(), Name = "Maria Lopez", Cellphone = "3001000003", Email = "maria.lopez@example.com" },
                new Recruiter { Id = Guid.NewGuid(), Name = "Juan Diaz",   Cellphone = "3001000004", Email = "juan.diaz@example.com" },
                new Recruiter { Id = Guid.NewGuid(), Name = "Sofia Ruiz",  Cellphone = "3001000005", Email = "sofia.ruiz@example.com" }
        ];

        // List of all possible position statuses
        private static readonly List<PositionStatusEnum> _positionStatuses = Enum.GetValues<PositionStatusEnum>().ToList();

        /// <summary>
        /// Generates a list of positions with random data
        /// </summary>
        /// <returns>A list of positions</returns>
        private static List<Position> GetPositions()
        {
            // Create a random number generator
            var rnd = new Random();

            // Generate 10 positions with random data
            return Enumerable.Range(1, 10).Select(i =>
            {
                // Select a random department
                var randomDepartment = _departments[rnd.Next(_departments.Count)];
                // Select a random recruiter
                var randomRecruiter = _recruiters[rnd.Next(_recruiters.Count)];
                // Select a random position status
                var randomPositionStatus = _positionStatuses[rnd.Next(_positionStatuses.Count)];

                // Create a new position with random data
                return new Position
                {
                    Id = Guid.NewGuid(),
                    Title = $"Position {i}",
                    Description = $"Position's description {i}",
                    Location = $"City {i}",
                    Status = randomPositionStatus,
                    Budget = 1000m + i * 100m,
                    ClosingDate = DateTime.UtcNow.AddDays(15 + i),
                    DepartmentId = randomDepartment.Id,
                    RecruiterId = randomRecruiter.Id
                };
            }).ToList();
        }

        /// <summary>
        /// Seeds the database with predefined data if it is empty
        /// </summary>
        /// <param name="context">The database context to seed</param>
        public static void Seed(this PositionManagementDbContext context)
        {
            // Check if the Departments table is empty and add predefined departments
            if (!context.Departments.Any()) context.Departments.AddRangeAsync(_departments);

            // Check if the Recruiters table is empty and add predefined recruiters
            if (!context.Recruiters.Any()) context.Recruiters.AddRangeAsync(_recruiters);

            // Check if the Positions table is empty and add generated positions
            if (!context.Positions.Any()) context.Positions.AddRangeAsync(GetPositions());

            // Save changes to the database
            context.SaveChanges();
        }
    }
}
