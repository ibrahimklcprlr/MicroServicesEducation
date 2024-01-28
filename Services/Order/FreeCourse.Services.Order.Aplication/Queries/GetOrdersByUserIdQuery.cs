using FreeCourse.Services.Order.Aplication;
using FreeCourse.Services.Order.Aplication.Dtos;
using FreeCourse.Shared.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Application.Queries
{
    public class GetOrdersByUserIdQuery : IRequest<GetQueryResponse>
    {
        public string UserId { get; set; }
    }
}

