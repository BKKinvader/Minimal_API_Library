﻿using System.Net;

namespace MinimalAPI_Book.Models
{
    public class APIResponse
    {
        public APIResponse()
        {
            ErrorMessages = new List<string>();
        }
        
        public bool IsSuccess { get; set; }
        public object Result { get; set; }
        public HttpStatusCode StatusCode { get; set; }

        public List<string> ErrorMessages { get; set; }





    }
}
