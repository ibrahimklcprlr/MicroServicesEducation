using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FreeCourse.Shared.Dtos
{
    public class Response<TResponse>
    {
        public TResponse Data { get;private set; }
        [JsonIgnore]
        public int StatusCode { get; private set; }
        [JsonIgnore]
        public bool IsSuccess { get; private set; }
        public List<string>? Errors { get; set; }
        public static Response<TResponse> Success(TResponse data ,int statusCode)
        => new Response<TResponse>{Data=data,StatusCode=statusCode, IsSuccess=true};
        public static Response<TResponse> Success(int statusCode)
        => new Response<TResponse> { Data = default, StatusCode = statusCode, IsSuccess = true };
         public static Response<TResponse> Fail(List<string> errors ,int statusCode) 
        =>new Response<TResponse> { Errors=errors,StatusCode=statusCode,IsSuccess=false};
        public static Response<TResponse> Fail(string error, int statusCode)
        => new Response<TResponse> { Errors = new List<string>() { error}, StatusCode = statusCode, IsSuccess = false };

    }
}
