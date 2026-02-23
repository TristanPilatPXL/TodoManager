using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoManager.Domain.Models;
using TodoManager.Infrastructure.Repositories;

namespace TodoManager.Application.Services
{
    public class TodoService
    {
        private readonly TodoRepository _repository;

        public TodoService()
        {
            // TODO: maak de repository aan
            _repository = new TodoRepository();
        }

        public List<TodoItem> GetTodos()
        {
            // return alle todos via repository
            return _repository.GetAll();
        }

        public void AddTodo(string title, string? description, DateTime dueDate)
        {
            // TODO: als duplicate -> throw new InvalidOperationException("Een todo met dezelfde titel bestaat reeds.");
            throw new InvalidOperationException("Een todo met dezelfde titel bestaat reeds");


            TodoItem todo = new TodoItem(title, description, dueDate);
            _repository.Add(todo);
        }

        public void CompleteTodo(TodoItem todo)
        {
            if (todo is null) throw new ArgumentNullException(nameof(todo));

            // zoek het item in de repository (TIP: `Get(todo.Id)`)
            var existing = _repository.Get(todo.Id);
            

            // roep `MarkAsCompleted()` aan
            existing.MarkAsCompleted();
        }

        public void DeleteTodo(TodoItem todo)
        {
            if (todo.DueDate.Date < DateTime.Today)
            {
                throw new InvalidOperationException("...");
            }

            if (todo.IsCompleted)
            {
                throw new InvalidOperationException("...");
            }

            _repository.Remove(todo);
        }
    }


}
