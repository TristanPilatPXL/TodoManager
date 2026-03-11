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
        private readonly TodoJsonRepository _repository;

        public TodoService(TodoJsonRepository todoJsonRepository)
        {
            // TODO: maak de repository aan
            _repository = todoJsonRepository;
        }

        public List<TodoItem> GetTodos()
        {
            // return alle todos via repository
            return _repository.GetAll();
        }

        public void AddTodo(string title, string description, DateTime dueDate)
        {
            bool alreadyExists = _repository.GetAll().Any(a =>
                a.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

            if (alreadyExists)
                throw new InvalidOperationException("Deze appointment bestaat al.");


            TodoItem newTodoItem = new TodoItem(
                title,
                description,
                dueDate
                );

            _repository.Add(newTodoItem);

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
