using evacuation.Application.DTOs.EvacuationZones;
using evacuation.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace evacuationApi.Controllers
{
    [ApiController]
    [Route("api/evacuation-zones")]
    public class EvacuationZonesController : ControllerBase
    {
        private readonly IEvacuationZoneService _service;

        public EvacuationZonesController(IEvacuationZoneService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
            => Ok(await _service.GetByIdAsync(id));

        [HttpPost]
        public async Task<IActionResult> Create(CreateEvacuationZoneDto dto)
        {
            var id = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(UpdateEvacuationZoneDto dto)
        {
            await _service.UpdateAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
