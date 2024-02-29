using DoorManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace DoorManagementSystem.Infrastructure
{
    public class DoorManagementContext : DbContext
    {
        public DoorManagementContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Door> Doors { get; set; }
        public DbSet<DoorLog> DoorLogs { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleDoor> RoleDoors { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserTag> UserTags { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //  modelBuilder.HasDefaultSchema("access_control");


            #region keys

            modelBuilder.Entity<RolePermission>()
                .HasKey(rp => new { rp.RoleId, rp.PermissionId });
            modelBuilder.Entity<RoleDoor>()
                .HasKey(rp => new { rp.RoleId, rp.DoorId });
            modelBuilder.Entity<UserTag>()
                .HasKey(rp => new { rp.UserId, rp.TagId });
            modelBuilder.Entity<UserRole>()
             .HasKey(rp => new { rp.UserId, rp.RoleId });

            #endregion
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

            modelBuilder.Entity<Door>()
                .HasMany(d => d.DoorLogs)
                .WithOne(dl => dl.Door)
                .HasForeignKey(dl => dl.DoorID);

            modelBuilder.Entity<Role>()
                .HasMany(r => r.RoleDoors)
                .WithOne(rd => rd.Role)
                .HasForeignKey(rd => rd.RoleId);

            modelBuilder.Entity<Door>()
                .HasMany(d => d.RoleDoors)
                .WithOne(rd => rd.Door)
                .HasForeignKey(rd => rd.DoorId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<Role>()
                .HasMany(r => r.UserRoles)
                .WithOne(ur => ur.Role)
                .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<User>()
                .HasMany(u => u.DoorLogs)
                .WithOne(dl => dl.User)
                .HasForeignKey(dl => dl.UserID);




            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Role)
                .WithMany(r => r.RolePermissions)
                .HasForeignKey(rp => rp.RoleId);

            modelBuilder.Entity<RolePermission>()
                .HasOne(rp => rp.Permission)
                .WithMany(p => p.RolePermissions)
                .HasForeignKey(rp => rp.PermissionId);


            #endregion

        }


    }
}
