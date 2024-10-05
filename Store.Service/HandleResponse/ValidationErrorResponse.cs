namespace Store.Service.HandleResponse
{
    public class ValidationErrorResponse : CustomExeption
    {
        public ValidationErrorResponse() : base(400)
        {
        }
        public IEnumerable<string> Errors { get; set; }
    }
}
