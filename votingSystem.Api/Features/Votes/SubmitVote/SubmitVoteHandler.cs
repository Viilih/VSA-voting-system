using FluentResults;
using votingSystem.Api.Features.Votes.SubmitVote.HTTP;
using votingSystem.Api.Infrastructure.Messaging.RabbitMQ;

namespace votingSystem.Api.Features.Votes.SubmitVote;

public class SubmitVoteHandler
{
    private readonly IRabbitMqProducer _rabbitMqProducer;

    public SubmitVoteHandler(IRabbitMqProducer rabbitMqProducer)
    {
        _rabbitMqProducer = rabbitMqProducer;
    }

    public async Task<Result<SubmitVoteResponse>> Handle(SubmitVoteRequest request)
    {
        _rabbitMqProducer.SendMessage(request.CandidateId);

        
        var SubmitVoteResponse = new SubmitVoteResponse(request.CandidateId);
        
        
        return Result.Ok<SubmitVoteResponse>(SubmitVoteResponse);


    }
}