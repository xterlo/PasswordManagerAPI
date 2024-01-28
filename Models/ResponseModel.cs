using Microsoft.AspNetCore.Mvc;

namespace PasswordManagerAPI.Models
{
    public class ResponseModel
    {
        public bool status { get; set; }
        public string message { get; set; }
        public object data { get; set; }

        public static ResponseModel BadRequest(string message,object data = null)
        {

            return new ResponseModel()
            {
                status = false,
                message = message,
                data = data
            };

        }

        public static ResponseModel Ok(string message="", object data = null)
        {

            return new ResponseModel()
            {
                status = true,
                message = message,
                data = data
            };

        }
    }

    
}
