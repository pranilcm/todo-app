using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Todo_App.Server.Models;
using Todo_App.Server.Services;

namespace Todo_App.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodosController : ControllerBase
    {
        // The header name used to identify the user making the request.
        private const string UserIdHeader = "X-User-Id";
        private const string AnonymousUserId = "anonymous";

        private readonly ITodoService _service;

        public TodosController(ITodoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TodoResponse>> GetAll()
        {
            return Ok(_service.GetAll(GetUserId()));
        }

        [HttpPost]
        public ActionResult<TodoResponse> Create(CreateTodoRequest request)
        {
            var response = _service.Create(GetUserId(), request.Title);
            return StatusCode(StatusCodes.Status201Created, response);
        }

        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            return _service.Delete(GetUserId(), id) ? NoContent() : NotFound();
        }

        private string GetUserId()
        {
            if (Request.Headers.TryGetValue(UserIdHeader, out var values))
            {
                var value = values.FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(value)) return value!;
            }

            return AnonymousUserId;
        }
    }
}
