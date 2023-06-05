using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using System.Reflection;

#pragma warning disable CS8619
#pragma warning disable CS8765
#pragma warning disable CS8601

namespace webapi.BaseControllers
{
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// Overrides Ok Response.
        /// </summary>
        /// <param name="value">The value object.</param>
        /// <returns>An OkObjectResult.</returns>
        public override OkObjectResult Ok(object value)
        {
            return base.Ok(new ServerResponse()
            {

                Status = ServerResponse.ResponseStatus.Success,
                Payload = value.GetType()
                                 .GetProperties(BindingFlags.Instance |
                                                BindingFlags.Public)
                                  .ToDictionary(prop => prop.Name,
                                                prop => prop.GetValue(value, null))
            });
        }

        /// <summary>
        /// Overrides Ok Response.
        /// </summary>
        /// <param name="message">The message to be returned.</param>
        /// <returns>An OkObjectResult.</returns>
        protected OkObjectResult Ok(string message)
        {
            return base.Ok(new ServerResponse()
            {
                Status = ServerResponse.ResponseStatus.Success,
                Message = message
            });
        }

        /// <summary>
        /// Overrides Ok Response.
        /// </summary>
        /// <param name="value">The value object.</param>
        /// <param name="message">The message to be returned.</param>
        /// <returns>An OkObjectResult.</returns>
        protected OkObjectResult Ok(object value, string message)
        {
            return base.Ok(new ServerResponse()
            {
                Status = ServerResponse.ResponseStatus.Success,
                Message = message,
                Payload = value.GetType().GetProperties(BindingFlags.Instance |
                                                        BindingFlags.Public)
                                         .ToDictionary(prop => prop.Name,
                                                       prop => prop.GetValue(value, null))
            });
        }

        /// <summary>
        /// Overrides Bad Request.
        /// </summary>
        /// <param name="value">The value object.</param>
        /// <returns>A BadRequestObjectResult.</returns>
        public override BadRequestObjectResult BadRequest(object value)
        {
            if (value.GetType() != typeof(string))
            {
                Dictionary<string, object> errorDictionary = value.GetType()
                                                                    .GetProperties(BindingFlags.Instance |
                                                                                   BindingFlags.Public)
                                                                      .ToDictionary(prop => prop.Name,
                                                                        prop => prop.GetValue(value, null));

                return base.BadRequest(new ServerResponse()
                {
                    Status = ServerResponse.ResponseStatus.Error,
                    Message = errorDictionary["Message"].ToString(),
                });
            }
            return base.BadRequest(new ServerResponse()
            {
                Status = ServerResponse.ResponseStatus.Error,
                Message = value.ToString(),
            });
        }
    }
}
