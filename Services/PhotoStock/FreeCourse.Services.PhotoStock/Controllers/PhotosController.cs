using FreeCourse.Services.PhotoStock.Dtos;
using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> Save(IFormFile photo,CancellationToken cancellationToken)
        {
            if (photo != null && photo.Length>0)
            {
               var path=Path.Combine(Directory.GetCurrentDirectory(),"photos",photo.FileName);
                using(var stream= new FileStream(path,FileMode.Create))
                    await photo.CopyToAsync(stream,cancellationToken);
                var returnPath = "photos/" + photo.FileName;
                PhotoDto response = new() { Url = returnPath };
                return CreateActionResultInstance(Response<PhotoDto>.Success(response, 200));
            }
            else
                return CreateActionResultInstance(Response<NoContent>.Fail("photo is empty",400));


        }
        [HttpDelete]
        public IActionResult Delete(string photoUrl)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "photos", photoUrl);
            if (!System.IO.File.Exists(path))
            {
                return CreateActionResultInstance(Response<NoContent>.Fail("Photo not Found", 404));
            }
            System.IO.File.Delete(path);
            return CreateActionResultInstance(Response<NoContent>.Success(204));

        }

    }
}
