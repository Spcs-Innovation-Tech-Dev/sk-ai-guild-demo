# Semantic Kernel Demonstration

This repository contains a demonstration of using Semantic Kernel to create an interactive console application that generates single-page HTML presentations. The application utilizes OpenAI's GPT-4 language model for generating the content and incorporates custom plugins to enhance its functionality.

## Prerequisites

Before running the application, ensure that you have the following:

- .NET 8.0 SDK installed
- OpenAI API key (set as an environment variable named `OPENAI_API_KEY`)
- OpenWeatherMap API key (set as an environment variable named `OPEN_WEATHER_MAP_API_KEY`)

## Getting Started

1. Clone the repository:

```
git clone https://github.com/yourusername/SkGuildDemo.git
```

2. Navigate to the project directory:

```
cd SkGuildDemo
```

3. Build the project:

```
dotnet build
```

4. Run the application:

```
dotnet run
```

## Usage

Once the application is running, you can interact with it through the console. Enter a message to prompt the assistant to generate a single-page HTML presentation. The assistant will create the HTML file and open it using the default browser on your Mac OS system.

The application uses custom plugins to provide additional functionality:

- `GetWeather`: Retrieves the current weather for a specified city using the OpenWeatherMap API.
- `GetLocation`: Fetches the user's location based on their IP address using the ip-api.com service.
- `SaveContentToFile`: Saves the generated HTML content to a file and returns the full file path.
- `OpenFile`: Opens the specified file using the Mac OS 'open' command.

## Logging

The application includes a logging filter (`LoggingFilter`) that logs the function invocations and their results to the console. The function name, arguments, and return values are displayed in different colors for better readability.

## Contributing

Feel free to submit pull requests or open issues if you find any bugs or have suggestions for improvements.

## License

This project is licensed under the [MIT License](LICENSE).

## Acknowledgments

- [Semantic Kernel](https://github.com/microsoft/semantic-kernel) - The framework used for building the application.
- [OpenAI](https://www.openai.com/) - The provider of the GPT-4 language model used for generating content.
- [OpenWeatherMap](https://openweathermap.org/) - The API used for retrieving weather data.
- [ip-api.com](http://ip-api.com/) - The service used for fetching user location based on IP address.