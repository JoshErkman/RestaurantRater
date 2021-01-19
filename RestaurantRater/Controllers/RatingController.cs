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

        // create new ratings
        // POST api/Rating
        [HttpPost]
        public async Task<IHttpActionResult> CreateRating(Rating model)
        {
            // check if model is null
            if (model is null)
                return BadRequest("Your request body cannot be empty.");

            // check if model state is invalid
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // find the restaurant by the model Restaurant ID and see that it exist
            var restaurantEntity = await _context.Restaurants.FindAsync(model.RestaurantId);
            if (restaurantEntity is null)
                return BadRequest($"The target restaurant with the ID of {model.RestaurantId} does not exist.");

            // Create the rating

            // Add to the rating table
            // _context.Ratings.Add(model);

            // Add to the restaurant entity
            restaurantEntity.Ratings.Add(model);
            if (await _context.SaveChangesAsync() == 1)
                return Ok($"You rated restaurant {restaurantEntity.Name} successfully");

            return InternalServerError();
        }

        // Get a rating by its ID

        // Get ALL ratings

        // Get all ratings for a specific restaurant by the Restaurant ID

        // Update a rating

        // Delete a rating
    }
}
