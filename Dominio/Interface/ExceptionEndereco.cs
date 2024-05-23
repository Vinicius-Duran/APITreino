namespace Dominio.Interface
{
    public class ExceptionEndereco : Exception
    {
        public int StatusCode { get; set; }

        public ExceptionEndereco(string message, int statusCode) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
