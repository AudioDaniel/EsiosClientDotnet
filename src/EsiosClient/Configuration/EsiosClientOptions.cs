namespace EsiosClient;

public class EsiosClientOptions
{
    /// <summary>
    /// Base address of the ESIOS API. Defaults to https://api.esios.ree.es/
    /// </summary>
    public string BaseAddress { get; set; } = "https://api.esios.ree.es/";

    /// <summary>
    /// The Personal Access Token to authenticate against the ESIOS API.
    /// </summary>
    public string PersonalToken { get; set; } = string.Empty;
}
