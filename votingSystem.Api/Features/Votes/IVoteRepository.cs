using votingSystem.Api.Domain;

namespace votingSystem.Api.Features.Votes;

public interface IVoteRepository
{
    Task AddVote(Vote vote);

    Task<Candidate?> FindCandidateById(int candidateId);
}