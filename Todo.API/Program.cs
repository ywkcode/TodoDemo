using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Todo.Api.UnitOfWork;
using Todo.API.Context;
using Todo.API.Extensions;
using Todo.API.Repository;
using Todo.API.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//CodeFirst
builder.Services.AddDbContext<TodoContext>(option =>
{
    var connectionString = builder.Configuration.GetConnectionString("ToDoConnection");
    option.UseSqlite(connectionString);
});
builder.Services.AddUnitOfWork<TodoContext>()
     .AddCustomRepository<ToDo, ToDoRepository>()
     .AddCustomRepository<Memo, MemoRepository>()
     .AddCustomRepository<User, UserRepository>();

builder.Services.AddTransient<IToDoService, ToDoService>();
#region Ìí¼ÓAutoMapper
var automapperConfog = new MapperConfiguration(config =>
{
    config.AddProfile(new AutoMapperProFile());
}); 
builder.Services.AddSingleton(automapperConfog.CreateMapper());
#endregion

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
