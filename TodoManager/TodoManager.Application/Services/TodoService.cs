using System;
using System.Collections.Generic;
using System.Linq;
using TodoManager.Domain.Models;
using TodoManager.Infrastructure.Repositories;

namespace TodoManager.Application.Services
{
    public class TodoService
    {
        private readonly TodoJsonRepository _repository;//juiste repo kiezen waar json actief is oops

        public TodoService()
        {
            _repository = new TodoJsonRepository();
        }

        public List<TodoItem> GetTodos()
        {
            return _repository.GetAll();
        }

        public void AddTodo(string title, string? description, DateTime dueDate)
        {
            // check op duplicate titel (case-insensitive)
            if (_repository.GetAll().Any(t => string.Equals(t.Title, title, StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException("Een todo met dezelfde titel bestaat reeds.");
            }

            TodoItem todo = new TodoItem(title, description, dueDate);
            _repository.Add(todo);
        }

        public void CompleteTodo(TodoItem todo)
        {
            if (todo is null) throw new ArgumentNullException(nameof(todo));

            var existing = _repository.Get(todo.Id);
            existing.MarkAsCompleted();
        }

        public void DeleteTodo(TodoItem todo)
        {
            if (todo.DueDate.Date < DateTime.Today)
            {
                throw new InvalidOperationException("Kan geen vervallen todo verwijderen.");
            }

            if (todo.IsCompleted)
            {
                throw new InvalidOperationException("Kan voltooid todo niet verwijderen.");
            }

            _repository.Remove(todo);
        }
    }
}