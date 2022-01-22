using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using ReportsDAL.Entities;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ReportsServerApi.PublicAccess
{
    public class ServerApi : IServerApi
    {
        private string _baseUrl = "https://localhost:5001/";
        private const string EmployeePage = "employee/";
        private const string TaskPage = "task/";
        
        public Employee[] GetAllEmployees()
        {
            var request = new RequestBuilder()
                .FullUrl(_baseUrl + EmployeePage)
                .MethodType(HttpMethod.Get)
                .GetHttpWebRequest();
            var jsonResponse = GetJsonStringFromResponse(request.GetResponse());
            return DeserializeObject<Employee[]>(jsonResponse);

        }

        public Employee GetEmployeeById(uint id)
        {
            var request = new RequestBuilder()
                .FullUrl(_baseUrl + EmployeePage + "?" + "id=" + id)
                .MethodType(HttpMethod.Get)
                .GetHttpWebRequest();
            var jsonResponse = GetJsonStringFromResponse(request.GetResponse());
            var answer = DeserializeObject<Employee[]>(jsonResponse);
            if (answer.Length > 1)
                throw new ServerApiException(answer.Length + " entities with id" + id);
            return answer.First();
        }

        public Employee[] GetEmployeeByName(string name)
        {
            var request = new RequestBuilder()
                .FullUrl(_baseUrl + EmployeePage + "?" + "name=" + name)
                .MethodType(HttpMethod.Get)
                .GetHttpWebRequest();
            var jsonResponse = GetJsonStringFromResponse(request.GetResponse());
            return DeserializeObject<Employee[]>(jsonResponse);
        }
        
        public Employee CreateEmployee(string name, long chiefId = -1)
        {
            var request = new RequestBuilder()
                .FullUrl(_baseUrl + EmployeePage + "?" + "name=" + name + "&" + "chiefId=" + chiefId)
                .MethodType(HttpMethod.Post)
                .GetHttpWebRequest();
            
            string jsonEmployee  = GetJsonStringFromResponse(request.GetResponse());
            return DeserializeObject<Employee>(jsonEmployee);
            
        }

        public Employee DeleteEmployee(uint id)
        {
            var request = new RequestBuilder()
                .FullUrl(_baseUrl + EmployeePage + "?" + "id=" + id)
                .MethodType(HttpMethod.Delete)
                .GetHttpWebRequest();

            string jsonAnswer = GetJsonStringFromResponse(request.GetResponse());
            return DeserializeObject<Employee>(jsonAnswer);
        }

        public Task[] GetAllTasks()
        {
            var request = new RequestBuilder()
                .FullUrl(_baseUrl + TaskPage)
                .MethodType(HttpMethod.Get)
                .GetHttpWebRequest();
            string jsonAnswer = GetJsonStringFromResponse(request.GetResponse());
            return DeserializeObject<Task[]>(jsonAnswer);
        }

        public Task GetTaskById(uint id)
        {
            var request = new RequestBuilder()
                .FullUrl(_baseUrl + TaskPage + "?id=" + id)
                .MethodType(HttpMethod.Get)
                .GetHttpWebRequest();
            string jsonAnswer = GetJsonStringFromResponse(request.GetResponse());
            var obj = DeserializeObject<Task[]>(jsonAnswer);
            if (obj.Length > 1)
            {
                throw new ServerApiException(obj.Length + " entities with id" + id);
            }

            return obj.First();
        }

        public Task[] GetTaskByCreationDate(DateTime creationDate)
        {
            var request = new RequestBuilder()
                .FullUrl(_baseUrl + TaskPage + "?=creationDate=" + creationDate)
                .MethodType(HttpMethod.Get)
                .GetHttpWebRequest();
            string jsonAnswer = GetJsonStringFromResponse(request.GetResponse());
            return DeserializeObject<Task[]>(jsonAnswer);
        }

        public Task CreateTask(uint responsibleEmployeeId, string taskText)
        {
            var request = new RequestBuilder()
                .FullUrl(_baseUrl + TaskPage + "?=responsibleEmployeeId" + responsibleEmployeeId + "&testText" +
                         taskText)
                .MethodType(HttpMethod.Post)
                .GetHttpWebRequest();
            string jsonAnswer = GetJsonStringFromResponse(request.GetResponse());
            return DeserializeObject<Task>(jsonAnswer);
        }

        public Task UpdateTask(uint id, TaskStatus status, string taskText, uint responsibleEmployee)
        {
            var request = new RequestBuilder()
                .FullUrl(_baseUrl + TaskPage + "?id=" + id + "&status=" + status + "&taskText=" + taskText +
                         "&responsibleEmployee=" + responsibleEmployee)
                .MethodType(HttpMethod.Put)
                .GetHttpWebRequest();
            string jsonAnswer = GetJsonStringFromResponse(request.GetResponse());
            return DeserializeObject<Task>(jsonAnswer);
        }


        private string GetJsonStringFromResponse(WebResponse response)
        {
            
            Stream receiveStream = (response as HttpWebResponse).GetResponseStream();
            StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
            return readStream.ReadToEnd();
        }

        private T DeserializeObject<T>(string from)
        {
            return JsonConvert.DeserializeObject<T>(from);
        }

        private class RequestBuilder
        {
            private string _fullUrl;
            private string _methodType;

            public RequestBuilder FullUrl(string url)
            {
                _fullUrl = url;
                return this;
            }

            public RequestBuilder MethodType(string methodType)
            {
                _methodType = methodType;
                return this;
            }

            public HttpWebRequest GetHttpWebRequest()
            {
                
                var request = HttpWebRequest.CreateHttp(_fullUrl);
                request.Method = _methodType;
                request.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
                return request;
            }
        }

        private static class HttpMethod
        {
            public const string Get = "GET";
            public const string Put = "PUT";
            public const string Post = "POST";
            public const string Delete = "DELETE";
        }
    }
}