using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoManager.Domain.Models;

namespace TodoManager.Infrastructure.Repositories
{


    public class TodoRepository
    {
        private readonly List<TodoItem> _todos;

        public TodoRepository()
        {
            // initialiseer de lijst
            _todos = new List<TodoItem>();


        }

        public List<TodoItem> GetAll()
        {
            return new List<TodoItem>(_todos);
        }

        public TodoItem Get(int id)
        {
            return _todos.Find(todo => todo.Id == id);
        }

        public void Add(TodoItem item)
        {

            // bepaal het volgende uniek Id (simpel: Count + 1)
            item.Id = _todos.Count + 1;

            // voeg toe aan _todos
            _todos.Add(item);


        }

        public bool Remove(TodoItem item)
        {
            // TODO: verwijder het item en return true/false
            if(_todos.Remove(item))
            {
                return true;
            }else
            {
                return false;
            }
             

        }
    }
}
