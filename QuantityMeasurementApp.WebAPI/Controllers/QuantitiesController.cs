using Microsoft.AspNetCore.Mvc;
using QuantityMeasurementApp.Service.Interfaces;
using QuantityMeasurementApp.Model.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

[ApiController]
[Route("api/[controller]")]
public class QuantitiesController : ControllerBase
{
    private readonly IQuantityService _service;

    public QuantitiesController(IQuantityService service)
    {
        _service = service;
    }

    [HttpPost("convert")]
    public IActionResult Convert(ConvertRequest req)
    {
        var result = _service.Convert(req.Source, req.TargetUnit);
        return Ok(result);
    }

    [HttpPost("add")]
    public IActionResult Add(ArithmeticRequest req)
    {
        var result = _service.Add(req.Q1, req.Q2, req.TargetUnit);
        return Ok(result);
    }

    [HttpPost("subtract")]
    public IActionResult Subtract(ArithmeticRequest req)
    {
        var result = _service.Subtract(req.Q1, req.Q2, req.TargetUnit);
        return Ok(result);
    }

    [HttpPost("divide")]
    public IActionResult Divide(ArithmeticRequest req)
    {
        return Ok(_service.Divide(req.Q1, req.Q2));
    }

    [Authorize]
    [HttpGet("history")]
    public IActionResult GetHistory()
    {
        // Read the email claim from the JWT token
        var email = User.FindFirst(ClaimTypes.Email)?.Value
                ?? User.FindFirst("email")?.Value;

        if (string.IsNullOrEmpty(email))
            return Unauthorized("Could not identify user from token.");

        // Pass email to your service so it filters by user
        var result = _service.GetHistory(email);
        return Ok(result);
    }

    [Authorize]
    [HttpGet("HistoryCount")]
    public IActionResult GetHistoryCount()
    {
        var email = User.FindFirst(ClaimTypes.Email)?.Value
                ?? User.FindFirst("email")?.Value;

        if (string.IsNullOrEmpty(email))
            return Unauthorized("Could not identify user from token.");

        return Ok(_service.GetHistoryCount(email));
    }
}