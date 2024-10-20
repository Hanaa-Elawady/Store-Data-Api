namespace Store.Service.HandleResponse
{
    public class CustomExeption : Response
    {
        public CustomExeption(int _statusCode , string? _details = null, string? _message = null) : base(_statusCode, _message)
        {
            Details = _details;
        }
        public string? Details {  get; set; }
    }
}
