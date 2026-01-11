using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiClient
{
    internal interface IWebClient<T>
    {

        //syncrocnous methods
        T Get();
        Task<T> GetAsync();
        bool Post(T data);
        bool Post(T data, Stream file);
        bool Post(T data, List<Stream> file);
        //asyncronous 
        Task<bool> PostAsync(T data);
        Task<bool> PostAsync(T data, Stream file);
        Task<bool> PostAsync(T data, List<Stream> file);

    }
}
