using Imageflow.Server;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();

app.UseImageflow(new ImageflowMiddlewareOptions()
    .SetMapWebRoot(true)
    .SetMyOpenSourceProjectUrl("https://github.com/AmirhoseinSaeedi/imageresizer"));

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
