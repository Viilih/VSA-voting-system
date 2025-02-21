using Microsoft.AspNetCore.Mvc;
using votingSystem.Api.Features.Votes.SubmitVote.HTTP;

namespace votingSystem.Api.Features.Votes.SubmitVote;


[Route("api/[controller]")]
[ApiController]
public class SubmitVoteController : ControllerBase
{
    
    private readonly SubmitVoteHandler _handler;

    public SubmitVoteController(SubmitVoteHandler handler)
    {
        _handler = handler;
    }
    [HttpPost("submit-vote")]
    public async Task<IActionResult> SubmitVote([FromBody] SubmitVoteRequest request)
    {
        var result = await _handler.Handle(request);
        if (result.IsFailed)
        {
            return BadRequest(result.Errors);
        }
        return Ok(result);
    }
    
}