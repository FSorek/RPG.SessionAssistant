using Blazor.WebApp.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SessionAssistant.API.Encounters;
using SessionAssistant.API.Persistence;
using SessionAssistant.Shared.DTOs.Combat.Requests;

namespace SessionAssistant.API.Combat
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncountersController(
        AppDbContext dbContext,
        IHubContext<EncounterHub, IEncounterClient> hubContext)
        : ControllerBase
    {
        // GET: api/Encounters/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var encounter = await dbContext
                .Encounters
                .AsNoTracking()
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();
            if (encounter is null)
            {
                return NotFound();
            }
            
            return Ok(encounter.ToDTO());
        }
        
        // POST: api/Encounters
        [HttpPost]
        public async Task<ActionResult> Create([FromBody]CreateEncounterRequest createRequest)
        {
            var encounter = new Encounter(createRequest.Name);
            dbContext.Encounters.Add(encounter);
            await dbContext.SaveChangesAsync();
            return CreatedAtAction(
                nameof(Get),
                new { id = encounter.Id },
                encounter.ToDTO()
            );
        }

        // // POST: api/Encounters
        // [HttpPost("{id}")]
        // public async Task<ActionResult> Join(int id, [FromBody]JoinEncounterRequest joinRequest)
        // {
        //     var encounter = await dbContext.Encounters.FindAsync(id);
        //     var player = await dbContext.Players.FindAsync(joinRequest.PlayerID);
        //     if(encounter is null || player is null)
        //         return NotFound();
        //     encounter.Join(id);
        //     await dbContext.SaveChangesAsync();
        //     return Ok();
        // }
    }

    [Route("api/Encounters/{encounterId:int}/[controller]")]
    [ApiController]
    public class CombatController(
        AppDbContext dbContext,
        IHubContext<EncounterHub, IEncounterClient> hubContext)
    {
        
    }
}
