using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Todo.Domain.Commands;
using Todo.Domain.Handlers;
using Todo.Tests.Repositories;

namespace Todo.Tests.HandlerTests
{
    [TestClass]
    public class CreateTodoHandlerTests
    {

        [TestMethod]
        public void Dado_um_comando_invalido_deve_interromper_a_execucao()
        {
            var command = new CreateTodoCommand("","",DateTime.Now);
            var handler = new TodoHandler(new FakeTodoRepository());
            var result = (GenericCommandResult)handler.Handle(command);
            Assert.AreEqual(result.Success, false);
        }

        [TestMethod]
        public void Dado_um_comando_valido_deve_criar_a_tarefa()
        {
            var command = new CreateTodoCommand("Titulo da Tarefa","Administrador",DateTime.Now);
            var handler = new TodoHandler(new FakeTodoRepository());
            var result = (GenericCommandResult)handler.Handle(command);
            Assert.AreEqual(result.Success, true);
        }
    }
}