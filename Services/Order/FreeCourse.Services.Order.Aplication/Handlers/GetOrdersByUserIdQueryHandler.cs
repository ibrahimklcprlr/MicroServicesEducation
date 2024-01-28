using Azure;
using FreeCourse.Services.Order.Aplication.Dtos;
using FreeCourse.Services.Order.Application.Queries;
using FreeCourse.Services.Order.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FreeCourse.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Internal.Mappers;
using FreeCourse.Services.Order.Aplication.Mapping;

namespace FreeCourse.Services.Order.Aplication.Handlers
{
    public class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, GetQueryResponse>
    {
        private readonly OrderDbContext _context;

        public GetOrdersByUserIdQueryHandler(OrderDbContext context)
        {
            _context = context;
        }
        public async Task<GetQueryResponse> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _context.Orders.Include(x => x.Orderitems).Where(x => x.BuyerId == request.UserId).ToListAsync();

            var response=new GetQueryResponse();
            
            if (!orders.Any())
            {
                response.response = Shared.Dtos.Response<List<OrderDto>>.Success(new List<OrderDto>(), 200);
                return response;
            }

            var ordersDto = ObjectMapper.Mapper.Map<List<OrderDto>>(orders);
            response.response = Shared.Dtos.Response<List<OrderDto>>.Success(ordersDto, 200);
            return response;
        }
    }
}
