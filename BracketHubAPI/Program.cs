using BracketHubDatabase;
using Microsoft.EntityFrameworkCore;

var MyAllowSpecificOrigins = "_AllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
#if DEBUG
            policy.WithOrigins("https://localhost:7140");
#else
            policy.WithOrigins("https://gregerdesign.dk");
#endif
        });
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Factory instead og normal Context
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContextFactory<BrackethubContext>(option => option.UseSqlServer(connectionString));
//builder.Services.AddDbContext<BrackethubContext>(option => option.UseSqlServer("Server=SERVER2000;Database=BracketHub;TrustServerCertificate=True;Trusted_Connection=True;"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
