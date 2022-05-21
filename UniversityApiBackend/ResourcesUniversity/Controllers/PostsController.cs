using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace InternationalApi.Resources.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IStringLocalizer<PostsController> _stringLocalizer;
       


        public PostsController(IStringLocalizer<PostsController> stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
           
        }
        [HttpGet]
        [Route("PostControllerResource")]
        public IActionResult GetUsingPostControllerResource()
        {
            //find text
            var authorize = _stringLocalizer["Authorize"];
            var postName = _stringLocalizer.GetString("Welcolme,Have a good day!").Value ?? String.Empty;

            return Ok(new
            {
                PostType = authorize.Value,
                PostName = postName
            });
        }

        
        
    }



}
        