using CustomerServiceWebApi.Application.DTOs;
using MediatR;

namespace CustomerServiceWebApi.Application.Commands
{
    public class CreateCustomerCommand : IRequest<int>
    {
        public CreateCustomerDto Customer { get; set; }
    }
}
