//1. Usings to work Entity Framework 
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using UniversityApiBackend;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Services;

var builder = WebApplication.CreateBuilder(args);

//1.Internalization /LOCALIZATION 
builder.Services.AddLocalization(options => options.ResourcesPath = "ResourcesUniversity");
//2. Connection with SQL Server Express

const string CONNECTIONNAME = "UniversityDb";
var connectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);

//3. Add Context
builder.Services.AddDbContext<UniversityDBContext>(options => options.UseSqlServer(connectionString));

//7. Add Service of JWT Autorization
builder.Services.AddJwtTokenServices(builder.Configuration);


// Add services to the container.

builder.Services.AddControllers();

//4. Add Custom Services (folder services)
builder.Services.AddScoped<IStudentsService, StudentsService>();
//TODO : Add the rest  of services

//8.Add authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserOnlyPolicy", policy => policy.RequireClaim("UserOnly", "User1"));
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//9.Config Swagger to take care of Autorization od JWT
builder.Services.AddSwaggerGen(options =>
{
    //We define the security for authorization 
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization Header using Bearer Scheme"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
          {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[]{}
        }
        
    }); 
}

);

// 5. CORS Configuration 
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy", builder =>
     {
         builder.AllowAnyOrigin();
         builder.AllowAnyMethod();
         builder.AllowAnyHeader();
     });

});

//
var app = builder.Build();


//2. Internalization/ SUPPORTED CULTURES
var supportedCultures = new[] { "en-US", "es-ES", "de-DE" };//USA ENGLISH, SPAIN SPANISH AND German Germany
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])  // english by default
    .AddSupportedCultures(supportedCultures) // add all supported cultures
    .AddSupportedUICultures(supportedCultures);// add supported cultures to UI

//3. Internalization ADD LOCALIZATION to APP
app.UseRequestLocalization(localizationOptions);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//6. Tell app to use Cors

app.UseCors("CorsPolicy");

app.Run();
