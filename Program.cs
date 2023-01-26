using Microsoft.EntityFrameworkCore;
using tech_test_payment_api.Context;
using tech_test_payment_api.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ApiContext>(options => options.UseInMemoryDatabase("DB"));


builder.Services.AddControllers();

// Rota documentação Swagger/OpenAPI https://swagger.io/solutions/api-documentation/


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
