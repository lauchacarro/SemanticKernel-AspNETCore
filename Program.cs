using Microsoft.SemanticKernel.ChatCompletion;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Nuestro metodo de extensión para realizar configuraciones de SemanticKernel
builder.Services.AddSemanticKernel();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/chat", async (ChatMessage request, IChatCompletionService chatService) =>
{

    ChatHistory chatMessages = new ChatHistory();

    chatMessages.AddUserMessage(request.Message);

    var chatResponse = await chatService.GetChatMessageContentAsync(chatMessages);

    return Results.Ok(new ChatMessage(chatResponse.ToString()));
})
.WithName("Chat")
.WithOpenApi();

app.Run();

record ChatMessage(string Message);
