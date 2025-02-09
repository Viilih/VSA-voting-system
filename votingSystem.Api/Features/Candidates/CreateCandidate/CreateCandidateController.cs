using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using votingSystem.Api.Features.Candidates.CreateCandidate.HTTP;

namespace votingSystem.Api.Features.Candidates.CreateCandidate
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreateCandidateController : ControllerBase
    {
        
        private readonly CreateCanidadateHandler _handler;
        public CreateCandidateController(CreateCanidadateHandler handler)
        {
            _handler = handler;
        }
        [HttpPost("create-candidate")]
        public async Task<IActionResult> CreateCandidate(CreateCandidateRequest request)
        {
            var result = await _handler.Handle(request);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Errors);
        }
    }
}
