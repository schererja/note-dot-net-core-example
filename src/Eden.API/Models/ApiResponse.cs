using Microsoft.AspNetCore.Mvc;
using System.Configuration;

namespace Eden.API.Models
{
    public class ApiResponse<T> 
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Result { get; set; }


    }
}

