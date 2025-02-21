using Microsoft.EntityFrameworkCore;
using votingSystem.Api.Domain;
using votingSystem.Api.Infrastructure.DbContext;

namespace votingSystem.Api.Features.Candidates;

public class CandidateRepository : ICandidateRepository
{
    private readonly VoteSystemDbContext _dbContext;

    public CandidateRepository(VoteSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Candidate candidate)
    {
        await _dbContext.Candidates.AddAsync(candidate);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<Candidate?> FindCandidateById(int candidateId)
    {
        return await _dbContext.Candidates.FirstOrDefaultAsync(v => v.Id == candidateId);
    }
}