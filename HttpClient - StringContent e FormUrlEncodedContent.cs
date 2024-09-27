Fonte: https://charlesgonzaga.medium.com/formurlencodedcontent-vs-stringcontent-9173374863a7

FormUrlEncodedContent e StringContent são duas classes que permitem que você envie dados no corpo da solicitação HTTP. A principal diferença entre elas é o tipo de dados que podem ser enviados.

//########################################################################################################
//FormUrlEncodedContent
//########################################################################################################

É usada para enviar dados que estejam no formato x-www-form-urlencoded, geralmente usado em formulários HTML. Esse formato consiste em uma sequência de pares chave-valor separados por '&' e onde o espaço é substituído pelo sinal de '+' ou '%20'.
Por exemplo, para enviar uma solicitação com dados no formato x-www-form-urlencoded, você pode usar um dos seguintes códigos abaixo.
  
//========================================================================================================
//Exemplos de uso passando dados via Body:
//========================================================================================================
var payload = new Dictionary<string, string>
{
    { "username", "john" },
    { "password", "doe" }
};

using var httpClient = new HttpClient();
var content = new FormUrlEncodedContent(payload);

var response = await httpClient.PostAsync(endpoint, content);

//========================================================================================================
//Exemplos de uso passando dados via Header, método SendAsync:
//========================================================================================================
var payload = new Dictionary<string, string>
{
    { "username", "john" },
    { "password", "doe" }
};

using var httpClient = new HttpClient();

var request = new HttpRequestMessage(HttpMethod.Post, endpoint);
request.Headers.Add("username", payload["username"]);
request.Headers.Add("password", payload["password"]);

var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

//========================================================================================================
//Exemplos de uso passando dados via Header, método PostAsync:
//========================================================================================================
var payload = new Dictionary<string, string>
{
    { "username", "john" },
    { "password", "doe" }
};

using var httpClient = new HttpClient();

var content = new FormUrlEncodedContent(new Dictionary<string, string>());
content.Headers.Add("username", payload["username"]);
content.Headers.Add("password", payload["password"]);

var response = await httpClient.PostAsync(endpoint, content);

//========================================================================================================
//Exemplos de uso passando dados via Query Params:
//========================================================================================================
var payload = new Dictionary<string, string>
{
    { "username", "john" },
    { "password", "doe" }
};

using var httpClient = new HttpClient();

var uriBuilder = new UriBuilder(endpoint)
{
    Query = string.Join("&", payload.Select(x => $"{Uri.EscapeDataString(x.Key)}={Uri.EscapeDataString(x.Value)}"))
};

var response = await httpClient.GetAsync(uriBuilder.Uri);

//########################################################################################################
//StringContent
//########################################################################################################

É usada para enviar dados em outros formatos, como JSON ou XML. Ela permite que você especifique o tipo de conteúdo usando o parâmetro mediaType. Nos exemplo a seguir, mediaType é definido como "application/json". Para enviar uma solicitação com dados no formato JSON, você pode usar um dos seguintes códigos abaixo.:

//========================================================================================================
//Exemplos de uso passando dados via Body:
//========================================================================================================

var payload = new { username = "john", password = "doe" };
var json = JsonSerializer.Serialize(payload);

var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

using var httpClient = new HttpClient();
var response = await httpClient.PostAsync(endpoint, httpContent);

//========================================================================================================
//Exemplos de uso passando dados via Header:
//========================================================================================================

var payload = new { username = "john", password = "doe" };

using var httpClient = new HttpClient();

var content = new StringContent(string.Empty);
content.Headers.Add("username", payload.username);
content.Headers.Add("password", payload.password);

var response = await httpClient.PostAsync(endpoint, content);

//========================================================================================================
//Exemplos de uso passando dados via Query Params:
//========================================================================================================
var httpClient = new HttpClient();

var payload = new { username = "john", password = "doe" };

var queryString = new Dictionary<string, string>()
{
    { "username", payload.username },
    { "password", payload.password }
};

var uriBuilder = new UriBuilder(endpoint)
{
    Query = string.Join("&", queryString.Select(x => $"{Uri.EscapeDataString(x.Key)}={Uri.EscapeDataString(x.Value)}"))
};

var response = await httpClient.GetAsync(uriBuilder.Uri);


//########################################################################################################
//Conclusão
//########################################################################################################
Este artigo se concentrou em discutir as diferenças entre o uso de FormUrlEncodedContent e StringContent em solicitações HTTP usando o HttpClient padrão do C#. Embora essas abordagens possam funcionar bem para aplicações simples, elas podem não ser escaláveis ou eficientes em aplicações maiores e mais complexas.

Nesses casos, a abordagem recomendada pela Microsoft é o uso do HttpClientFactory. O HttpClientFactory oferece várias vantagens em relação ao HttpClient padrão, incluindo o gerenciamento automático de conexões HTTP, o suporte para autenticação de nível de sistema e a possibilidade de configurar políticas de retentativas de solicitações, entre outras coisas.

Em resumo, embora as abordagens de FormUrlEncodedContent e StringContent possam funcionar bem para demonstrações de código simples, recomenda-se o uso do HttpClientFactory para aplicativos maiores e mais complexos. A adoção desta abordagem pode melhorar significativamente a escalabilidade, segurança e eficiência de seu aplicativo.

Para saber mais a respeito de HttpClientFactory, recomendo ler o artigo: https://charlesgonzaga.medium.com/httpclientfactory-55dd337bb023
