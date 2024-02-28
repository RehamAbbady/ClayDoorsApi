using DoorManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace DoorManagementSystem.Infrastructure
{
    public class DoorManagementContext : DbContext
    {
        public DoorManagementContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Doors> Doors { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<RoleDoorAccess> RoleDoorAccess { get; set; }
        public DbSet<UserTags> UserTags { get; set; }
        public DbSet<UserRoles> UserRoles { get; set; }
        public DbSet<DoorLogs> DoorLogs { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("door_user");

            #region Indexes
            modelBuilder.Entity<Doors>()
                .HasIndex(d => d.Location);

            modelBuilder.Entity<DoorLogs>()
                .HasIndex(dl => dl.AccessDateTime);

            modelBuilder.Entity<Users>()
                .HasIndex(u => u.Email);

            modelBuilder.Entity<Users>()
                .HasIndex(u => u.Email)
                .IsUnique();
            #endregion

            #region relationships
            // Door to DoorLog (One-to-Many)
            modelBuilder.Entity<Doors>()
                .HasMany(d => d.DoorLogs)
                .WithOne(dl => dl.Door)
                .HasForeignKey(dl => dl.DoorID);

            // Role to RoleDoor (One-to-Many)
            modelBuilder.Entity<Roles>()
                .HasMany(r => r.RoleDoors)
                .WithOne(rd => rd.Role)
                .HasForeignKey(rd => rd.RoleId);

            // Door to RoleDoor (One-to-Many)
            modelBuilder.Entity<Doors>()
                .HasMany(d => d.RoleDoors)
                .WithOne(rd => rd.Door)
                .HasForeignKey(rd => rd.DoorId);

            // User to UserRole (One-to-Many)
            modelBuilder.Entity<Users>()
                .HasMany(u => u.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId);

            // Role to UserRole (One-to-Many)
            modelBuilder.Entity<Roles>()
                .HasMany(r => r.UserRoles)
                .WithOne(ur => ur.Role)
                .HasForeignKey(ur => ur.RoleId);

            // User to DoorLog (One-to-Many)
            modelBuilder.Entity<Users>()
                .HasMany(u => u.DoorLogs)
                .WithOne(dl => dl.User)
                .HasForeignKey(dl => dl.UserID);


            #endregion

        }


    }
}
