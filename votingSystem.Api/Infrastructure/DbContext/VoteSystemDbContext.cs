using Microsoft.EntityFrameworkCore;
using votingSystem.Api.Domain;
using votingSystem.Api.Features.Candidates;

namespace votingSystem.Api.Infrastructure.DbContext;

public class VoteSystemDbContext : Microsoft.EntityFrameworkCore.DbContext
{
 public DbSet<Candidate> Candidates { get; set; } = default!;
 
 public VoteSystemDbContext(DbContextOptions<VoteSystemDbContext> options) : base(options){}
 
 
 protected override void OnModelCreating(ModelBuilder modelBuilder)
 {
  modelBuilder.ApplyConfigurationsFromAssembly(typeof(VoteSystemDbContext).Assembly);
 }
}