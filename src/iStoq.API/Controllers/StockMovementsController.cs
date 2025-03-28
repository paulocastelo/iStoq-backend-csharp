using Microsoft.AspNetCore.Mvc;
using iStoq.Application.DTOs;
using iStoq.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace iStoq.API.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class StockMovementsController : ControllerBase
{
    private readonly IStockMovementService _service;

    public StockMovementsController(IStockMovementService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var movements = _service.GetAll();
        return Ok(movements);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var movement = _service.GetById(id);
        if (movement == null)
            return NotFound();
        return Ok(movement);
    }

    [HttpPost]
    public IActionResult Create(StockMovementCreateDto dto)
    {
        var movement = _service.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = movement.Id }, movement);
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, StockMovementUpdateDto dto)
    {
        var movement = _service.Update(id, dto);
        if (movement == null)
            return NotFound();
        return Ok(movement);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var result = _service.Delete(id);
        if (!result)
            return NotFound();
        return NoContent();
    }
}