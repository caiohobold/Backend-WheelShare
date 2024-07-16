using System.Text.Json;
using System.Text;

public class Base64ImageMiddleware
{
    private readonly RequestDelegate _next;

    public Base64ImageMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Armazene o fluxo de resposta original
        var originalBodyStream = context.Response.Body;

        // Crie um novo MemoryStream para capturar a resposta
        using (var responseBody = new MemoryStream())
        {
            context.Response.Body = responseBody;

            await _next(context);

            // Leia a resposta do stream capturado
            responseBody.Seek(0, SeekOrigin.Begin);
            var responseText = await new StreamReader(responseBody).ReadToEndAsync();

            // Converta as imagens em base64
            var modifiedResponse = ConvertImagesToBase64(responseText);

            // Escreva a resposta modificada no fluxo de resposta original
            context.Response.Body = originalBodyStream;
            await context.Response.WriteAsync(modifiedResponse);
        }
    }

    private string ConvertImagesToBase64(string response)
    {
        var jsonDoc = JsonDocument.Parse(response);
        var root = jsonDoc.RootElement;

        using (var stream = new MemoryStream())
        {
            using (var writer = new Utf8JsonWriter(stream, new JsonWriterOptions { Indented = true }))
            {
                ConvertElement(writer, root);
            }

            return Encoding.UTF8.GetString(stream.ToArray());
        }
    }

    private void ConvertElement(Utf8JsonWriter writer, JsonElement element)
    {
        switch (element.ValueKind)
        {
            case JsonValueKind.Object:
                writer.WriteStartObject();
                foreach (var property in element.EnumerateObject())
                {
                    if (property.Name.StartsWith("foto", StringComparison.OrdinalIgnoreCase) && property.Value.ValueKind == JsonValueKind.String)
                    {
                        var base64 = property.Value.GetString();
                        if (!string.IsNullOrEmpty(base64) && !base64.StartsWith("data:image", StringComparison.OrdinalIgnoreCase))
                        {
                            base64 = $"data:image/jpeg;base64,{base64}";
                        }
                        writer.WriteString(property.Name, base64);
                    }
                    else
                    {
                        writer.WritePropertyName(property.Name);
                        ConvertElement(writer, property.Value);
                    }
                }
                writer.WriteEndObject();
                break;

            case JsonValueKind.Array:
                writer.WriteStartArray();
                foreach (var item in element.EnumerateArray())
                {
                    ConvertElement(writer, item);
                }
                writer.WriteEndArray();
                break;

            default:
                element.WriteTo(writer);
                break;
        }
    }
}
