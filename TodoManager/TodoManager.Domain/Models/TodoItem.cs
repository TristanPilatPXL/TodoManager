using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoManager.Domain.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; }
        public string? Description { get; }
        public DateTime DueDate { get; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedAt { get; set; }

        public TodoItem(string title, string? description, DateTime dueDate)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                throw new ArgumentException("Title is verplicht.", nameof(title));
            }

            // assign properties
            Title = title.Trim();
            Description = description?.Trim();
            DueDate = dueDate;

            IsCompleted = false;
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
