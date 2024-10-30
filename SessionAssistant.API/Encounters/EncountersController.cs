using Blazor.WebApp.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SessionAssistant.API.Persistence;
using SessionAssistant.Shared.DTOs.Combat;

namespace SessionAssistant.API.Combat
{
    [Route("api/[controller]")]
    [ApiController]
    public class EncountersController(
        SessionAssistantReadDbContext readDbContext,
        SessionAssistantWriteDbContext writeDbContext)
        : ControllerBase
    {
        // GET: api/Encounters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EncounterDTO>> Get(int id)
        {
            var encounter = await readDbContext
                .Encounters
                .AsNoTracking()
                .Where(e => e.Id == id)
                .Include(e => e.Combatants)
                .FirstOrDefaultAsync();
            if (encounter == default(EncounterDTO))
            {
                return NotFound();
            }
            
            return encounter;
        }
        
        // POST: api/Encounters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        public record NewEncounterData(string Name);
        [HttpPost]
        public async Task<ActionResult<EncounterDTO>> PostEncounterDTO([FromBody]NewEncounterData encounterData)
        {
            var encounter = new Encounter();
            writeDbContext.Encounters.Add(encounter);
            await writeDbContext.SaveChangesAsync();
            return new EncounterDTO()
            {
                Id = encounter.Id,
                ActingInitiative = encounter.ActingInitiative,
                ActingPriority = encounter.ActingPriority,
                CurrentRound = encounter.CurrentRound,
                Combatants = new List<CombatantDTO>(),
            };
        }

        // DELETE: api/Encounters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEncounterDTO(int id)
        {
            var encounterDTO = await readDbContext.Encounters.FindAsync(id);
            if (encounterDTO == null)
            {
                return NotFound();
            }

            readDbContext.Encounters.Remove(encounterDTO);
            await readDbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
