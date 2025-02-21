using FluentResults;
using votingSystem.Api.Features.Candidates;
using votingSystem.Api.Features.Votes.SubmitVote.HTTP;
using votingSystem.Api.Infrastructure.Messaging.RabbitMQ;

namespace votingSystem.Api.Features.Votes.SubmitVote;

public class SubmitVoteHandler
{
    private readonly IRabbitMqProducer _rabbitMqProducer;
    private readonly ICandidateRepository _candidateRepository;

    public SubmitVoteHandler(IRabbitMqProducer rabbitMqProducer,ICandidateRepository candidateRepository)
    {
        _rabbitMqProducer = rabbitMqProducer;
        _candidateRepository = candidateRepository;
    }

    public async Task<Result<SubmitVoteResponse>> Handle(SubmitVoteRequest request)
    {
        if (request.CandidateId < 1)
        {
            return Result.Fail<SubmitVoteResponse>("Invalid candidate Id");
        }
        
        var candidate = await _candidateRepository.FindCandidateById(request.CandidateId);

        if (candidate == null)
        {
            return Result.Fail<SubmitVoteResponse>("Candidate not found");
        }
        _rabbitMqProducer.SendMessage(request.CandidateId);

        
        var submitVoteResponse = new SubmitVoteResponse(request.CandidateId);
        
        
        return Result.Ok<SubmitVoteResponse>(submitVoteResponse);


    }
}