using My_Portfolio.Interface;
using My_Portfolio.Models;
using My_Portfolio;
using My_Portfolio.DAO;

var builder = WebApplication.CreateBuilder(args);



builder.Services.Configure<ConnectionString>(builder.Configuration.GetSection("ConnectionStrings"));

// Register IAddEmail and DataAccessLayer
builder.Services.AddScoped<IEmail, DataAccessLayer>();
builder.Services.AddScoped<IEmployees, EmployeeDAO>();
builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews();
builder.Services.AddSession();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
