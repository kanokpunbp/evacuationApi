using evacuation.Application.DTOs.EvacuationPlan;
using evacuation.Application.DTOs.EvacuationZones;
using evacuation.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace evacuationApi.Controllers
{
    [ApiController]
    [Route("api/evacuations")]
    public class EvacuationsController : ControllerBase
    {

        private readonly IEvacuationPlanService _service;
        private readonly IEvacuationStatusCache _evacuationStatusCache;
        public EvacuationsController(IEvacuationPlanService service,
            IEvacuationStatusCache evacuationStatusCache)
        {
            _service = service;
            _evacuationStatusCache = evacuationStatusCache;
        }

        [HttpPost("plan")]
        public async Task<IActionResult> Create()
        {
            bool success = await _service.CreatePlans();

            if (!success)
                return BadRequest("Failed to create evacuation plans");

            return Ok();
        }

        [HttpGet("ActivePlans")]
        public async Task<IActionResult> GetActivePlans()
        {
            var result = await _service.GetActivePlansAsync();
            return Ok(result);
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetStatus()
        {
            var result = await _evacuationStatusCache.GetAllAsync();
            return Ok(result);
        }

        [HttpPut("{planId}")]
        public async Task<IActionResult> Update(Guid planId, UpdateEvacuationPlanDto dto)
        {

            bool success = await _service.UpdatStatus(planId, dto);
            if (!success)
                return BadRequest();

            return Ok();
        }

        [HttpDelete()]
        public async Task<IActionResult> Clear()
        {
            await _service.ClearPlans();
            return NoContent();

        }
    }
}