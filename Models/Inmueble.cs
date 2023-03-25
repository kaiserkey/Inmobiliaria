namespace Inmobiliaria.Models;

public class Inmueble
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}