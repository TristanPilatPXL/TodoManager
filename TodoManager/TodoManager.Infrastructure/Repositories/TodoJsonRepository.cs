using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TodoManager.Domain.Models;

namespace TodoManager.Infrastructure.Repositories
{
    
    public class TodoJsonRepository
    {
        private readonly string _dataFolder;
        private readonly string _filePath;



        public TodoJsonRepository()
        {
            _dataFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "todos");
            _filePath = Path.Combine(_dataFolder, "todos.json");

            if (!Directory.Exists(_dataFolder))
                Directory.CreateDirectory(_dataFolder);

            // ✅ Schrijf "[]" (lege JSON array) in plaats van een leeg bestand
            if (!File.Exists(_filePath))
                File.WriteAllText(_filePath, "[]");
        }



        // Lees alle items uit het JSON bestand
        public List<TodoItem> GetAll()
        {
            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<TodoItem>>(json) ?? new List<TodoItem>();
        }



        // Voeg een item toe en sla alles opnieuw op
        public void Add(TodoItem item)
        {
            try
            {
                List<TodoItem> items = GetAll();
                items.Add(item);
                string json = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_filePath, json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("[AppointmentJsonRepository] Error writing file: " + ex);
                throw;
            }
        }

        public TodoItem Get(int id)
        {
            List<TodoItem> items = GetAll();

            return items.Find(todo => todo.Id == id);

        }

        public bool Remove(TodoItem item)
        {
            List<TodoItem> items = GetAll();

            items.Remove(item);

            return true;
        }
    }
}
