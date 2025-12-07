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
        bool Post(T data, FileStream file);
        bool Post(T data, List<FileStream> file);
        //asyncronous 
        Task<bool> PostAsync(T data);
        Task<bool> PostAsync(T data, FileStream file);
        Task<bool> PostAsync(T data, List<FileStream> file);

    }
}
