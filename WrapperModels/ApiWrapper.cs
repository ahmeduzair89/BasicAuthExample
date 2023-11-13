namespace BasicAuthExample.WrapperModels
{
    public class ApiWrapper
    {
        public bool Success { get; set; } = false;
        public dynamic Data { get; set; } = Array.Empty<string>();
        public string? Error { get; set; }


        public static ApiWrapper SetResponse(bool success, dynamic data, string error) {

            ApiWrapper   res = new ApiWrapper();
            res.Success = success;  
            res.Data = data;
            res.Error = error;
            return res;
                
        }
    }
}



