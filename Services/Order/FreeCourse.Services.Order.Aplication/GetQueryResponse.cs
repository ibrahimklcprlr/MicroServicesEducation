using FreeCourse.Services.Order.Aplication.Dtos;
using FreeCourse.Services.Order.Domain.OrderAggregate;
using FreeCourse.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Aplication
{
    public class GetQueryResponse
    {
        public Response<List<OrderDto>> response;
    }
}
