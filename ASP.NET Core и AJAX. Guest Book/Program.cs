using ASP.NET_Core_�_AJAX._Guest_Book.Models;
using ASP.NET_Core_�_AJAX._Guest_Book.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ��� ������ �������� ������ ������� IDistributedCache, � 
// ASP.NET Core ������������� ���������� ���������� IDistributedCache
builder.Services.AddDistributedMemoryCache();// ��������� IDistributedMemoryCache
builder.Services.AddSession();  // ��������� ������� ������

// �������� ������ ����������� �� ����� ������������
string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

// ��������� �������� ApplicationContext � �������� ������� � ����������
builder.Services.AddDbContext<Guest_BookContext>(options => options.UseSqlServer(connection));

// ��������� ������� MVC
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IRepository, Guest_Book_Repository>();

var app = builder.Build();
app.UseSession();   // ��������� middleware-��������� ��� ������ � ��������
app.UseStaticFiles(); // ������������ ������� � ������ � ����� wwwroot

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Message}/{action=Index}/{id?}");

app.Run();