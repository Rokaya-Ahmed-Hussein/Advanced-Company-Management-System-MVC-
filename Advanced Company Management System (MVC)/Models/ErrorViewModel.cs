namespace Advanced_Company_Management_System__MVC_.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public int StatusCode { get; set; }
        public string? ErrorMessage { get; set; }
        public bool ShowDetails { get; set; }
        public string? StackTrace { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string GetUserFriendlyMessage()
        {
            return StatusCode switch
            {
                400 => "Bad Request - The request could not be understood by the server.",
                401 => "Unauthorized - You need to log in to access this resource.",
                403 => "Forbidden - You don't have permission to access this resource.",
                404 => "Not Found - The requested resource could not be found.",
                500 => "Internal Server Error - An unexpected error occurred on the server.",
                _ => "An error occurred while processing your request."
            };
        }
    }
}
