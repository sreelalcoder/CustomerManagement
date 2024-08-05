using CustomerServiceWebApi.Application.Commands;
using CustomerServiceWebApi.Application.Interfaces;
using MediatR;

namespace CustomerServiceWebApi.Application.Handlers
{
    public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, Unit>
    {
        private readonly ICustomerRepository _repository;

        public UpdateCustomerHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            // cheking whether the customer id exist or   not
            if (!await _repository.ExistsAsync(request.Customer.Id))
            {
                throw new KeyNotFoundException($"Customer with ID {request.Customer.Id} not found.");
            }

            // fetch the customer and update.
            var customer = await _repository.GetByIdAsync(request.Customer.Id);
            customer.Id = request.Customer.Id;
            customer.Name = request.Customer.Name;
            customer.Email = request.Customer.Email;
            customer.PhoneNumber = request.Customer.PhoneNumber;

            await _repository.UpdateAsync(customer);
            return Unit.Value;
        }


    }
}
