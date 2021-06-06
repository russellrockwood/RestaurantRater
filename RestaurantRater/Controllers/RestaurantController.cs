using RestaurantRater.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRater.Controllers
{
    public class RestaurantController : ApiController
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext();

        [HttpPost]
        public async Task<IHttpActionResult> PostRestaurant([FromBody] Restaurant model)
        {
            if (model is null)
                return BadRequest("Request body cannot be empty.");

            if (ModelState.IsValid)
            {
                _context.Restaurants.Add(model);
                await _context.SaveChangesAsync();
                return Ok("Restaurant Created");
            }

            return BadRequest(ModelState);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            List<Restaurant> restaurants = await _context.Restaurants.ToListAsync();
            return Ok(restaurants);

        }

        [HttpGet]
        public async Task<IHttpActionResult> GetById([FromUri] int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);

            if (restaurant != null)
                return Ok(restaurant);

            return NotFound();
        }

        [HttpPut]
        public async Task<IHttpActionResult> UpdateRestaurant([FromUri] int id, [FromBody] Restaurant updatedRestaurant)
        {
            if (id != updatedRestaurant?.Id)
                return BadRequest("Ids do not match.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Restaurant restaurantToUpdate = await _context.Restaurants.FindAsync(id);

            if (restaurantToUpdate == null)
                return NotFound();

            restaurantToUpdate.Name = updatedRestaurant.Name;
            restaurantToUpdate.Address = updatedRestaurant.Address;

            _context.Restaurants.Add(restaurantToUpdate);
            await _context.SaveChangesAsync();
            return Ok("Restaurant was updated");
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteRestaurant([FromUri] int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);

            if (restaurant == null)
                return NotFound();

            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
            return Ok("Restaurant deleted");
        }
    }
}
