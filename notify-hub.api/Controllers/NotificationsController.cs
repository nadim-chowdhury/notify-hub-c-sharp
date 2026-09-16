using Microsoft.AspNetCore.Mvc;
using notify_hub.application.DTOs;
using notify_hub.application.Interfaces;

namespace notify_hub.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController(INotificationService notificationService) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(NotificationResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(
        [FromBody] CreateNotificationRequest request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            var result = await notificationService.CreateAsync(request, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(
                new ProblemDetails
                {
                    Title = "Validation Error",
                    Detail = ex.Message,
                    Status = StatusCodes.Status400BadRequest,
                }
            );
        }
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(NotificationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var result = await notificationService.GetByIdAsync(id, cancellationToken);
        if (result is null)
        {
            return NotFound(
                new ProblemDetails
                {
                    Title = "Not Found",
                    Detail = $"Notification with ID '{id}' was not found.",
                    Status = StatusCodes.Status404NotFound,
                }
            );
        }

        return Ok(result);
    }

    [HttpGet("recipient/{recipientId:guid}")]
    [ProducesResponseType(typeof(IReadOnlyList<NotificationResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByRecipientId(
        Guid recipientId,
        CancellationToken cancellationToken
    )
    {
        var result = await notificationService.GetByRecipientIdAsync(
            recipientId,
            cancellationToken
        );
        return Ok(result);
    }

    [HttpPatch("{id:guid}/read")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> MarkAsRead(Guid id, CancellationToken cancellationToken)
    {
        var updated = await notificationService.MarkAsReadAsync(id, cancellationToken);
        if (!updated)
        {
            return NotFound(
                new ProblemDetails
                {
                    Title = "Not Found",
                    Detail = $"Notification with ID '{id}' was not found.",
                    Status = StatusCodes.Status404NotFound,
                }
            );
        }

        return NoContent();
    }
}
