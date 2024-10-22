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
        IHubContext<EncounterHub, IEncounterClient> hubContext)
        : ControllerBase
    {
        // GET: api/Encounters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EncounterDTO>> GetEncounterDTO(int id)
        {
            var encounterDTO = await readDbContext.Encounters.FindAsync(id);

            if (encounterDTO == null)
            {
                return NotFound();
            }

            return encounterDTO;
        }
        
        // PUT: api/Encounters/5/join
        [HttpPut("{id}/join")]
        public async Task<ActionResult<CombatantDTO>> JoinEncounter(int id, [FromBody]CombatantDTO combatant)
        {
            var encounterDTO = await readDbContext.Encounters.FindAsync(id);

            if (encounterDTO == null)
            {
                return NotFound();
            }
            encounterDTO.Combatants.Add(combatant);
            await readDbContext.SaveChangesAsync();
            await hubContext.Clients.All.UpdateEncounter();
            return combatant;
        }

        [HttpPut("{id}/endTurn")]
        public async Task<IActionResult> EndTurn(int id, [FromBody] CombatantDTO combatant)
        {
            var encounterDTO = await readDbContext.Encounters.FindAsync(id);
            var dbCombatant = encounterDTO.Combatants.SingleOrDefault(c => c.Id == combatant.Id);
            if (dbCombatant == null)
                return NotFound();
            
            return Ok();
        }
        
        // PUT: api/Encounters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEncounterDTO(int id, EncounterDTO encounterDTO)
        {
            if (id != encounterDTO.Id)
            {
                return BadRequest();
            }

            readDbContext.Entry(encounterDTO).State = EntityState.Modified;

            try
            {
                await readDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EncounterDTOExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Encounters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<EncounterDTO>> PostEncounterDTO(EncounterDTO encounterDTO)
        {
            readDbContext.Encounters.Add(encounterDTO);
            await readDbContext.SaveChangesAsync();

            return CreatedAtAction("GetEncounterDTO", new { id = encounterDTO.Id }, encounterDTO);
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

        private bool EncounterDTOExists(int id)
        {
            return readDbContext.Encounters.Any(e => e.Id == id);
        }
    }
}
