namespace ForDevs.IntegrationTests.Models
{
    public class ApiResponseError
    {
        public bool suceess { get; set; }
        public int status { get; set; }
        public List<string> errors { get; set; }
    }

    public class ApiResponseSucess
    {
        public bool suceess { get; set; }
        public int status { get; set; }
        public ApiModelToken model { get; set; }
    }

    public class ApiModelToken
    {
        public string accessToken { get; set; }
    }
}
