using Imageflow.Fluent;
using Imageflow.Server;
using Imageflow.Server.HybridCache;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddImageflowHybridCache(
                new HybridCacheOptions("cache")
                {
                    // How long after a file is created before it can be deleted
                    MinAgeToDelete = TimeSpan.FromSeconds(10),
                    // How much RAM to use for the write queue before switching to synchronous writes
                    WriteQueueMemoryMb = 100,
                    // The maximum size of the cache (1GB)
                    CacheSizeMb = 1000,
                });
var app = builder.Build();

app.UseRouting();

app.UseImageflow(new ImageflowMiddlewareOptions()
    .SetAllowCaching(true)
    .SetMapWebRoot(true)
    .SetMyOpenSourceProjectUrl("https://github.com/AmirhoseinSaeedi/imageresizer")
                .AddWatermark(
                    new NamedWatermark("imazen",
                        "/images/imazen_400.png",
                        new WatermarkOptions().SetMargins(new WatermarkMargins()
                                                                .SetRelativeTo(WatermarkAlign.Image)
                                                                 .SetBottom(40)
                                                                 .SetRight(0)))));
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
