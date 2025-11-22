using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ML_API.Extensions;
using ML_Application.DTOs.Lenders;
using ML_Application.Services;

namespace ML_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Lender,Admin")]
    public class LendersController : ControllerBase
    {
        private readonly ILenderService _lenderService;
        private readonly ILogger<LendersController> _logger;

        public LendersController(ILenderService lenderService, ILogger<LendersController> logger)
        {
            _lenderService = lenderService;
            _logger = logger;
        }

        [HttpGet("me")]
        [ProducesResponseType(typeof(LenderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMe(CancellationToken ct)
        {
            var userId = HttpContext.GetUserId();
            var lender = await _lenderService.GetByUserIdAsync(userId, ct);

            if (lender is null)
                return NotFound();

            return Ok(lender);
        }

        [HttpPost]
        [ProducesResponseType(typeof(LenderResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] LenderCreateRequest request, CancellationToken ct)
        {
            var userId = HttpContext.GetUserId();
            var lender = await _lenderService.CreateAsync(userId, request, ct);
            return CreatedAtAction(nameof(GetMe), new { id = lender.Id }, lender);
        }

        [HttpPut("me")]
        [ProducesResponseType(typeof(LenderResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] LenderUpdateRequest request, CancellationToken ct)
        {
            var userId = HttpContext.GetUserId();
            var lender = await _lenderService.UpdateAsync(userId, request, ct);
            return Ok(lender);
        }
    }
}
