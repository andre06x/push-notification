using Microsoft.EntityFrameworkCore;
using PushNotification.Configuracao;
using WebPush;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<WebPushClient>();

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

string databaseUrl = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT_CONNECTSTRING");

if(databaseUrl == null)
{
    databaseUrl = builder.Configuration.GetConnectionString("DefaultConnection");
}


builder.Services.AddDbContext<Contexto>(options =>
    options.UseNpgsql(databaseUrl, npgsqlOptions =>
        npgsqlOptions.MigrationsAssembly(typeof(Program).Assembly.FullName)
    )
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("corsapp");

app.UseAuthorization();

app.MapControllers();

app.Run();
