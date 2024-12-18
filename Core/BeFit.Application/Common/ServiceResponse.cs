﻿using System.Text.Json.Serialization;

namespace BeFit.Application.Common
{
    public class ServiceResponse<T>
    {
        public T? Data { get; private set; }

        [JsonIgnore]
        public int StatusCode { get; private set; }
        [JsonIgnore]
        public bool IsSuccessful { get; private set; }

        public List<string> Errors { get; set; } = new();

        //static factory methods
        public static ServiceResponse<T> Success(T data, int StatusCode)
            => new() { Data = data, StatusCode = StatusCode, IsSuccessful = true };

        public static ServiceResponse<NoContent> Success(int StatusCode)
            => new() { StatusCode = StatusCode, Data = default, IsSuccessful = true };

        public static ServiceResponse<T> Failure(List<string> errors, int statusCode)
            => new() { Errors = errors, StatusCode = statusCode, IsSuccessful = false };

        public static ServiceResponse<T> Failure(string error, int statusCode)
            => new() { Errors = new() { error }, StatusCode = statusCode, IsSuccessful = false };


    }
}
