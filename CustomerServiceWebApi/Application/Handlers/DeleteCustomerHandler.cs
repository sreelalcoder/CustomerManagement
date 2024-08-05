using CustomerServiceWebApi.Application.Commands;
using CustomerServiceWebApi.Application.Interfaces;
using MediatR;

namespace CustomerServiceWebApi.Application.Handlers
{
    public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, Unit>
    {
        private readonly ICustomerRepository _repository;

        public DeleteCustomerHandler(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            // cheking whether the customer id exist or   not
            if (!await _repository.ExistsAsync(request.Id))
            {
                throw new KeyNotFoundException($"Customer with ID {request.Id} not found.");
                
            }
            await _repository.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}
