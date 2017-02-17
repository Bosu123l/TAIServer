using Microsoft.EntityFrameworkCore;

namespace TAIServer.Entities.DataAccess
{
    public class DataContext : DbContext
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<TaskGroup> TaskGroups { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<Member> Members { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region mapping betwen task and member

            modelBuilder.Entity<Member>()
                .HasMany(x => x.Tasks)
                .WithOne(x => x.Member)
                .HasForeignKey(x => x.MemberId)
                .IsRequired(false);

            modelBuilder.Entity<TaskGroup>()
                .HasOne(x=>x.TaskGroupManager)
                .WithMany(x=>x.ManagedTaskGroups)
                .HasForeignKey(x => x.TaskGroupManagerId)
                .IsRequired(true);

            modelBuilder.Entity<Member>()
                .HasMany(m => m.ManagedTaskGroups)
                .WithOne(tg => tg.TaskGroupManager)
                .HasForeignKey(tg => tg.TaskGroupManagerId)
                .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict)
                .IsRequired(true);

            #endregion mapping betwen task and member

            modelBuilder.Entity<Project>()
                .HasMany(x => x.TaskGroups)
                .WithOne(x => x.Project)
                .HasForeignKey(x => x.ProjectId)
                .IsRequired(false);

            modelBuilder.Entity<Project>()
                .HasOne(p => p.ProjectManager)
                .WithMany(m => m.ManagedProjects)
                .HasForeignKey(p => p.ProjectManagerId)
                .OnDelete(Microsoft.EntityFrameworkCore.Metadata.DeleteBehavior.Restrict)
                .IsRequired(false);

            #region many-to-many project mamber relationship

            modelBuilder.Entity<ProjectMember>().HasKey(t => new { t.MemberId, t.ProjectId });

            modelBuilder.Entity<ProjectMember>()
                .HasOne(p => p.Project)
                .WithMany(m => m.ProjectMembers)
                .HasForeignKey(p => p.ProjectId);

            modelBuilder.Entity<ProjectMember>()
                .HasOne(m => m.Member)
                .WithMany(p => p.ProjectsMembers)
                .HasForeignKey(m => m.MemberId);

            #endregion many-to-many project mamber relationship

            #region many-to-many taskgroup member relationship

            modelBuilder.Entity<GroupTaskMember>().HasKey(t => new { t.MemberId, t.GroupTaskId });

            modelBuilder.Entity<GroupTaskMember>()
                .HasOne(p => p.TaskGroup)
                .WithMany(m => m.GroupTaskMembers)
                .HasForeignKey(p => p.GroupTaskId);

            modelBuilder.Entity<GroupTaskMember>()
                .HasOne(m => m.Member)
                .WithMany(p => p.GroupTaskMembers)
                .HasForeignKey(m => m.MemberId);

            #endregion many-to-many taskgroup member relationship

            modelBuilder.Entity<TaskGroup>()
                .HasMany(x => x.Tasks)
                .WithOne(x => x.TaskGroup)
                .HasForeignKey(x => x.TaskGroupId)
                .IsRequired(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}