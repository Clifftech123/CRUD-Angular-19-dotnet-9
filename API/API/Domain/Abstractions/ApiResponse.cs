namespace API.Domain.Abstractions
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public required string Message { get; set; }
        public required T Data { get; set; }
        public int StatusCode { get; set; }
        public int TotalCount { get; set; }

        public static ApiResponse<T> CreateSuccess(T data, string message = "Operation successful", int statusCode = 200)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data,
                StatusCode = statusCode
            };
        }

        public static ApiResponse<T> CreateFail(string message, int statusCode = 400)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Data = default,
                StatusCode = statusCode
            };
        }
    }
}
