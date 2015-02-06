using System;
using System.Threading.Tasks;

namespace Gw2spidyApi.Network
{
    public interface IHttpRequest
    {
        Task<string> MakeJsonRequest(Uri uri);
    }
}
