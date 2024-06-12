using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

var builder = Kernel.CreateBuilder()
  .AddOpenAIChatCompletion("gpt-4o", Environment.GetEnvironmentVariable("OPENAI_API_KEY"));
var kernel = builder.Build();
kernel.ImportPluginFromType<MyPlugins>();
kernel.FunctionInvocationFilters.Add(new LoggingFilter());

string botPrefix = "[Bot] ";

var prompt = @"
You are an assistant which creates onepage html fullscreen slides for a presentation. Everything must be contained in the html - the css, the javascript, and the content.
Instead of images, if a sprite or icon is required, use a fitting emoji.

---

You must use the SaveContentToFile function to create a file from the provided html and the OpenFile function to open the created file using the full path.
";

var chatHistory = new ChatHistory();
var chatCompletionService = kernel.GetRequiredService<IChatCompletionService>();
var executionSettings = new OpenAIPromptExecutionSettings { MaxTokens = 4000, ToolCallBehavior = ToolCallBehavior.AutoInvokeKernelFunctions };

chatHistory.AddSystemMessage(prompt);

while (true)
{
  Console.Write("Enter a message: ");
  string input = Console.ReadLine();
  chatHistory.AddUserMessage(input);
  var response = await chatCompletionService.GetChatMessageContentAsync(chatHistory, executionSettings, kernel);
  chatHistory.AddAssistantMessage(response.ToString());
  string echoedMessage = botPrefix + response;
  Console.WriteLine(echoedMessage);
}

public class MyPlugins
{
  [KernelFunction, Description("Get the current weather in the specified city.")]
  [return: Description("The current weather in the specified city.")]
  public string GetWeather([Description("the city to query on")] string city)
  {
    var openWeatherMapApiKey = Environment.GetEnvironmentVariable("OPEN_WEATHER_MAP_API_KEY");
    var openWeatherMapApiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={openWeatherMapApiKey}";
#pragma warning disable SYSLIB0014 // Type or member is obsolete
    var response = new WebClient().DownloadString(openWeatherMapApiUrl);
#pragma warning restore SYSLIB0014 // Type or member is obsolete


    return response;
  }

  [KernelFunction, Description("Get the user location.")]
  [return: Description("The user location.")]
  public async Task<string> GetLocation()
  {
    // use ip-api.com to fetch the location
    var url = "http://ip-api.com/json";
    using (var client = new WebClient())
    {
      var response = await client.DownloadStringTaskAsync(url);
      return response;
    }
  }

  [KernelFunction, Description("Save the provided content, e.g. a presentation, to a file")]
  [return: Description("The full file path of the saved HTML file.")]
  public string SaveContentToFile([Description("The HTML content to save to a file.")] string htmlContent)
  {
    string tempFileName = Path.GetRandomFileName();
    string tempDirectory = Path.GetTempPath();
    string filePath = Path.Combine(tempDirectory, Path.ChangeExtension(tempFileName, ".html"));
    File.WriteAllText(filePath, htmlContent);
    return filePath;
  }

  [KernelFunction, Description("Open the provided file using the Mac OS 'open' command.")]
  public void OpenFile([Description("The file name to open.")] string fileName)
  {
    Process.Start("open", fileName);
  }

}