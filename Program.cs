using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PokemonReviewApp;
using PokemonWebApp.Configurations;
using PokemonWebApp.Data;
using PokemonWebApp.Interfaces;
using PokemonWebApp.Repository;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddTransient<Seed>();
builder.Services.AddControllers().AddJsonOptions(x => 
                 x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IPokemonRepository, PokemoRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();  
builder.Services.AddScoped<ICountryRepository, CountryRepository>(); 
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>(); 
builder.Services.AddScoped<IReviewerRepository, ReviewerRepository>();
// Learn more about configuring Swagger/OpenAPI at htt  ps://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//var connectionString = builder.Configuration.GetSection("ConnectionStrings:DefautConnection");
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer("Data Source=bkostadinov-nb\\SQLEXPRESS;Initial Catalog=pokemonreview;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
});

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection(key: "Jwt"));

//CG P T
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,   
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = "Boris",
//        ValidAudience = "Boris",
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Boris123"))
//    };
//});

//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pokemon API", Version = "v1" });

//    var securityScheme = new OpenApiSecurityScheme
//    {
//        Name = "Authorization",
//        Description = "JWT Authorization header using the Bearer scheme",
//        In = ParameterLocation.Header,
//        Type = SecuritySchemeType.ApiKey,
//        Scheme = "Bearer",
//        BearerFormat = "JWT"
//    };
//    c.AddSecurityDefinition("Bearer", securityScheme);
//    var securityRequirment = new OpenApiSecurityRequirement
//    {
//        {securityScheme, new[] {"Bearer"} }
//    };
//    c.AddSecurityRequirement(securityRequirment);
//});


//Insomnia
//builder.Services.AddAuthentication(configureOptions: options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//    .AddJwtBearer(jwt =>
//    {
//        var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection(key: "Jwt:Key").Value);

//        jwt.SaveToken = true;
//        jwt.TokenValidationParameters = new TokenValidationParameters()
//        {
//            ValidateIssuerSigningKey = true,
//            IssuerSigningKey = new SymmetricSecurityKey(key),
//            ValidateIssuer = false, // for dev
//            ValidateAudience = false, // for dev
//            RequireExpirationTime = false, // for dev -- need to be updated when refrest token is out
//            ValidateLifetime = true
//        };
//    });
//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedEmail = false)
//   .AddEntityFrameworkStores<DbContext>();



//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "dotnetClaimAuthorization", Version = "v1" });
//});

//////This is the base swagger / Not working the verification
//builder.Services.AddSwaggerGen(options =>
//{
//    options.AddSecurityDefinition("oauth2", new ApiKeyScheme
//    {
//        Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
//        In = "header",
//        Name = "Authorization",
//        Type = "apiKey"
//    });
//    options.OperationFilter<SecurityRequirementsOperationFilter>();
//});


//THIS IS THE MAIN CODE/WORKING CODE
var securityScheme = new OpenApiSecurityScheme()
{
    Name = "Authorization",
    Type = SecuritySchemeType.ApiKey,
    Scheme = "Bearer",
    BearerFormat = "JWT",
    In = ParameterLocation.Header,
    Description = "JWT authentication for MinimalAPI"
};

var securityRequirements = new OpenApiSecurityRequirement()
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id ="Bearer"
            }
        },
        new string[] {}
    }
};

var contactInfo = new OpenApiContact()
{
    Name = "Boris Kostadinov",
    Email = "Boris@email.com"

};

var license = new OpenApiLicense()
{
    Name = "Free License",
};

var info = new OpenApiInfo()
{
    Version = "V1",
    Title = "Pokemon Api with JWT Authentication",
    Description = "Pokemon Api with JWT Authentication",
    Contact = contactInfo,
    License = license
};

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", info);
    options.AddSecurityDefinition("Bearer", securityScheme);
    options.AddSecurityRequirement(securityRequirements);

});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {


        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateAudience = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();
////if (args.Length == 1 && args[0].ToLower() == "seeddata")
SeedData(app);
void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<Seed>();
        service.SeedDataContext();
    }
}

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapPost("/accounts/login", [AllowAnonymous] (UserDto user) =>
{
    if (user.username == "admin@email.com" && user.password == "Password123")
    {
        var secureKey = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);


        var issuer = builder.Configuration["Jwt:Issuer"];
        var audience = builder.Configuration["Jwt:Audience"];
        var securityKey = new SymmetricSecurityKey(secureKey);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

        var jwtTokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("id","1"),
                new Claim(JwtRegisteredClaimNames.Sub, user.username),
                new Claim(JwtRegisteredClaimNames.Email, user.username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())

            }),
            Expires = DateTime.Now.AddMinutes(20),
            Audience = audience,
            Issuer = issuer,
            SigningCredentials = credentials
        };

        var token = jwtTokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = jwtTokenHandler.WriteToken(token);
        return Results.Ok(jwtToken);
    }
    return Results.Unauthorized();


});
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();


record UserDto(string username, string password);
