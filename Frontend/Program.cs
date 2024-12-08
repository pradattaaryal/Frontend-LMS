using Frontend.Repositories;
using Frontend.Services;
using Presentation.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();
builder.Services.AddScoped<AuthService>();

// Register the IJwtAuthenticationManager service
builder.Services.AddSingleton<IJwtAuthenticationManager>(provider =>
    new JwtAuthenticationManager("your_secret_key")); // Replace "your_secret_key" with your actual secret key

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
 
 app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Authentication}/{action=Index}/{id?}");

app.Run();
