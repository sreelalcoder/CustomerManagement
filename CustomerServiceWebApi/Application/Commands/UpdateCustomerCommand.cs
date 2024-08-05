using CustomerServiceWebApi.Application.DTOs;
using MediatR;

namespace CustomerServiceWebApi.Application.Commands
{
    public class UpdateCustomerCommand : IRequest<Unit>
    {
        public UpdateCustomerDto Customer { get; set; }
    }
}
