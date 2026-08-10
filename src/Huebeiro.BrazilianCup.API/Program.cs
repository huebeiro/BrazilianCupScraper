using Huebeiro.BrazilianCup.API.Configuration;
using Huebeiro.BrazilianCup.API.Filters;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices(builder.Configuration); // Injetando dependências
builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await scope.ServiceProvider
        .GetRequiredService<ApplicationInitializer>()
        .InitializeAsync(); // Iniciando Scraper 
}

app.UseMiddleware<ExceptionHandlingFilter>(); // Filtro para exceções customizadas
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();// Configuração Swagger para confirmação visual da subida da API

app.Run();
