using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Dynamic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Serialization;

public static class HttpClientManager
{
    private static HttpClient Client = new HttpClient();

    /// <summary>
    /// 認証ありのWeb APIを利用する時に利用するパラメータです。
    /// </summary>
    public static string BearerValue { get; set; } = string.Empty;

    public static async Task<T> ExecuteGetJsonAsync<T>(string url)
    {
        //各種設定を行います。
        Client.DefaultRequestHeaders.Accept.Clear();
        Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", BearerValue);

        var response = await Client.GetAsync(url);
        return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
    }

    public static async Task<T> ExecuteGetXMLAsync<T>(string url)
    {
        var response = await Client.GetAsync(url);
        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
        return (T)xmlSerializer.Deserialize(await response.Content.ReadAsStreamAsync());
    }

    public static async Task<string> ExecuteGetStringAsync(string url)
    {
        Client.DefaultRequestHeaders.Accept.Clear();

        var response = await Client.GetStringAsync(url);
        return response;
    }

}