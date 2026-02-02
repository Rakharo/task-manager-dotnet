using task_manager_dotnet.Services;
using task_manager_dotnet.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using task_manager_dotnet.Data;
using task_manager_dotnet.Repositories.Interfaces;
using task_manager_dotnet.Repositories;

var builder = WebApplication.CreateBuilder(args);


// Controllers
builder.Services.AddControllers();

// Repository
builder.Services.AddScoped<ITaskRepository, TaskRepository>();


// Service
builder.Services.AddScoped<ITaskService, TaskService>();


// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Database Configuration
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    );
});


var app = builder.Build();

// Swagger só em ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();

