using CustomerServiceWebApi.Application.Commands;
using CustomerServiceWebApi.Application.Interfaces;
using CustomerServiceWebApi.Domain.Models;
using MediatR;

namespace CustomerServiceWebApi.Application.Handlers
{
    public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, int>
    {
        private readonly ICustomerRepository _repository;

        public CreateCustomerHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = new Customer
            {
                Name = request.Customer.Name,
                Email = request.Customer.Email,
                PhoneNumber = request.Customer.PhoneNumber
            };

            await _repository.AddAsync(customer);
            return customer.Id;
        }
    }

}
