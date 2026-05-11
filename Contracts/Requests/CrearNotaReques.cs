namespace DevKickstart.Api.Contracts.Requests;

public class CrearNotaRequest
{
    public string Titulo { get; set; } = string.Empty;

    public string Contenido { get; set; } = string.Empty;
}