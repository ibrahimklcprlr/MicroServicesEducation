using StackExchange.Redis;

namespace FreeCourses.Services.Basket.Services
{
    public class RedisServices
    {
        private readonly string _host;
        private readonly int _port;
        private ConnectionMultiplexer _multiplexer;
        public RedisServices(string host, int port)
        {
            _host = host;
            _port = port;
        }
        public void Connect() => _multiplexer = ConnectionMultiplexer.Connect($"{_host}:{_port}");
        public IDatabase GetDb(int db=1)=>_multiplexer.GetDatabase(db);
        
    }
}
