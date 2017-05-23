using System.Threading.Tasks;
using StackExchange.Redis;

namespace IBDatabase.Redis
{
    public interface IReciever<T>
    {
        IDatabase Db { get; set; }
        ISubscriber Sub { get; set; }
        void Subscribe();       
        void UpdateRedis(string message);
    }
}