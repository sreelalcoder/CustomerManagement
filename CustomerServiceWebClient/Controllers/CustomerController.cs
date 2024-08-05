using CustomerServiceWebClient.Models;
using CustomerServiceWebClient.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CustomerServiceWebClient.Controllers
{
    public class CustomerController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(IHttpClientFactory httpClientFactory, ILogger<CustomerController> logger)
        {
            _httpClient = httpClientFactory.CreateClient("CustomerClient");
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {

                _logger.LogInformation("Fetching all customers.");
                var response = await _httpClient.GetAsync("api/customer");
                var content = await response.Content.ReadAsStringAsync();

                var customers = JsonConvert.DeserializeObject<IEnumerable<CustomerDto>>(content);
                return View(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching customers.");
                return View("Error", new ErrorViewModel() { RequestId = HttpContext.Request.Headers.RequestId });
            }
        }

        public async Task<IActionResult> GetCustomer(int id)
        {
            try
            {
                _logger.LogInformation($"Fetching  customer id: {id}.");

                var response = await _httpClient.GetAsync($"api/customer/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    return NotFound();
                }

                var content = await response.Content.ReadAsStringAsync();
                var customer = JsonConvert.DeserializeObject<CustomerDto>(content);
                return PartialView("_CustomerDetails", customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while fetching customer id:{id}.");
                return View("Error", new ErrorViewModel() { RequestId = HttpContext.Request.Headers.RequestId });
            }
        }

        public IActionResult Create()
        {
            try
            {
                _logger.LogInformation($"Show customer create form.");
                return View();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in displaying create form");
                return View("Error", new ErrorViewModel() { RequestId = HttpContext.Request.Headers.RequestId });
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create(CustomerDto customer)
        {
            try
            {
                _logger.LogInformation($"Creating new customer{customer.Name}");
                if (!ModelState.IsValid)
                {
                    return View(customer);
                }

                var response = await _httpClient.PostAsJsonAsync("api/customer", customer);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return View(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in creating new  customer");
                return View("Error", new ErrorViewModel() { RequestId = HttpContext.Request.Headers.RequestId });
            }
        }

        public async Task<IActionResult> Update(int id)
        {
            try
            {
                _logger.LogInformation($"Updating  customer id: {id}");

                var response = await _httpClient.GetAsync($"api/customer/{id}");
                if (!response.IsSuccessStatusCode)
                {
                    return NotFound();
                }
                var content = await response.Content.ReadAsStringAsync();
                var customer = JsonConvert.DeserializeObject<UpdateCustomerDto>(content);
                return View(customer); // Assuming you have an Edit view
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in updating  customer id{id}");
                return View("Error", new ErrorViewModel() { RequestId = HttpContext.Request.Headers.RequestId });
            }

        }


        [HttpPost]
        public async Task<IActionResult> Update(UpdateCustomerDto customer)
        {
            try
            {
                _logger.LogInformation($"Updating  customer id: {customer.Id}");
                if (!ModelState.IsValid)
                {
                    return View(customer);
                }

                var response = await _httpClient.PutAsJsonAsync($"api/customer/{customer.Id}", customer);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return View(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in updating  customer id{customer.Id}");
                return View("Error", new ErrorViewModel() { RequestId = HttpContext.Request.Headers.RequestId });
            }
        }




        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting  customer id: {id}");
                var response = await _httpClient.DeleteAsync($"api/customer/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error in deleting  customer id{id}");
                return View("Error", new ErrorViewModel() { RequestId = HttpContext.Request.Headers.RequestId });
            }
        }
    }
}
