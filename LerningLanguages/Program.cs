using LerningLanguages.Data;
using LerningLanguages.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Tokens.Experimental;
using System.Text;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=app.db"));
builder.Services.AddScoped<JwtService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = "user",
            ValidateAudience= true,
            ValidAudience="audience",
            ValidateLifetime= true,
            IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secrect_jwt_key_123456789_dont_tell_this")),
            ValidateIssuerSigningKey= true

        };
    });
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("https://localhost:7018/openapi/v1.json", "LearningLanguages API v1");
        options.RoutePrefix = "swagger";
    });
}
using(var scope= app.Services.CreateScope())
{
    var context= scope.ServiceProvider.GetRequiredService<AppDbContext>();
    SeedData.Initializer(context);
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseRouting();
app.MapControllers();
app.Run();
