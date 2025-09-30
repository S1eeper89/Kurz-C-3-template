var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/test", () => "This is a test.");
app.MapGet("/cyechitas", () => "Vitej na kurzu Czechitas Jane!");
app.MapGet("/poydrav/{jmeno}", (string jmeno) => $"Ahoj {jmeno}!");
app.Run();
