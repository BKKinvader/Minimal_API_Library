using System.Net;

namespace MinimalAPI_Book.Models
{
    public class APIResponse
    {
        public List<string> ErrorMessages { get; set; }
        public bool IsSuccess { get; set; }
        public object Result { get; set; }
        public HttpStatusCode StatusCode { get; set; }




        public APIResponse()
        {
            ErrorMessages = new List<string>();
        }
        

        
    }
}
