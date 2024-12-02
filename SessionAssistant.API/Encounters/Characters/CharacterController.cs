using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SessionAssistant.API.Persistence;
using SessionAssistant.Shared.DTOs.Combat.Requests;

namespace SessionAssistant.API.Encounters
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CharacterController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Character/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var character = await _context.Characters
                .Where(c => c.Id == id)
                .Include(c => c.KnownAbilities)
                .FirstOrDefaultAsync();

            if (character == null)
            {
                return NotFound();
            }

            return Ok(character.ToDTO());
        }

        //POST: api/Character
        [HttpPost]
        public async Task<ActionResult> PostCharacter(CreateCharacterRequest createReqeust)
        {
            var (name, attacks) = createReqeust;
            var character = new Character(name, attacks);
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();
        
            return CreatedAtAction("Get", 
                new { id = character.Id }, 
                character.ToDTO());
        }

        [HttpPost("{id}/abilities")]
        public async Task<ActionResult> LearnAbility(int id, LearnAbilityRequest learnAbilityRequest)
        {
            int abilityId = learnAbilityRequest.AbilityId;
            Character? character = await _context
                .Characters
                .Where(c => c.Id == id)
                .Include(c => c.KnownAbilities)
                .SingleOrDefaultAsync();
            Ability? ability = await _context.Abilities
                .Where(a => a.Id == abilityId)
                .SingleOrDefaultAsync();
            if (character == null || ability == null)
            {
                return NotFound();
            }
            character.Learn(ability);
            var response = character.ToDTO();
            return Accepted(response);
        }
    }
}
