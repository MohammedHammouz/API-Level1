using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstRESTProject.Controllers
{
    //[Route("api/[controller]")]
    
    [Route("api/MyFirstAPI")]
    [ApiController]
    public class MyFirstAPIController : ControllerBase
    {
        [HttpGet("MyName",Name ="MyName")]
        public string MyName()
        {
            return "My name's Mohammed Al-Hammouz";
        }
        [HttpGet("MyAge", Name = "MyAge")]
        public int MyAge()
        {
            return 30;
        }
        [HttpGet("YourName", Name = "YourName")]
        public string YourName()
        {
            return "Your name's Mohammed Abu-Hadhoud";
        }
        [HttpGet("sum/{Num1},{Num2}")]
        public int Sum2Numbers(int Num1,int Num2)
        {
            return Num1 + Num2;
        }
        [HttpGet("Multiply/{Num1},{Num2}")]
        public int Myltiply2Numbers(int Num1, int Num2)
        {
            return Num1 * Num2;
        }

    }
}
