using OnlineEducationPlatform.DAL.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineEducationPlatform.BLL.handleresponse
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; } // Indicates if the operation was successful
        public string Message { get; set; } // Additional message, e.g., success or failure details
                                            // Additional message, e.g., success or failure details
        public T Data { get; set; } // The response data, can be enrollment or anything else

        // public Enrollment? enrollment { get; set; }=new Enrollment();
        public ServiceResponse()
        {
            Success = true; // By default, assume success unless otherwise indicated
        }

    }
    public class ServiceResponsewithstatus<T> : ServiceResponse<T>
    {
        public string Status { get; set; }


    }
}
