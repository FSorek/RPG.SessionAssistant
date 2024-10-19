using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazor.WebApp.Hubs;
using Microsoft.AspNetCore.Http;
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
        SessionAssistantDbContext dbContext,
        IHubContext<CombatHub, ICombatClient> hubContext)
        : ControllerBase
    {
        private static EncounterDTO _encounter = new EncounterDTO()
        {
            Id = 1,
            Combatants = new List<CombatantDTO>(),
            CurrentRound = 1
        };
        // GET: api/Encounters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EncounterDTO>>> GetEncounters()
        {
            return await dbContext.Encounters.ToListAsync();
        }

        // GET: api/Encounters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EncounterDTO>> GetEncounterDTO(int id)
        {
            var encounterDTO = await dbContext.Encounters.FindAsync(id);

            if (encounterDTO == null)
            {
                return NotFound();
            }

            return encounterDTO;
        }
        
        // POST: api/Encounters/join/5
        [HttpPost("join/{id}")]
        public async Task<ActionResult<CombatantDTO>> JoinEncounter(int id, [FromBody]CombatantDTO combatant)
        {
            var encounterDTO = await dbContext.Encounters.FindAsync(id);

            if (encounterDTO == null)
            {
                return NotFound();
            }
            dbContext.Entry(encounterDTO).State = EntityState.Modified;
            encounterDTO.Combatants.Add(combatant);
            await dbContext.SaveChangesAsync();
            await hubContext.Clients.All.UpdateEncounter();
            return combatant;
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

            dbContext.Entry(encounterDTO).State = EntityState.Modified;

            try
            {
                await dbContext.SaveChangesAsync();
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
            dbContext.Encounters.Add(encounterDTO);
            await dbContext.SaveChangesAsync();

            return CreatedAtAction("GetEncounterDTO", new { id = encounterDTO.Id }, encounterDTO);
        }

        // DELETE: api/Encounters/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEncounterDTO(int id)
        {
            var encounterDTO = await dbContext.Encounters.FindAsync(id);
            if (encounterDTO == null)
            {
                return NotFound();
            }

            dbContext.Encounters.Remove(encounterDTO);
            await dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool EncounterDTOExists(int id)
        {
            return dbContext.Encounters.Any(e => e.Id == id);
        }
    }
}
