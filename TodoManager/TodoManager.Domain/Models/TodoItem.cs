using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoManager.Domain.Models
{
    internal class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; }
        public string? Description { get; }
        public DateTime DueDate { get; }
        public bool IsCompleted { get; private set; }
        public DateTime? CompletedAt { get; private set; }

        public TodoItem(string title, string? description, DateTime dueDate)
        {

            //titel moet
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentNullException("title cannot be null, empty, or whitespace");
            }


            //dit mag null zijn
            description = description?.Trim();

            IsCompleted = false;
            CompletedAt = null;
        }

        public void MarkAsCompleted()
        {

            IsCompleted = true;
            CompletedAt = DateTime.Now;
         
        }

        public override string ToString()
        {
            string status = IsCompleted ? "[Done]" : "[Open]";
            return $"{status} {Title} (due {DueDate:dd/MM/yyyy})";
        }
    }

}
