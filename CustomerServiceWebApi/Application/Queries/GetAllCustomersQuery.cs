using CustomerServiceWebApi.Application.DTOs;
using MediatR;

namespace CustomerServiceWebApi.Application.Queries
{

    public class GetAllCustomersQuery : IRequest<IEnumerable<CustomerDto>>
    {


    }

}
