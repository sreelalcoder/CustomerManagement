using CustomerServiceWebApi.Application.Commands;
using CustomerServiceWebApi.Application.DTOs;
using CustomerServiceWebApi.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CustomerServiceWebApi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CustomerController> _logger;


        public CustomerController(IMediator mediator, ILogger<CustomerController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        /// <summary>
        /// Get all customers
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAll()
        {
            try
            {
                _logger.LogInformation($"Fetching all customers");

                var customers = await _mediator.Send(new GetAllCustomersQuery());
                return Ok(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching customers.");
                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Get customer by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerDto>> GetById(int id)
        {
            try
            {
                _logger.LogInformation($"Fetch customer details , id{id}");

                var customer = await _mediator.Send(new GetCustomerByIdQuery(id));
                if (customer == null)
                {
                    return NotFound();
                }
                return Ok(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching customer id: {id}.");
                return NotFound();
            }
        }

        /// <summary>
        /// Create a new customer
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<int>> Create([FromBody] CreateCustomerDto dto)
        {
            try
            {
                _logger.LogInformation($"Creating new customer, name {dto.Name}");

                var id = await _mediator.Send(new CreateCustomerCommand { Customer = dto });
                return CreatedAtAction(nameof(GetById), new { id }, id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while creating customer");
                return NotFound();
            }
        }

        /// <summary>
        /// Update an existing customer
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCustomerDto dto)
        {
            try
            {
                _logger.LogInformation($"Updating customer id{id}");

                if (id != dto.Id)
                {
                    return BadRequest();
                }
                await _mediator.Send(new UpdateCustomerCommand { Customer = dto });
                return NoContent();

            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, $"Error occurred while updating customer, id{id}");

                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Delete a customer
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting customer, id{id}");

                await _mediator.Send(new DeleteCustomerCommand(id));
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting customer ,id{id}");

                return NotFound(ex.Message);
            }
        }
    }
}
