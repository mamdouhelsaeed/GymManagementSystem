using GymManagementSystem.Models;

namespace GymManagementSystem.Data
{
    public static class DbInitializer
    {
        public static void Seed(ApplicationDbContext context)
        {
            // ---- Users (login) ----
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User { Email = "admin@gym.com", Password = "Admin123", Name = "System Admin", IsAdmin = true },
                    new User { Email = "trainer@gym.com", Password = "Trainer123", Name = "Sample Trainer", IsAdmin = false }
                );
                context.SaveChanges();
            }

            // ---- Trainers ----
            if (!context.Trainers.Any())
            {
                context.Trainers.AddRange(
                    new Trainer { Name = "John Carter", Specialization = "Strength & Conditioning" },
                    new Trainer { Name = "Amira Hassan", Specialization = "Yoga & Flexibility" },
                    new Trainer { Name = "Mike Chen", Specialization = "CrossFit" }
                );
                context.SaveChanges();
            }

            // ---- Gym Classes ----
            if (!context.GymClasses.Any())
            {
                var john = context.Trainers.First(t => t.Name == "John Carter");
                var amira = context.Trainers.First(t => t.Name == "Amira Hassan");
                var mike = context.Trainers.First(t => t.Name == "Mike Chen");

                context.GymClasses.AddRange(
                    new GymClass { Name = "Morning Strength", Description = "Full body strength training.", Schedule = "Mon/Wed/Fri 7:00 AM", TrainerId = john.Id },
                    new GymClass { Name = "Power Lifting", Description = "Focused heavy lifting session.", Schedule = "Tue/Thu 6:00 PM", TrainerId = john.Id },
                    new GymClass { Name = "Vinyasa Flow Yoga", Description = "Relaxing flow-based yoga class.", Schedule = "Mon/Wed 8:00 AM", TrainerId = amira.Id },
                    new GymClass { Name = "Deep Stretch", Description = "Flexibility and mobility work.", Schedule = "Fri 5:00 PM", TrainerId = amira.Id },
                    new GymClass { Name = "CrossFit Basics", Description = "Intro to CrossFit movements.", Schedule = "Sat 9:00 AM", TrainerId = mike.Id }
                );
                context.SaveChanges();
            }

            // ---- Members ----
            if (!context.Members.Any())
            {
                context.Members.AddRange(
                    new Member { Name = "Sara Ahmed", Email = "sara.ahmed@example.com", Phone = "01000000001" },
                    new Member { Name = "Omar Youssef", Email = "omar.youssef@example.com", Phone = "01000000002" },
                    new Member { Name = "Lina Fathy", Email = "lina.fathy@example.com", Phone = "01000000003" }
                );
                context.SaveChanges();
            }

            // ---- Sample Enrollments ----
            if (!context.Enrollments.Any())
            {
                var sara = context.Members.First(m => m.Name == "Sara Ahmed");
                var omar = context.Members.First(m => m.Name == "Omar Youssef");
                var strength = context.GymClasses.First(c => c.Name == "Morning Strength");
                var yoga = context.GymClasses.First(c => c.Name == "Vinyasa Flow Yoga");

                context.Enrollments.AddRange(
                    new Enrollment { MemberId = sara.Id, GymClassId = strength.Id, EnrollmentDate = DateTime.Today.AddDays(-10) },
                    new Enrollment { MemberId = omar.Id, GymClassId = yoga.Id, EnrollmentDate = DateTime.Today.AddDays(-5) }
                );
                context.SaveChanges();
            }
        }
    }
}
