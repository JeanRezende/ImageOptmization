using System.Net;
using System.Net.Http.Headers;
using ImageOptimization.Dominio;
using ImageOptimization.Services;
using Microsoft.AspNetCore.Mvc;

namespace ImageOptimization.Controller;

[ApiController]
[Route("[controller]")]
public class ImageOptimizer : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> SendImage([FromForm] ImageFile imageFile)
    {
        OptmizeJpeg optmizeJpeg = new();

        if (imageFile.Image == null || imageFile.Image.Length == 0)
        {
            return BadRequest("file empty");
        }

        await optmizeJpeg.ProcessImage(imageFile);

        return NoContent();
    }

    [HttpGet]
    public HttpResponseMessage GetLastImage()
    {
        ImageReturn imageReturn = new();
        var image = imageReturn.ProcessReturn();

        HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
        response.Content = new StreamContent(image);
        response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");


        return response;
    }
}

