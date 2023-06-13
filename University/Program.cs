//Usings to work with entity frameworks
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using University;
using University.DataAccess;
using University.Services;
//13. Use Serilog to log events
using Serilog;


var builder = WebApplication.CreateBuilder(args);

//14. Config Serilog
builder.Host.UseSerilog((hostBuilderCtx, loggerConf) =>
{
    loggerConf
    .WriteTo.Console()
    .WriteTo.Debug()
    .ReadFrom.Configuration(hostBuilderCtx.Configuration);
});


//2. Connection with SQL server Express

const string CONNECTIONNAME = "UniversityDB";
var connectionString = builder.Configuration.GetConnectionString(CONNECTIONNAME);

//3. Add context
builder.Services.AddDbContext<UniversityDBContext>(options => options.UseSqlServer(connectionString));


// 7. Add Service of JWT Autorization
builder.Services.AddJwtTokenServices(builder.Configuration);

// 10. Add localization service
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");


// Add services to the container.

builder.Services.AddControllers();

//4. Add Custom Services (folder Services)

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IChapterService, ChapterService>();

//8. Add Authorization Policy
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserOnlyPolicy", policy => policy.RequireClaim("UserOnly", "User1"));
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//9. Config Swagger to take care of Autorization of JWT
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


//5. CORS Configuration
builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: "CorsPolicy", builder =>
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyMethod();
            builder.AllowAnyHeader();
        });
    }
);


var app = builder.Build();

//11. Supported cultures
var supportedCultures = new[] { "en-US", "es-ES", "fr-FR", "de-DE" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

//12.Add Localization to app
app.UseRequestLocalization(localizationOptions);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//15. Tell app to use Serilog
app.UseSerilogRequestLogging();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//6. Tell app to use CORS
app.UseCors("CorsPolicy");


app.Run();
