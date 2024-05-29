using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [ApiController]
    [Route("fundoonotes")]
    public class LoginRegistrationController : Controller
    {
          
        [Route("Registration")]

        public string  RegistrationController()
        {
            return "Controller added successfully";
        }
    }
}
