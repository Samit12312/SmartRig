using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiClient
{
    internal class WebClient<T> : IWebClient<T>
    {
        HttpClient httpClient;
        HttpRequestMessage requestMessage;
        HttpResponseMessage responseMessage;

        UriBuilder uriBuilder; // מרכיב כתובת של בקשה 
        public WebClient()
        {
            this.httpClient = new HttpClient();
            this.uriBuilder = new UriBuilder();
        }
        public string Schema
        {
            set
            {
                this. uriBuilder.Scheme = value;
            }
        }
        public string Host
        {
            set
            {
                this. uriBuilder.Host = value;
            }
        }
        public int Port
        {
            set
            {
                this. uriBuilder.Port = value;
            }
        }
        public string Path
        {
            set
            {
                this. uriBuilder.Path = value;
            }
        }
        public void AddParameter(string key, string value)
        {
            if (this.uriBuilder.Query != string.Empty)
                this.uriBuilder.Query += "&" + key + "=" + value;
            else
                this.uriBuilder.Query += key + "=" + value;
        }
        public T Get()
        {
            using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, this.uriBuilder.Uri))
            {
                requestMessage.Method = HttpMethod.Get;// get שיטת שליחת בקשה
                requestMessage.RequestUri = this.uriBuilder.Uri; // מגדיר את כתובת הבקשה
                using (HttpResponseMessage respondMessage = this.httpClient.SendAsync(requestMessage).Result) //בעזרתhttpclient שולחים בקשה לשרת
                {
                    if (responseMessage.IsSuccessStatusCode == true)
                    {
                        string result = responseMessage.Content.ReadAsStringAsync().Result;
                        T data = JsonSerializer.Deserialize<T>(result);
                        return data;
                    }
                    else
                        return default(T);
                }
            }
        }
        public async Task<T> GetAsync()
        {
            using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, this.uriBuilder.Uri))
            {
                requestMessage.Method = HttpMethod.Get;// get שיטת שליחת בקשה
                requestMessage.RequestUri = this.uriBuilder.Uri; // מגדיר את כתובת הבקשה
                using (HttpResponseMessage respondMessage = await this.httpClient.SendAsync(requestMessage)) //בעזרתhttpclient שולחים בקשה לשרת
                {
                    if (responseMessage.IsSuccessStatusCode == true)
                    {
                        string result = await responseMessage.Content.ReadAsStringAsync();
                        T data = JsonSerializer.Deserialize<T>(result);
                        return data;
                    }
                    else
                        return default(T);
                }
            }
        }

        public bool Post(T data)
        {
            using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, this.uriBuilder.Uri))
            {
                requestMessage.Method = HttpMethod.Post;// post שיטת שליחת בקשה
                requestMessage.RequestUri = this.uriBuilder.Uri; // מגדיר את כתובת הבקשה
                string jsonData = JsonSerializer.Serialize(data);
                requestMessage.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                using(HttpResponseMessage responseMessage = this.httpClient.SendAsync(requestMessage).Result) 
                    return responseMessage.IsSuccessStatusCode;
            }
        }

        public bool Post(T data, List<FileStream> files)
        {
            using (HttpRequestMessage requestMessage = new HttpRequestMessage())
            {
                requestMessage.Method = HttpMethod.Post;// post שיטת שליחת בקשה
                requestMessage.RequestUri = this.uriBuilder.Uri; // מגדיר את כתובת הבקשה
                MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent();
                string jsonData = JsonSerializer.Serialize(data);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                multipartFormDataContent.Add(stringContent, "data");
                foreach (var file in files)
                {
                    StreamContent fileContent = new StreamContent(file);
                    multipartFormDataContent.Add(fileContent, "file", "fileName");
                }
                requestMessage.Content = multipartFormDataContent;
                using (HttpResponseMessage responseMessage = this.httpClient.SendAsync(requestMessage).Result)
                {
                    return responseMessage.IsSuccessStatusCode;
                }
            }
        }

        public bool Post(T data, FileStream file)
        {
            using (HttpRequestMessage requestMessage = new HttpRequestMessage())
            {
                requestMessage.Method = HttpMethod.Post;// post שיטת שליחת בקשה
                requestMessage.RequestUri = this.uriBuilder.Uri; // מגדיר את כתובת הבקשה
                MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent();
                string jsonData = JsonSerializer.Serialize(data);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                multipartFormDataContent.Add(stringContent, "data");
                StreamContent fileContent = new StreamContent(file);
                multipartFormDataContent.Add(fileContent, "file", "fileName");
                requestMessage.Content = multipartFormDataContent;
                using (HttpResponseMessage responseMessage = this.httpClient.SendAsync(requestMessage).Result)
                {
                    return responseMessage.IsSuccessStatusCode;
                }
            }
        }

        public async Task<bool> PostAsync(T data)
        {
            using (HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, this.uriBuilder.Uri))
            {
                requestMessage.Method = HttpMethod.Post;// post שיטת שליחת בקשה
                requestMessage.RequestUri = this.uriBuilder.Uri; // מגדיר את כתובת הבקשה
                string jsonData = JsonSerializer.Serialize(data);
                requestMessage.Content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                using (HttpResponseMessage responseMessage = await this.httpClient.SendAsync(requestMessage))
                    return responseMessage.IsSuccessStatusCode;
            }
        }

        public async Task<bool> PostAsync(T data, FileStream file)
        {
            using (HttpRequestMessage requestMessage = new HttpRequestMessage())
            {
                requestMessage.Method = HttpMethod.Post;// post שיטת שליחת בקשה
                requestMessage.RequestUri = this.uriBuilder.Uri; // מגדיר את כתובת הבקשה
                MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent();
                string jsonData = JsonSerializer.Serialize(data);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                multipartFormDataContent.Add(stringContent, "data");
                StreamContent fileContent = new StreamContent(file);
                multipartFormDataContent.Add(fileContent, "file", "fileName");
                requestMessage.Content = multipartFormDataContent;
                using (HttpResponseMessage responseMessage = await this.httpClient.SendAsync(requestMessage))
                {
                    return responseMessage.IsSuccessStatusCode;

                }
            }
        }

        public async Task<bool> PostAsync(T data, List<FileStream> files)
        {
            using (HttpRequestMessage requestMessage = new HttpRequestMessage())
            {
                requestMessage.Method = HttpMethod.Post;// post שיטת שליחת בקשה
                requestMessage.RequestUri = this.uriBuilder.Uri; // מגדיר את כתובת הבקשה
                MultipartFormDataContent multipartFormDataContent = new MultipartFormDataContent();
                string jsonData = JsonSerializer.Serialize(data);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                multipartFormDataContent.Add(stringContent, "data");
                foreach (var file in files)
                {
                    StreamContent fileContent = new StreamContent(file);
                    multipartFormDataContent.Add(fileContent, "file", "fileName");
                }
                requestMessage.Content = multipartFormDataContent;
                using (HttpResponseMessage responseMessage = await this.httpClient.SendAsync(requestMessage))
                {
                    return responseMessage.IsSuccessStatusCode;
                }
            }
        }
    }
}
