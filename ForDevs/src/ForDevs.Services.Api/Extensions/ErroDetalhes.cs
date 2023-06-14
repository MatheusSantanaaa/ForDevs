using System.Text.Json;

namespace ForDevs.Services.Api.Extensions
{
    public class ErroDetalhes
    {
        public int status { get; set; }
        public string error { get; set; } = null;
        public Exception Exception { get; set; } = null;

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
