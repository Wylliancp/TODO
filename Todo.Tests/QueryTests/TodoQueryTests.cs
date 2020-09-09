using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Todo.Domain.Entities;
using Todo.Domain.Queries;

namespace Todo.Tests.QueryTests
{
    [TestClass]
    public class TodoQueryTests
    {
        private List<TodoItem> _items;

        public TodoQueryTests()
        {
            _items = new List<TodoItem>();
            _items.Add(new TodoItem("Tarefa 1", DateTime.Now, "xpto"));
            _items.Add(new TodoItem("Tarefa 2", DateTime.Now, "Usuario 3"));
            _items.Add(new TodoItem("Tarefa 3", DateTime.Now, "Usuario 4"));
            _items.Add(new TodoItem("Tarefa 4", DateTime.Now, "Usuario 2"));
            _items.Add(new TodoItem("Tarefa 5", DateTime.Now, "xpto"));
        }

        [TestMethod]
        public void Dada_a_consulta_deve_retorna_tarefas_apenas_do_usuario_xpto()
        {
            var result = _items.AsQueryable().Where(TodoQueries.GetAll("xpto"));
            Assert.AreEqual(2, result.Count());
        }
    }
}