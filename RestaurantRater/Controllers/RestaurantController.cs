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
        public async Task<IHttpActionResult> PostRestaurant([FromBody]Restaurant model)
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
        public async Task<IHttpActionResult> GetById([FromUri]int id)
        {
            Restaurant restaurant = await _context.Restaurants.FindAsync(id);

            if (restaurant != null)
                return Ok(restaurant);

            return NotFound();
        } 


    }
}
