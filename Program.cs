using ContactsApi.Middleware;
using ContactsApi.Services;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder =>
        {
            builder
                .AllowAnyOrigin() 
                .AllowAnyMethod() 
                .AllowAnyHeader();
        });
});
builder.Services.AddControllers();
builder.Services.AddSingleton<IContactService, ContactService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contact API V1");
     });
}
app.UseCors("AllowAllOrigins");
app.MapControllers();
app.Run();
