using System.Collections.Generic;
using TAI.Utils;

namespace TAIServer.Entities.DataAccess
{
    public static class MigrationConfiguration
    {
        private static ProjectMember projectMember = new ProjectMember()
        {
            Project = firstProject,
            Member = Bogdan
        };

        private static Member Bogdan = new Member()
        {
            FirstName = "Bogdan",
            Surname = "Starachowicki",
            Login = "bogdan.starachowicki",
            Password = Common.Sha256("1234"),
            ProjectsMembers = new List<ProjectMember>()
            {
                projectMember
            }
        };

        private static Member Andzej = new Member()
        {
            FirstName = "Andzej",
            Surname = "Starachowicki",
            Login = "andrzej.starachowicki",
            Password = Common.Sha256("1234"),
            ProjectsMembers = new List<ProjectMember>()
            {
                projectMember
            }
        };

        private static Project firstProject = new Project()
        {
            Name = "Tai Projekt",
            ProjectManager = Andzej,
            ProjectMembers = new List<ProjectMember>()
            {
                projectMember
            }
        };

        private static Task task = new Task()
        {
            Status = Status.InProgress,
            Name = "Szablon Projektu",
            Member = Bogdan,
            Description = "Być grubym i truć ludziom ze nie umie se nic",
            Duration = 10,
            ExpectedTaskDuration = 30,
            PercentOfComplite = 33
        };

        public static void Seed(this DataContext context)
        {
            // Perform database delete and create
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            // Perform seed operations
            //  AddMemebers(context);
            //  AddUsers(context);

            AddProject(context);
            AddTaskGroup(context);
            AddMemebers(context);
            AddTask(context);

            // Save changes and release resources
            context.SaveChanges();
            context.Dispose();
        }

        private static void AddMemebers(DataContext context)
        {
            context.AddRange(
               Bogdan,
               Andzej,
                new Member()
                {
                    FirstName = "Janusz",
                    Surname = "Walczak",
                    Login = "janusz.walczak",
                    Password = Common.Sha256("1234")
                }
            );
        }

        private static void AddTask(DataContext context)
        {
            context.AddRange(
                 task,
                 new Task()
                 {
                     Name = "Budowa Projektu",
                     ExpectedTaskDuration = 30,
                 }
            );
        }

        private static void AddTaskGroup(DataContext context)
        {
            context.AddRange(
                new TaskGroup()
                {
                    Name = "Morele",
                    Project = firstProject,
                    TaskGroupManager = Andzej,
                    Tasks = new List<Task>()
                    {
                        task
                    }
                }

            );
        }

        private static void AddProject(DataContext context)
        {
            context.AddRange(
                new Project()
                {
                    Name = "Tai",
                    ProjectManager = Andzej,
                    ProjectMembers = new List<ProjectMember>()
                    {
                        projectMember
                    }
                });
        }
    }
}