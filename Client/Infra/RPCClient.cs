using MediatR;
using System.Net.Http.Json;

namespace Client.Infra;

internal static class RPCClient
{
    internal const string BaseUrl = "https://localhost:5000/";
    private static HttpClient Client => new HttpClient()
    {
        BaseAddress = new Uri(BaseUrl)
    };

    internal async static Task<TReturn> HandleAsync<TReturn>(IRequest<TReturn> request)
    {
        var res = await Client.PostAsJsonAsync(request.GetType().Name, request);
        return (await res.Content.ReadFromJsonAsync<TReturn>())!;
    }
}
