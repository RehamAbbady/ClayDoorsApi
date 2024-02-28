using DoorManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace DoorManagementSystem.Infrastructure
{
    public class DoorManagementContext : DbContext
    {
        public DoorManagementContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Door> Doors { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleDoorAccess> RoleDoorAccess { get; set; }
        public DbSet<UserTag> UserTags { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<DoorLog> DoorLogs { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("door_user");

            #region Indexes
            modelBuilder.Entity<Door>()
                .HasIndex(d => d.Location);

            modelBuilder.Entity<DoorLog>()
                .HasIndex(dl => dl.AccessDateTime);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            #endregion

            #region relationships
            // Door to DoorLog (One-to-Many)
            modelBuilder.Entity<Door>()
                .HasMany(d => d.DoorLogs)
                .WithOne(dl => dl.Door)
                .HasForeignKey(dl => dl.DoorID);

            // Role to RoleDoor (One-to-Many)
            modelBuilder.Entity<Role>()
                .HasMany(r => r.RoleDoors)
                .WithOne(rd => rd.Role)
                .HasForeignKey(rd => rd.RoleId);

            // Door to RoleDoor (One-to-Many)
            modelBuilder.Entity<Door>()
                .HasMany(d => d.RoleDoors)
                .WithOne(rd => rd.Door)
                .HasForeignKey(rd => rd.DoorId);

            // User to UserRole (One-to-Many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId);

            // Role to UserRole (One-to-Many)
            modelBuilder.Entity<Role>()
                .HasMany(r => r.UserRoles)
                .WithOne(ur => ur.Role)
                .HasForeignKey(ur => ur.RoleId);

            // User to DoorLog (One-to-Many)
            modelBuilder.Entity<User>()
                .HasMany(u => u.DoorLogs)
                .WithOne(dl => dl.User)
                .HasForeignKey(dl => dl.UserID);


            #endregion

        }


    }
}
