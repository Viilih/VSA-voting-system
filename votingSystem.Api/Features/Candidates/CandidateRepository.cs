using votingSystem.Api.Domain;
using votingSystem.Api.Infrastructure.DbContext;

namespace votingSystem.Api.Features.Candidates;

public class CandidateRepository
{
    private readonly VoteSystemDbContext _dbContext;

    public CandidateRepository(VoteSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Candidate candidate)
    {
        _dbContext.Candidates.AddAsync(candidate);
        await _dbContext.SaveChangesAsync();
    }
}