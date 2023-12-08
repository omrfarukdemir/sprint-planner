namespace SprintPlanner.Infrastructure.Extensions;

public static class FlurlResponseExtensions
{
    public static async Task<T?> GetXmlAsync<T>(this IFlurlResponse response) where T : new()
    {
        var xmlSerializer = new XmlSerializer(typeof(T));
        
        return (T)xmlSerializer.Deserialize(await response.GetStreamAsync())!;
    }
}