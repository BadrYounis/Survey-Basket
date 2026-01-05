namespace SurveyBasket.Helpers;
public static class EmailBodyBuilder
{
    public static string GenerateEmailBody(string template, Dictionary<string, string> templateValues)
    {
        var templatePath = $"{Directory.GetCurrentDirectory()}/Templates/{template}.html";
        var streamReader = new StreamReader(templatePath);
        var body = streamReader.ReadToEnd();   //has all html that exists in template
        streamReader.Close();

        foreach (var value in templateValues)
            body = body.Replace(value.Key, value.Value);

        return body;
    }
}