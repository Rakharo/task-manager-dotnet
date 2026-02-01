using task_manager_dotnet.Services;
using task_manager_dotnet.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using task_manager_dotnet.Data;

var builder = WebApplication.CreateBuilder(args);


// Controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITaskService, TaskService>();

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

