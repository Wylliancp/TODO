using Flunt.Notifications;
using Todo.Domain.Commands;
using Todo.Domain.Commands.Contracts;
using Todo.Domain.Entities;
using Todo.Domain.Handlers.Contracts;
using Todo.Domain.Repositories;

namespace Todo.Domain.Handlers
{
    public class TodoHandler :
    Notifiable,
    IHandler<CreateTodoCommand>,
    IHandler<UpdateTodoCommand>,
    IHandler<MarkTodoAsDoneCommand>,
    IHandler<MarkTodoAsUndoneCommand>
    {
    
    private readonly ITodoRepository _repository;

    public TodoHandler(ITodoRepository repository)
    {
        _repository = repository;    
    }

       public ICommandResult Handle(CreateTodoCommand command)
       {
           //Fail Fast Validation
           if(command.Invalid)
            return new GenericCommandResult(
                false,
                "Ops",
                command.Notifications
            );
            
            //Gera Entidade
            var todo = new TodoItem(command.Title, command.Date, command.User);

            //Salva no banco
            _repository.Create(todo);

            //Retorna o Generic true
            return new GenericCommandResult(
                true, 
                "Tarefa Salva!", 
                todo
            );
       }

       public ICommandResult Handle(UpdateTodoCommand command)
       {
           //Fail Fast Validation
           if(command.Invalid)
            return new GenericCommandResult(
                false,
                "Ops",
                command.Notifications
            );
            
            //Recupera o TodoItem - Ré Hidratação
            var todo = _repository.GetById(command.Id, command.User);

            //Altera o Titulo
            todo.UpdateTitle(command.Title);

            //Atualiza no banco
            _repository.Update(todo);

            //Retorna o Generic true
            return new GenericCommandResult(
                true, 
                "Tarefa Atualizada!", 
                todo
            );
       }

       public ICommandResult Handle(MarkTodoAsDoneCommand command)
       {
           //Fail Fast Validation
           if(command.Invalid)
            return new GenericCommandResult(
                false,
                "Ops",
                command.Notifications
            );
            
            //Recupera o TodoItem - Ré Hidratação
            var todo = _repository.GetById(command.Id, command.User);

            //Altera o estado
            todo.MarkAsDone();

            //Atualiza no banco
            _repository.Update(todo);

            //Retorna o Generic true
            return new GenericCommandResult(
                true, 
                "Tarefa Atualizada!", 
                todo
            );
       }

       public ICommandResult Handle(MarkTodoAsUndoneCommand command)
       {
           
           //Fail Fast Validation
           if(command.Invalid)
            return new GenericCommandResult(
                false,
                "Ops",
                command.Notifications
            );
            
            //Recupera o TodoItem - Ré Hidratação
            var todo = _repository.GetById(command.Id, command.User);

            //Altera o estado
            todo.MarkAsUnDone();

            //Atualiza no banco
            _repository.Update(todo);

            //Retorna o Generic true
            return new GenericCommandResult(
                true, 
                "Tarefa Atualizada!", 
                todo
            );
       }
    }
}