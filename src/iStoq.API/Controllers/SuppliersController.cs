﻿using Microsoft.AspNetCore.Mvc;
using iStoq.Application.DTOs;
using iStoq.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace iStoq.API.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class SuppliersController : ControllerBase
{
    private readonly ISupplierService _service;

    public SuppliersController(ISupplierService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_service.GetAll());

    [HttpPost]
    public IActionResult Create(SupplierDto dto)
    {
        var created = _service.Create(dto);
        return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
    }
    [HttpPut("{id}")]
    public IActionResult Update(Guid id, SupplierDto dto)
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
