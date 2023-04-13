using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NETCore.MailKit.Core;
using UserManagementAPI.Data;
using UserManagementAPI.Entity;
using UserManagementAPI.Models.Authentication.SignUp;
using UserManagement.Service.Models;
using UserManagement.Service.Services;
using IEmailService = UserManagement.Service.Services.IEmailService;
using EmailService = UserManagement.Service.Services.EmailService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddDbContext<APIContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")));
builder.Services.AddControllers();
var Configuration = builder.Configuration;
builder.Services.AddDbContext<APIContext>(options => options.UseSqlServer(Configuration.GetConnectionString("defaultConnection")));
builder.Services.AddIdentity<AddUserDetails, IdentityRole>()
.AddEntityFrameworkStores<APIContext>()
.AddDefaultTokenProviders();
var emailConfig = Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.Configure<IdentityOptions>(opts => opts.SignIn.RequireConfirmedEmail = true);



//Adding Authentication





builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
