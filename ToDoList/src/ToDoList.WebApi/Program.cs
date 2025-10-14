var builder = WebApplication.CreateBuilder(args);
{
    //configure DI
    builder.Services.AddControllers();
}
var app = builder.Build();
{
    //Configure Middleware (HTTP request pipeline)
    app.MapControllers(); ;
}

app.Run();
