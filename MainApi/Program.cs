using Common.Encryption;
using Data;
using Data.Abstracts;
using MainApi.Helpers;
using MainApi.Options;
using MainApi.Services.Purchases;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

const string DefaultConnection = "DefaultConnection";

var builder = WebApplication.CreateBuilder(args);

var authOptions = new AuthOptions();
builder.Configuration.GetRequiredSection(nameof(AuthOptions)).Bind(authOptions);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = authOptions.TokenIssuer,
            ValidateAudience = true,
            ValidAudience = authOptions.TokenAudience,
            ValidateLifetime = true,
            IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
            ValidateIssuerSigningKey = true,
        };
    });

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DbApplicationContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString(DefaultConnection);
    DbApplicationContext.ConfigureContextOptions(options, connectionString);
});

builder.Services.Configure<AuthOptions>(builder.Configuration.GetSection(typeof(AuthOptions).Name));
builder.Services.Configure<EncryptionOptions>(builder.Configuration.GetSection(typeof(EncryptionOptions).Name));

builder.Services.AddAllImplementationsAsTransient<IQuery>();
builder.Services.AddAllImplementationsAsTransient<ICommand>();
builder.Services.AddAllImplementationsAsTransient<IValidator>();

builder.Services.AddTransient<IEncryptionService, EncryptionService>();
builder.Services.AddTransient<IPurchaseService, DummyPurchaseService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
