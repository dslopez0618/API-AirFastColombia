using API_AIRFAST.Services.LoginService;

var builder = WebApplication.CreateBuilder(args);


// Habilitar CORS
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowAngularApp",
//        builder =>
//        {
//            builder.WithOrigins("http://localhost:4200") // URL de tu aplicación Angular
//                   .AllowAnyMethod()
//                   .AllowAnyHeader();
//        });
//});

// Agregar CORSS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});



// Add services to the container.

// Agregar CORSS
builder.Services.AddCors(options => 
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Agrego los servicios
builder.Services.AddScoped<ILoginService, LoginService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
