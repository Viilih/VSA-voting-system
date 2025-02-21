using votingSystem.Api.Domain;

namespace votingSystem.Api.Features.Candidates;

public interface ICandidateRepository
{
    Task AddAsync(Candidate candidate);
    
    Task<Candidate?> FindCandidateById(int id);
}