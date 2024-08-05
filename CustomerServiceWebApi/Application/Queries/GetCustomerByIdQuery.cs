using CustomerServiceWebApi.Application.DTOs;
using MediatR;

namespace CustomerServiceWebApi.Application.Queries
{
    public class GetCustomerByIdQuery : IRequest<CustomerDto>
    {
        public int Id { get; set; }

        public GetCustomerByIdQuery(int id)
        {
            Id = id;
        }
    }
}
