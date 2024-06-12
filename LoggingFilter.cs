using Microsoft.SemanticKernel;

public class LoggingFilter : IFunctionInvocationFilter
{
  public async Task OnFunctionInvocationAsync(FunctionInvocationContext context, Func<FunctionInvocationContext, Task> next)
  {
    Console.ForegroundColor = ConsoleColor.Magenta;
    Console.WriteLine($"Function {context.Function.Name} invoked with arguments {string.Join(", ", context.Arguments)}");

    Console.ResetColor();
    await next(context);

    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"Function {context.Function.Name} returned {context.Result}");
    Console.ResetColor();
  }
}