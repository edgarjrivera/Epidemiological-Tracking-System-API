using EpidemiologicalTrackingApi.Contracts;
using EpidemiologicalTrackingApi.Data;
using EpidemiologicalTrackingApi.Exceptions;
using EpidemiologicalTrackingApi.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace EpidemiologicalTrackingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IndividualsController : ControllerBase
    {
        private readonly IIndividualService _individualService;

        public IndividualsController(IIndividualService individualService)
        {
            _individualService = individualService;
        }

        [HttpGet("Get")]
        public IActionResult GetIndividuals()
        {
            var individuals = DataStore.Individuals.Where(i => i.Diagnosed).ToList();
            return Ok(individuals);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetIndividual(int id)
        {
            try
            {
                var individual = _individualService.FindIndividualById(id);
                return Ok(individual);
            }
            catch (IndividualNotFoundException ex)
            {
                return NotFound(ex.Message); // Return 404 Not Found with a message
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while getting the individual.");
                return StatusCode(500, "An unexpected error occurred."); // Return 500 Internal Server Error
            }
        }


        [HttpPost("Create")]
        public IActionResult CreateIndividual(Individual individual)
        {
            var newId = DataStore.Individuals.Any() ? DataStore.Individuals.Max(i => i.Id) + 1 : 1;
            individual.Id = newId;

            DataStore.Individuals.Add(individual);

            Log.Information("Created new individual with ID {Id}.", individual.Id);
            return CreatedAtAction(nameof(GetIndividual), new { id = individual.Id }, new { success = true, id = individual.Id , value = individual});
        }

        [HttpPut("Update/{id}")]
        public IActionResult UpdateIndividual(int id, Individual updatedIndividual)
        {
            try
            {
                var individual = _individualService.FindIndividualById(id);

                _individualService.UpdateIndividualFields(individual, updatedIndividual);

                Log.Information("Updated individual with ID {Id}.", id);
                return NoContent();
            }
            catch (IndividualNotFoundException ex)
            {
                Log.Warning("Individual with ID {Id} not found for update: {Message}", id, ex.Message);
                return NotFound(ex.Message); // Return 404 Not Found with a message
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while updating the individual.", id);
                return StatusCode(500, "An unexpected error occurred."); // Return 500 Internal Server Error
            }

        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteIndividual(int id)
        {
            try
            {
                var individual = _individualService.FindIndividualById(id);
                DataStore.Individuals.Remove(individual);

                Log.Information("Deleted individual with ID {Id}.", id);
                return NoContent();
            }
            catch (IndividualNotFoundException ex)
            {
                Log.Warning("Individual with ID {Id} not found for deletion: {Message}", id, ex.Message);
                return NotFound(ex.Message); // Return 404 Not Found with a message
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An error occurred while deleting the individual with ID {Id}.", id);
                return StatusCode(500, "An unexpected error occurred."); // Return 500 Internal Server Error
            }
        }
    }
}
