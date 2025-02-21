using FluentResults;
using votingSystem.Api.Domain;

namespace votingSystem.Api.Features.Votes.ProcessVote;

public class ProcessVoteHandler
{
    private readonly IVoteRepository _voteRepository;

    public ProcessVoteHandler(IVoteRepository voteRepository)
    {
        _voteRepository = voteRepository;
    }

    public async Task<Result> Handle(int candidateId)
    {
        var candidate = await _voteRepository.FindCandidateById(candidateId);
        if (candidate == null)
        {
            return Result.Fail("Candidate not found");
        }

        var voteToAdd = Vote.Create(candidateId);
        if (voteToAdd.IsFailed)
        {
            return Result.Fail("Invalid vote");
        }

        try
        {
            await _voteRepository.AddVote(voteToAdd.Value);
            return Result.Ok();
        }
        catch (InvalidOperationException ex)
        {
            return Result.Fail(ex.Message); // Return error if duplicate vote detected
        }
    }
}