using Building.API.Defined.Enums;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Building.API.Types
{
    public static class Responses
    {
        public static StatusResponse Error(ResponseCodes code, string desc)
        {
            return new StatusResponse
            {
                Code = code,
                Desc = desc
            };
        }

        public static StatusResponse Success(string desc)
        {
            return new StatusResponse
            {
                Code = ResponseCodes.Success,
                Desc = desc
            };
        }
    }

    public class StatusResponse
    {
        public ResponseCodes Code { get; set; }
        public string Desc { get; set; }
    }

    public class BadRequestResponse : StatusResponse
    {
        public Dictionary<string, string[]> Errors { get; set; }

        public BadRequestResponse(ActionContext context)
        {
            this.Code = ResponseCodes.BadRequest;
            this.Desc = "Sai dữ liệu đầu vào";
            this.getErrors(context);
        }

        private void getErrors(ActionContext context)
        {
            this.Errors = new Dictionary<string, string[]>();

            foreach (var keyModelStatePair in context.ModelState)
            {
                string prop = keyModelStatePair.Key;
                var errors = keyModelStatePair.Value.Errors;
                if (errors != null && errors.Count > 0)
                {
                    if (errors.Count == 1)
                    {
                        var errorMessage = errors[0].ErrorMessage;
                        Errors.Add(prop, new[] { errorMessage });
                    }
                    else
                    {
                        var errorMessages = new string[errors.Count];
                        for (var i = 0; i < errors.Count; i++)
                        {
                            errorMessages[i] = errors[i].ErrorMessage;
                        }

                        Errors.Add(prop, errorMessages);
                    }
                }
            }
        }
    }
}
