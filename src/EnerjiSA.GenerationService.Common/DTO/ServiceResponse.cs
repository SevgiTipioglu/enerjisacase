using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnerjiSA.GenerationService.Common
{
    public class ServiceResponse<T> : ServiceResponse
    {

        protected ServiceResponse(bool success, string message, T data)
        {
            ValidationErrors = new List<string>();
            base._success = success;
            Message = message;
            Data = data;
        }

        protected ServiceResponse(bool success, string message, IList<string> validationErrors, T data)
        {
            ValidationErrors = new List<string>();
            _success = success;
            Message = message;
            ValidationErrors = validationErrors;
            Data = data;
        }




        public T Data { get; set; }


        public static ServiceResponse<T> SuccessResult(T data, string message = "")
        {
            return new ServiceResponse<T>(true, message, data);
        }
    }





    public class ServiceResponse
    {
        protected bool _success = false;
        protected ServiceResponse()
        {
            ValidationErrors = new List<string>();
        }

        protected ServiceResponse(bool success)
        {
            ValidationErrors = new List<string>();
            _success = success;
        }
        protected ServiceResponse(bool success, string message)
        {
            ValidationErrors = new List<string>();
            _success = success;
            Message = message;
        }
        protected ServiceResponse(bool success, string message, IList<string> validationErrors)
        {
            ValidationErrors = new List<string>();
            _success = success;
            Message = message;
            ValidationErrors = validationErrors;
        }
        public bool Success
        {
            get
            {
                if (ValidationErrors.Any())
                    return false;
                else return _success;
            }
            set
            {
                _success = value;
            }
        }
        public void AddError(string error)
        {
            ValidationErrors.Add(error);
        }
        public string Message { get; set; }
        public IList<string> ValidationErrors { get; set; }

        public static ServiceResponse SuccessResult(string message = "")
        {
            return new ServiceResponse(true, message);
        }
        public static ServiceResponse ErrorResult(string message = "")
        {
            return new ServiceResponse(false, message);
        }
    }
}
