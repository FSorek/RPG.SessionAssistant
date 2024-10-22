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
        SessionAssistantWriteDbContext writeDbContext,
        IHubContext<EncounterHub, IEncounterClient> hubContext)
        : ControllerBase
    {
        // GET: api/Encounters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EncounterDTO>> GetEncounterDTO(int id)
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
        // PUT: api/Encounters/5/join
        [HttpPut("{id}/join")]
        public async Task<ActionResult<CombatantDTO>> JoinEncounter(int id, [FromBody]CombatantDTO combatantData)
        {
            var encounter = await writeDbContext.Encounters.FindAsync(id);
            if (encounter == null)
            {
                return NotFound();
            }
            var combatant = encounter.EnterCombat(combatantData.Name, combatantData.Initiative, combatantData.Attacks);
            await writeDbContext.SaveChangesAsync();
            await hubContext.Clients.All.UpdateEncounter();
            combatantData.Id = combatant.Id;
            return combatantData;
        }
        public record EndTurnPayload(int CombatantId, bool UsedMultiAttack);
        [HttpPut("{id}/endTurn")]
        public async Task<IActionResult> EndTurn(int id, [FromBody]EndTurnPayload payload)
        {
            var encounter = await writeDbContext.Encounters.FindAsync(id);
            if (encounter == null)
            {
                return NotFound();
            }
            
            encounter.EndTurn(payload.CombatantId, payload.UsedMultiAttack);
            await writeDbContext.SaveChangesAsync();
            await hubContext.Clients.All.UpdateEncounter();
            return Ok();
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
