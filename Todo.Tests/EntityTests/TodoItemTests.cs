using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Todo.Domain.Entities;

namespace Todo.Tests.EntityTests
{
    [TestClass]
    public class TodoItemTests
    {
        [TestMethod]
        public void Dado_um_novo_todo_o_mesmo_nao_pode_ser_concluido()
        {
            var todo = new TodoItem("Titulo Todo", DateTime.Now, "Administrador");
            Assert.AreEqual(todo.Done, false);

        }
        
    }
}