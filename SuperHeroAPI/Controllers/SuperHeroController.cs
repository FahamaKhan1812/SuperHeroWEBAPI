using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext context;

        public SuperHeroController(DataContext context)
        {
            this.context = context;  
        }
    
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>>Get()
        {   
            return Ok(await this.context.SuperHeros.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Get(int id)
        {   
            var hero = await this.context.SuperHeros.FindAsync(id);
            if (hero == null)
            {
                return BadRequest("Hero not found");
            }
            return Ok(hero);
        }
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            this.context.SuperHeros.Add(hero);
            await this.context.SaveChangesAsync();
            return Ok(await this.context.SuperHeros.ToListAsync());
        }
        [HttpPut]
        public async Task<ActionResult> UpdateHero(SuperHero request )
        {
            var dbHero = await this.context.SuperHeros.FindAsync(request.Id);
            if (dbHero == null)
            {
                return BadRequest("Hero not found");
            }
            dbHero.Name = request.Name;
            dbHero.FirstName = request.FirstName;
            dbHero.LastName = request.LastName;
            dbHero.Place = request.Place;

            await this.context.SaveChangesAsync();

            return Ok(await this.context.SuperHeros.ToListAsync());
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteHero(int id)
        {
            var dbhero = await this.context.SuperHeros.FindAsync(id);
            if (dbhero == null)
            {
                return BadRequest("Hero not found");
            }
            this.context.SuperHeros.Remove(dbhero);
            await this.context.SaveChangesAsync(true);
            return Ok(await this.context.SuperHeros.ToListAsync());
        }
    }
}
