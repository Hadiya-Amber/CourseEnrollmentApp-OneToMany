
using ManyToMany.Models;
using ManyToMany.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<IOperationTransient, Operation>(); // new EACH injection
builder.Services.AddScoped<IOperationScoped, Operation>();       // one per HTTP request
builder.Services.AddSingleton<IOperationSingleton, Operation>(); // one for app lifetime
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BookDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("conn")));
builder.Services.AddControllers()
        .AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

        });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();  // forces https
app.UseRouting();           // matches routes to endpoints

app.UseAuthorization();     // applies auth rules

app.MapControllers();       // executes controller actions

app.Run();
