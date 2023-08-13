using Microsoft.AspNetCore.Mvc;

namespace objctv_test_case_2.Controllers
{
    [ApiController]
    [Route("/api/count")]
    public class CountController : Controller
    {
        [HttpGet]
        public IActionResult GetCount()
        {
            int count = Server.GetCount();
            return Ok(count);
        }


        [HttpPost]
        public IActionResult AddToCount([FromBody]int value)
        {
            try
            {
                Server.AddToCount(value);
                return Ok();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
            
        }
    }
}
