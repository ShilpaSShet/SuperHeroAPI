using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SuperAPI.Controllers
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

        //Get method of SuperHeroAPI
        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {

            return Ok(await context.SuperHeroes.ToListAsync());
        }

        //Get method by using id
        [HttpGet("{id}")]

        public async Task<ActionResult<List<SuperHero>>> Get(int id)
        {
            var hero = await context.SuperHeroes.FindAsync(id);

            if (hero == null)
                return BadRequest("Hero Not Found!!!");
            return Ok(hero);
        }

        //Post/Add method of SuperHeroAPI
        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero(SuperHero hero)
        {
            context.SuperHeroes.Add(hero);
            await context.SaveChangesAsync();
            return Ok(await context.SuperHeroes.ToListAsync());
        }

        //Update method of SuperHeroAPI
        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> Update(SuperHero request)
        {

            var dbHero = await context.SuperHeroes.FindAsync(request.Id);

            if (dbHero == null)
                return BadRequest("Hero Not Found!!!");

            dbHero.Name = request.Name;
            dbHero.FirstName = request.FirstName;
            dbHero.LastName = request.LastName;
            dbHero.Place = request.Place;

            await context.SaveChangesAsync();

            return Ok(await context.SuperHeroes.ToListAsync());
        }

        //Delete method by using SuperHero id
        [HttpDelete("{id}")]

        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var dbHero = await context.SuperHeroes.FindAsync(id);

            if (dbHero == null)
                return BadRequest("Hero Not Found!!!");



            context.SuperHeroes.Remove(dbHero);
            await context.SaveChangesAsync();
            return Ok(await context.SuperHeroes.ToListAsync());
        }


    
}
}
