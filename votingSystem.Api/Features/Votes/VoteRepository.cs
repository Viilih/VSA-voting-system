using Microsoft.EntityFrameworkCore;
using votingSystem.Api.Domain;
using votingSystem.Api.Infrastructure.DbContext;

namespace votingSystem.Api.Features.Votes;

public class VoteRepository
{
    private readonly VoteSystemDbContext _dbContext;

    public VoteRepository(VoteSystemDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddVote(Vote vote)
    {
        await _dbContext.Votes.AddAsync(vote);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Candidate?> FindCandidateById(int candidateId)
    {
        return await _dbContext.Candidates.FirstOrDefaultAsync(v => v.Id == candidateId);
    }
}