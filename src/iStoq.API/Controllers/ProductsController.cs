using Microsoft.AspNetCore.Mvc;
using iStoq.Application.DTOs;
using iStoq.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace iStoq.API.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;

    public ProductsController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_service.GetAll());

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var product = _service.GetById(id);
        return product is null ? NotFound() : Ok(product);
    }

    [HttpPost]
    public IActionResult Create(ProductCreateDto dto)
    {
        var created = _service.Create(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, ProductUpdateDto dto)
    {
        var updated = _service.Update(id, dto);
        if (updated == null) return NotFound();
        return Ok(updated);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        if (_service.Delete(id))
            return NoContent();
        return NotFound();
    }
}
