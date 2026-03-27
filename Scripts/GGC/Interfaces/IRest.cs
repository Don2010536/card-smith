using System.Net.Http;
using System.Threading.Tasks;

namespace GGC.Interfaces
{
    public interface IRest<T0, T1>
    {
        public Task<HttpResponseMessage> AsyncDelete(int index);
        public Task<HttpResponseMessage> AsyncCreate(T0 data);
        public Task<HttpResponseMessage> AsyncUpdate(int index, T0 data);
        public Task<T1> AsyncGet();
    }
}
