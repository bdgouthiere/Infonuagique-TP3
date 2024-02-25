
using ModernRecrut.Documents.API.Services;
using ModernRecrut.Documents.API.Interfaces;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Azure
builder.Services.AddAzureClients( configure =>
    {
        configure.AddBlobServiceClient(builder.Configuration.GetConnectionString("StorageConnectionString"));
});

builder.Services.AddScoped<IStorageServiceHelper, StorageServiceHelper>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDocumentsService, DocumentService>();

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

app.UseStaticFiles();

app.Run();
