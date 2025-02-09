using FluentResults;
using votingSystem.Api.Domain;
using votingSystem.Api.Features.Candidates.CreateCandidate.HTTP;

namespace votingSystem.Api.Features.Candidates.CreateCandidate;

public class CreateCanidadateHandler
{
    private readonly CandidateRepository _candidateRepository;

    public CreateCanidadateHandler(CandidateRepository candidateRepository)
    {
        _candidateRepository = candidateRepository;
    }

    public async Task<Result<CreateCandidateResponse>> Handle(CreateCandidateRequest request)
    {
        try
        {
            var candidateResult = Candidate.Create(request.name);

            if (candidateResult.IsFailed)
            {
                return Result.Fail<CreateCandidateResponse>(candidateResult.Errors);
            }
            
            var candidate = candidateResult.Value;
            await _candidateRepository.AddAsync(candidate);
            
            var createCandidateResponse = new CreateCandidateResponse(candidate.Id, candidate.Name);
            
            return Result.Ok<CreateCandidateResponse>(createCandidateResponse);
        }
        catch (Exception e)
        {
            return Result.Fail<CreateCandidateResponse>(e.Message);
        }
    }
}