using AgroOrganizer.Models.Dtos.VehiclesDto;
using AgroOrganizer.Services.Interfaces;

namespace AgroOrganizer.Controllers;

public class VehicleController
{
    public static WebApplication SetUpVehiclesRoutes(WebApplication app, string baseRoute)
    {
        app.MapGet(baseRoute + "/", async (IVehiclesService service, int offset = 0, int limit = 10) =>
        {
            return Results.Ok(await service.GetAllAsync(offset, limit));
        }).WithName("GetAllVehicles").WithTags("Vehicle");

        app.MapGet(baseRoute + "/{id:int}", async (IVehiclesService service, int id) =>
        {
            return Results.Ok(await service.GetByIdAsync(id));
        }).WithName("GetVehicleById").WithTags("Vehicle");

        app.MapPost(baseRoute + "/", async (IVehiclesService service, CreateVehicleDto dto) =>
        {
            return Results.Ok(await service.CreateVehicleAsync(dto));
        }).WithName("CreateVehicle").WithTags("Vehicle");

        app.MapPut(baseRoute + "/{id:int}", async (IVehiclesService service, int id, CreateVehicleDto dto) =>
        {
            return Results.Ok(await service.UpdateVehicleAsync(id, dto));
        }).WithName("UpdateVehicle").WithTags("Vehicle");

        app.MapDelete(baseRoute + "/{id:int}", async (IVehiclesService service, int id) =>
        {
            return Results.Ok(await service.DeleteVehicleAsync(id));
        }).WithName("DeleteVehicle").WithTags("Vehicle");
        
        app.MapPost(baseRoute + "/{id:int}/image", async (int id, HttpContext context, IVehiclesService service, IWebHostEnvironment env) =>
        {
            var file = context.Request.Form.Files.FirstOrDefault();
            if (file == null || file.Length == 0) return Results.BadRequest("Няма избран файл.");

            // Create folder wwwroot/uploads if not exists
            var webRootPath = env.WebRootPath ?? Path.Combine(env.ContentRootPath, "wwwroot");
            var uploadsFolder = Path.Combine(webRootPath, "uploads");
            Directory.CreateDirectory(uploadsFolder);

            // Generate unique name for the image
            var fileName = $"{Guid.NewGuid()}_{file.FileName}";
            var filePath = Path.Combine(uploadsFolder, fileName);

            // Save the file in the hard disc
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // Path for react
            var fileUrl = $"/uploads/{fileName}";

            // Call the service
            var success = await service.UpdateImageUrlAsync(id, fileUrl);
    
            if (!success) return Results.NotFound("Машината не е намерена.");

            return Results.Ok(new { imageUrl = fileUrl });
        }).DisableAntiforgery();
        
        return app;
    }
}