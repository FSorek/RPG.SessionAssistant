using System.Net;
using Blazor.WebApp.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SessionAssistant.API.Encounters;
using SessionAssistant.API.Persistence;
using SessionAssistant.Shared.DTOs.Combat;
using SessionAssistant.Shared.DTOs.Combat.Requests;

namespace SessionAssistant.API.Combat;

[Route("api/Encounters/{encounterId:int}/[controller]")]
[ApiController]
public class CombatantsController(
    AppDbContext dbContext,
    IHubContext<EncounterHub, IEncounterClient> hubContext)
    : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<ActionResult<CombatantDTO?>> Get(int id)
    {
        var combatant = await dbContext.Combatants.FindAsync(id);
        if (combatant is not null)
            return Ok(combatant);
        return NotFound();
    }
    [HttpGet]
    public async Task<ActionResult<CombatantDTO?>> GetForUser([FromQuery]int userId)
    {
        var combatant = await dbContext.Combatants.FirstOrDefaultAsync(c => c.UserId == userId);
        if (combatant is not null)
            return Ok(combatant.ToDTO());
        return NotFound();
    }
    [HttpPost("{id}")]
    public async Task<IActionResult> EndTurn(int encounterId, int id, [FromBody]EndTurnRequest request)
    {
        var encounter = await dbContext.Encounters.FindAsync(encounterId);
        if (encounter == null)
        {
            return NotFound();
        }
            
        encounter.EndTurn(id, request.UsedMultiAttack);
        await dbContext.SaveChangesAsync();
        await hubContext.Clients.All.UpdateEncounter();
        return Ok();
    }
        
    [HttpPost]
    public async Task<ActionResult> Create(int encounterId, CreateCombatantRequest request)
    {
        var encounter = await dbContext.Encounters.FindAsync(encounterId);
        if (encounter == null)
            return BadRequest();
            
        var combatant = encounter.EnterCombat(request.Name, request.Initiative, request.Attacks, request.UserId);
        await dbContext.SaveChangesAsync();
        await hubContext.Clients.All.UpdateEncounter();
        return CreatedAtAction(
            nameof(Get),
            new { id = combatant.Id },
            combatant.ToDTO()
        );
    }
}