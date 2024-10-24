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
        public async Task<ActionResult<EncounterDTO>> GetEncounter(int id)
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
        public record JoinEncounterPayload(string Name, int Initiative, int Attacks, int UserId);
        [HttpPut("{id}/join")]
        public async Task<ActionResult<CombatantDTO>> JoinEncounter(int id, [FromBody]JoinEncounterPayload payload)
        {
            var encounter = await writeDbContext.Encounters.FindAsync(id);
            if (encounter == null)
            {
                return NotFound();
            }
            var combatant = encounter.EnterCombat(payload.Name, payload.Initiative, payload.Attacks, payload.UserId);
            await writeDbContext.SaveChangesAsync();
            await hubContext.Clients.All.UpdateEncounter();
            return new CombatantDTO()
            {
                Id = combatant.Id,
                Attacks = combatant.Attacks,
                Initiative = combatant.Initiative,
                Name = combatant.Name,
                ActPriority = combatant.ActPriority,
                HasCompletedRound = combatant.HasCompletedRound,
                EncounterId = encounter.Id,
            };
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
    
    [Route("api/[controller]")]
    [ApiController]
    public class CombatantsController(
        SessionAssistantReadDbContext readDbContext,
        SessionAssistantWriteDbContext writeDbContext)
    {
        [HttpGet("{userId}")]
        public async Task<ActionResult<CombatantDTO?>> GetCombatantForUser(int userId)
        {
            var user = await readDbContext.Combatants.FirstOrDefaultAsync(c => c.UserId == userId);
            if (user is not null)
                return new JsonResult(user);
            return new NotFoundResult();
        }
    }
}
