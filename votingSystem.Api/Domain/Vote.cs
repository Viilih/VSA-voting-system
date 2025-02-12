using FluentResults;

namespace votingSystem.Api.Domain;

public class Vote
{
    public Guid Id { get; private set; }
    
    public int CandidateId { get; private set; }
    
    public virtual Candidate Candidate { get; private set; }

    private Vote(int candidateId)
    {
        CandidateId = candidateId;
    }

    public static Result<Vote> Create(int candidateId)
    {
        if (candidateId < 1)
        {
            return Result.Fail<Vote>("CandidateId cannot be null or empty");
        }

        return Result.Ok(new Vote(candidateId));
    }
}