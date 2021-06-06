using RestaurantRater.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace RestaurantRater.Controllers
{
    public class RatingController : ApiController
    {
        private readonly RestaurantDbContext _context = new RestaurantDbContext();
        [HttpPost]
        public async Task<IHttpActionResult> CreateRating([FromBody] Rating model)
        {
            if (model is null)
                return BadRequest("Request body cannot be null");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var restaurantEntity = await _context.Restaurants.FindAsync(model.RestaurantId);

            if (restaurantEntity is null)
                return BadRequest($"The target restaurant with the Id of {model.RestaurantId} does not exist.");

            // _context.Ratings.Add(model);
            restaurantEntity.Ratings.Add(model);
            if (await _context.SaveChangesAsync() == 1)
                return Ok($"You succesfully rated {restaurantEntity.Name}");

            return InternalServerError();
        }
    }
}
