using Microsoft.EntityFrameworkCore;
using votingSystem.Api.Domain;
using votingSystem.Api.Features.Candidates;

namespace votingSystem.Api.Infrastructure.DbContext;

public class VoteSystemDbContext : Microsoft.EntityFrameworkCore.DbContext
{
 public DbSet<Candidate> Candidates { get; set; } = default!;
 
 public DbSet<Vote> Votes { get; set; } = default!;
 
 public VoteSystemDbContext(DbContextOptions<VoteSystemDbContext> options) : base(options){}
 
 
 protected override void OnModelCreating(ModelBuilder modelBuilder)
 {
    modelBuilder.ApplyConfigurationsFromAssembly(typeof(VoteSystemDbContext).Assembly);

    modelBuilder.Entity<Vote>(entity =>
    {
     entity.HasKey(e => e.Id);

     entity.HasOne(e => e.Candidate)
      .WithMany(c => c.Votes)
      .HasForeignKey(e => e.CandidateId)
      .IsRequired();
    });

    modelBuilder.Entity<Candidate>(entity =>
    {
     entity.HasKey(e => e.Id);
    });
 }
}