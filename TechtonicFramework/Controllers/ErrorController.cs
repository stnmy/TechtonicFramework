using System;
using System.Web.Http;

namespace TechtonicFramework.Controllers
{
    [RoutePrefix("api/error")]
    public class ErrorController : ApiController
    {
        [HttpGet]
        [Route("not-found")]
        public IHttpActionResult GetNotFound()
        {
            return NotFound();
        }

        [HttpGet]
        [Route("bad-request")]
        public IHttpActionResult GetBadRequest()
        {
            return BadRequest("This is not a good request.");
        }

        [HttpGet]
        [Route("unauthorized")]
        public IHttpActionResult GetUnauthorized()
        {
            return Unauthorized();
        }

        [HttpGet]
        [Route("validation-error")]
        public IHttpActionResult GetValidationError()
        {
            ModelState.AddModelError("problem1", "This is the first error.");
            ModelState.AddModelError("problem2", "This is the second error.");
            return BadRequest(ModelState);
        }

        [HttpGet]
        [Route("server-error")]
        public IHttpActionResult GetServerError()
        {
            throw new Exception("This is a server error.");
        }
    }
}
