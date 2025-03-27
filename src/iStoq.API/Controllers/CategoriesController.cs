using Microsoft.AspNetCore.Mvc;
using iStoq.Application.DTOs;
using iStoq.Application.Interfaces;

namespace iStoq.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoriesController(ICategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_service.GetAll());

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var category = _service.GetById(id);
        if (category == null)
            return NotFound();
        return Ok(category);
    }
    
    [HttpPost]
    public IActionResult Create(CategoryCreateDto dto)
    {
        var created = _service.Create(dto);
        return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, CategoryUpdateDto dto)
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
