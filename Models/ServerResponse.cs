namespace webapi.Models
{
    /// <summary>
    /// The server response class.
    /// Used by all controllers to build a response for the request.
    /// </summary>
    public class ServerResponse
    {

        /// <summary>
        /// The response status.
        /// </summary>
        public enum ResponseStatus { Success = 0, Error = 1 }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        public Dictionary<string, object> Payload { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        public ResponseStatus Status { get; set; } = ResponseStatus.Success;

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; } = null;
    }
}
