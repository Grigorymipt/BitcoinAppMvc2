using BitcoinAppMvc.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
//using BitcoinAppMvc.Data;
var builder = WebApplication.CreateBuilder(args);

/*builder.Services.AddDbContext<BitcoinAppMvcContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BitcoinAppMvcContext") ?? throw new InvalidOperationException("Connection string 'BitcoinAppMvcContext' not found.")));
*/

 builder.Services.AddDbContext<BitcoinAppMvcContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("BitcoinAppMvcContext"),
    npgsqlOptionsAction => npgsqlOptionsAction.MigrationsAssembly("BitcoinAppMvc"))); 

 // Add services to the container.
 builder.Services.AddControllersWithViews();

 // Add Swagger
 builder.Services.AddControllers();

 builder.Services.AddEndpointsApiExplorer();
 builder.Services.AddSwaggerGen(options =>
 {
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ToDo API",
        Description = "An ASP.NET Core Web API for managing ToDo items",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Example Contact",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
 });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
/*
else
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}*/

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=TransactionData}/{action=Index}/{id?}");

app.Run();
