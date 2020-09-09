using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Domain.Commands;
using Todo.Domain.Entities;
using Todo.Domain.Handlers;
using Todo.Domain.Repositories;

namespace Todo.Api.Controllers
{

    [ApiController]
    [Route("v1/todos")]
    [Authorize]
    public class TodoController : ControllerBase
    {
        
        // [Route("")]
        // [HttpGet]
        // public IEnumerable<TodoItem> GetById
        // (
        //     [FromServices] ITodoRepository repository),
        //     [FromBody] Guid id
        // {
        //     //var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
        //     return  repository.GetById(id,"xpto");
        // }

        [Route("")]
        [HttpGet]
        public IEnumerable<TodoItem> GetAll
        ([FromServices] ITodoRepository repository)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return  repository.GetAll(user);
        }

        [Route("done")]
        [HttpGet]
        public IEnumerable<TodoItem> GetAllDone
        ([FromServices] ITodoRepository repository)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return  repository.GetAllDone(user);
        }

        
        [Route("Undone")]
        [HttpGet]
        public IEnumerable<TodoItem> GetAllUndone
        ([FromServices] ITodoRepository repository)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return  repository.GetAllUndone(user);
        }

        [Route("done/Today")]
        [HttpGet]
        public IEnumerable<TodoItem> GetDoneForToday
        ([FromServices] ITodoRepository repository)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return  repository.GetByPeriod(user,DateTime.Now, true);
        }

        [Route("Undone/Today")]
        [HttpGet]
        public IEnumerable<TodoItem> GetInactiveForToday
        ([FromServices] ITodoRepository repository)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return  repository.GetByPeriod(user,DateTime.Now, false);
        }

        [Route("done/tomorrow")]
        [HttpGet]
        public IEnumerable<TodoItem> GetDoneForTomorrow
        ([FromServices] ITodoRepository repository)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return  repository.GetByPeriod(user,DateTime.Now.AddDays(1), true);
        }

        [Route("undone/tomorrow")]
        [HttpGet]
        public IEnumerable<TodoItem> GetUndoneForTomorrow
        ([FromServices] ITodoRepository repository)
        {
            var user = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
            return  repository.GetByPeriod(user,DateTime.Now.AddDays(1), false);
        }


        [Route("")]
        [HttpPost] 
        public GenericCommandResult Create(
            [FromBody] CreateTodoCommand command,
            [FromServices] TodoHandler handler
        )
        {
         command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

         return (GenericCommandResult)handler.Handle(command);   
        }

        [Route("")]
        [HttpPut] 
        public GenericCommandResult Update(
            [FromBody] UpdateTodoCommand command,
            [FromServices] TodoHandler handler
        )
        {
         command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

         return (GenericCommandResult)handler.Handle(command);   
        }

        [Route("mark-as-done")]
        [HttpPut] 
        public GenericCommandResult Update(
            [FromBody] MarkTodoAsDoneCommand command,
            [FromServices] TodoHandler handler
        )
        {
        command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

         return (GenericCommandResult)handler.Handle(command);   
        }

        [Route("mark-as-undone")]
        [HttpPut] 
        public GenericCommandResult Update(
            [FromBody] MarkTodoAsUndoneCommand command,
            [FromServices] TodoHandler handler
        )
        {
         command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;

         return (GenericCommandResult)handler.Handle(command);   
        }
    }
}