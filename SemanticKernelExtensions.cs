using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Embeddings;
#pragma warning disable SKEXP0001, SKEXP0003, SKEXP0010, SKEXP0011, SKEXP0050, SKEXP0052


public static class SemanticKernelExtensions
{
    public static IServiceCollection AddSemanticKernel(this IServiceCollection services)
    {

        var kernel = Kernel.CreateBuilder()
            .AddOpenAIChatCompletion(
                    modelId: "phi3:3.8b",
                    endpoint: new Uri("http://localhost:11434/"),
                    apiKey: "non required")
            .Build();

        var chatService = kernel.GetRequiredService<IChatCompletionService>();
        var textService = kernel.GetRequiredService<ITextEmbeddingGenerationService>();

        services.AddSingleton<Kernel>(kernel);

        services.AddSingleton<IChatCompletionService>(chatService);

        services.AddSingleton<ITextEmbeddingGenerationService>(textService);

        return services;
    }
}

