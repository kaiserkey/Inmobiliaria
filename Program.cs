using Inmobiliaria.Models;
using MySql.Data.MySqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();
var serviceProvider = new ServiceCollection();
// Agrega la clase MySqlDatabase al contenedor de servicios.
builder.Services.AddSingleton<IMySqlDatabase, MySqlDatabase>();
serviceProvider.AddTransient<MySqlDatabase>(_ => new MySqlDatabase("server=localhost; database=Inmobiliaria; uid=root; pwd=1234;"));
//.BuildServiceProvider();
/* Esta linea de codigo se utiliza para crear una instancia de la clase MySqlDatabase 
y registrarla en el contenedor de servicios de la aplicación.
La clase MySqlDatabase se utiliza para conectarse a una base de datos MySQL. 
En este caso, se está creando una nueva instancia de esta clase y pasando una cadena 
de conexión como parámetro en el constructor. La cadena de conexión especifica el 
servidor MySQL, la base de datos a la que se conectará, el nombre de usuario y la contraseña.
Al utilizar el método AddTransient, se registra la clase MySqlDatabase como un servicio transient 
en el contenedor de servicios de la aplicación. Esto significa que cada vez que se solicite una 
instancia de la clase MySqlDatabase a través del contenedor de servicios, se creará una nueva 
instancia de la clase.
La inyección de dependencias es una técnica de programación que se utiliza para separar la creación 
de objetos y su uso. En este caso, se registra la clase MySqlDatabase en el contenedor de servicios 
para que pueda ser utilizada por otras clases de la aplicación que dependan de ella, sin tener que 
preocuparse por crear una instancia de la clase manualmente.
 */

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
