using Apontamento.API.Configurations.AutoMapper;
using Apontamento.API.Configurations.CrossCuttingIoC;
using Apontamento.Core.API.Environment;
using Apontamento.Identidade.CrossCuttingIoC;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(DomainToViewModelProfile), typeof(InputModelToDomainProfile));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

EnvironmentNativeInjector.ConfigurarVariaveisAmbiente(builder.Services, builder.Configuration);

var environment = (IEnvironment)builder.Services.BuildServiceProvider().GetService(typeof(IEnvironment));

CoreNativeInjection.ConfigurarDependenciasCore(builder.Services, environment);
IdentidadeNativeInjection.ConfigurarDependenciasIdentidade(builder.Services, environment);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
