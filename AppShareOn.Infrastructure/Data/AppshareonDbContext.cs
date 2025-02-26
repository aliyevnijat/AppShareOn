using AppShareOn.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppShareOn.Infrastructure.Data;

/// <summary>
/// Application database context using SQLite.
/// </summary>
public class AppshareonDbContext : IdentityDbContext
{
    public AppshareonDbContext(DbContextOptions<AppshareonDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// <see cref="DbSet{PlatformEntity}"/>.
    /// </summary>
    public DbSet<PlatformEntity> Platforms { get; set; }

    /// <summary>
    /// <see cref="DbSet{WallEntity}"/>.
    /// </summary>
    public DbSet<WallEntity> Walls { get; set; }

    /// <summary>
    /// <see cref="DbSet{ProfileEntity}"/>.
    /// </summary>
    public DbSet<ProfileEntity> Profiles { get; set; }

    /// <summary>
    /// <see cref="DbSet{PostEntity}"/>.
    /// </summary>
    public DbSet<PostEntity> Posts { get; set; }

    /// <summary>
    /// <see cref="DbSet{HashtagEntity}"/>.
    /// </summary>
    public DbSet<HashtagEntity> Hashtags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Call base OnModelCreating for identity.
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<HashtagEntity>()
            .HasMany(e => e.Posts)
            .WithMany(e => e.Hashtags)
            .UsingEntity("HashtagsToPostsMapping");

        modelBuilder.Entity<WallEntity>()
            .HasMany(e => e.Hashtags)
            .WithMany(e => e.Walls)
            .UsingEntity("WallsToHashtagsMapping");

        modelBuilder.Entity<WallEntity>()
            .HasMany(e => e.Profiles)
            .WithMany(e => e.Walls)
            .UsingEntity("WallsToProfilesMapping");

        // This is a work around for how SQLite handles Guids. 
        // TODO: Remove if using SQL Server.
        modelBuilder.Entity<PlatformEntity>().Property(k => k.Id).HasConversion<string>();
    }
}