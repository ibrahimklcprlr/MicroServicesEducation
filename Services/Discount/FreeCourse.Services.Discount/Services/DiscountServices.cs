using Dapper;
using FreeCourse.Shared.Dtos;
using Npgsql;
using System.Data;

namespace FreeCourse.Services.Discount.Services
{
    public class DiscountServices : IDiscountServices
    {

        private readonly IConfiguration _configuration;
        private readonly IDbConnection _dbConnection;

        public DiscountServices(IConfiguration configuration)
        {
            _configuration = configuration;
            _dbConnection = new NpgsqlConnection(_configuration.GetConnectionString("PostgreSql"));
        }

        public async Task<Response<NoContent>> Creat(Models.Discount discount)
        {
            var status = await _dbConnection.ExecuteAsync("INSERT INTO discount(userid,rate,code) VALUES(@UserId,@Rate,@Code)", discount);
            if (status > 0)
            {
                return Response<NoContent>.Success(204);

            }
            return Response<NoContent>.Fail("An error accured while Adding", 500);
        }

        public async Task<Response<NoContent>> Delete(int id)
        {
            var status = await _dbConnection.ExecuteAsync("delete from discount where id=@id", new { id = id });
            if (status > 0)
            {
                return Response<NoContent>.Success(200);

            }
            return Response<NoContent>.Fail("An error accured while deleting", 500);
        }

        public async Task<Response<List<Models.Discount>>> GetAll()
        {
            var discounts = await _dbConnection.QueryAsync<Models.Discount>("select * from discount") ;
            return Response<List<Models.Discount>>.Success(discounts.ToList(), 200);
        }

        public async Task<Response<Models.Discount>> GetByCodeAndUserId(string code, string userId)
        {
            var discount = (await _dbConnection.QueryAsync<Models.Discount>("select * from discount where userid=@Userid and code=@Code", new { Userid = userId, Code = code })).FirstOrDefault() ;
            return discount == null ? Response<Models.Discount>.Fail("Discount not found", 404) : Response<Models.Discount>.Success(discount, 200);
        }

        public async Task<Response<Models.Discount>> GetById(int Id)
        {
            var discount =(await _dbConnection.QueryAsync<Models.Discount>("Select * from discount  where Id=@Id", new {Id=Id})).SingleOrDefault();
            if (discount!=null)
            {
                return Response<Models.Discount>.Success(discount, 200);
            }
            return Response<Models.Discount>.Fail("Discount not Fount", 404);
        }

        public async Task<Response<NoContent>> Update(Models.Discount discount)
        {
            var status = await _dbConnection.ExecuteAsync("update  discount set userid=@UserId ,rate=@Rate,code=@Code where id=@id)", new {id=discount.Id,Code=discount.Code,Rate=discount.Rate,UserId=discount.UserId});
            if (status > 0)
            {
                return Response<NoContent>.Success(200);

            }
            return Response<NoContent>.Fail("An error accured while Update", 500);
        }
    }
}
