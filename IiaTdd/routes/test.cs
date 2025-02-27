using Microsoft.AspNetCore.Mvc;

namespace IiaTdd.routes;
[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
 
    
    [HttpGet]
    public bool Get()
    {
       
        
        return true;
    }
   
}