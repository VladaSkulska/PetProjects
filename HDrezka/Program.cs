using HDrezka.Repositories;
using HDrezka.Services;

var builder = WebApplication.CreateBuilder();

builder.Services.AddSingleton<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "HDrezka API");
    });
}

app.MapControllers();

app.Run();