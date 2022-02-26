using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon_Shakespear.Business.Interfaces
{
    public interface IHttpClientWrapper
    {
        Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content);
    }
}
