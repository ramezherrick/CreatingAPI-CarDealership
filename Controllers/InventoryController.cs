using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using capestone_CreateAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace capestone_CreateAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly CardealershipDbContext _context;

        public InventoryController(CardealershipDbContext context)
        {
            _context = context;
        }

        //GET: api/inventory
        [HttpGet]
        public async Task<ActionResult<List<Inventory>>> GetInventory()
        {
            var cars = await _context.Inventory.ToListAsync();
            return cars;
        }
        //GET: api/inventory/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Inventory>> GetCar(int id)
        {
            var car = await _context.Inventory.FindAsync(id);
            return car;
        }
        //GET: api/inventory/GetCarsByMakeAndYear?make=Honda&&year=1990
        [Route("[action]")]
        [HttpGet]
        public async Task<List<Inventory>> GetCarsByMakeAndYear([FromQuery] string make, [FromQuery] string year)
        {
            List<Inventory> cars = new List<Inventory>();

            cars = await _context.Inventory.Where(x => x.Make == make && x.YearMade == year).ToListAsync();


            return cars;
        }
        //Delete: api/inventory/1
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCar(int id)
        {
            var car = await _context.Inventory.FindAsync(id);

            if (car == null)
            {
                return NotFound();
            }
            else
            {
                _context.Inventory.Remove(car);
                await _context.SaveChangesAsync();
                return NoContent(); //return 204 -- successful
            }
        }
        //ADD: api/inventory
        [HttpPost]
        public async Task<ActionResult<Inventory>> AddCar(Inventory newCar)
        {
            if (ModelState.IsValid)
            {
                await _context.Inventory.AddAsync(newCar);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetCar), new { id = newCar.Id }, newCar); //returns 201 -- standard response
            }
            else
            {
                return NotFound();
            }
        }
        //UPDATE
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCar (int id, Inventory updatedCar)
        {
            if(!ModelState.IsValid || id!=updatedCar.Id)
            {
                return BadRequest();
            }
            else
            {
                Inventory dbCar = _context.Inventory.Find(id);
                dbCar.Make = updatedCar.Make;
                dbCar.Model = updatedCar.Model;
                dbCar.YearMade = updatedCar.YearMade;
                dbCar.Color = updatedCar.Color;

                _context.Entry(dbCar).State = EntityState.Modified;
                _context.Update(dbCar);
                await _context.SaveChangesAsync();

                return NoContent();
            }
        }
    }
}
