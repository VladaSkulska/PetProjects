using HDrezka.Middlewares;
using HDrezka.Repositories;

var builder = WebApplication.CreateBuilder();

builder.Services.AddSingleton<IMovieRepository, MovieRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<MovieMiddleware>();

app.MapControllers();

app.Run();