using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TodoManager.Application.Services;
using TodoManager.Domain.Models;

namespace TodoManager.WPF
{
    /// <summary>
    /// Interaction logic for TodoListWindow.xaml
    /// </summary>
    public partial class TodoListWindow : Window
    {
        private readonly TodoService _todoService;

        public TodoListWindow()
        {
            InitializeComponent();

            _todoService = new TodoService();
            // RefreshTodos();
        }

        private void RefreshTodos()
        {
            todosListBox.Items.Clear();

            List<TodoItem> todos = _todoService.GetTodos();

            foreach (TodoItem todo in todos)
            {
                todosListBox.Items.Add(todo); // relies on TodoItem.ToString()
            }

            // ClearDetails();
        }

        private void ClearDetails()
        {
            titleTextBlock.Text = "";
            dueDateTextBlock.Text = "";
            completedTextBlock.Text = "";
            completedAtTextBlock.Text = "";
            descriptionTextBlock.Text = "";
        }

        private void TodosListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // geselecteerde todo ophalen
            var todo = todosListBox.SelectedItem as TodoItem;

            // call ClearDetails & return indien null
            if (todo is null)
            {
                ClearDetails();
                return;
            }

            // details tonen
            titleTextBlock.Text = todo.Title;
            dueDateTextBlock.Text = todo.DueDate.ToString("dd/MM/yyyy");
            descriptionTextBlock.Text = todo.Description ?? string.Empty;

            completedTextBlock.Text = todo.IsCompleted ? "Ja" : "Nee";
            completedAtTextBlock.Text = todo.CompletedAt?.ToString("dd/MM/yyyy HH:mm") ?? "-";
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddTodoWindow addWindow = new AddTodoWindow();
            addWindow.Owner = this;

            bool? result = addWindow.ShowDialog();
            if (result != true)
            {
                return;
            }

            // TODO: try-catch
            // TODO: _todoService.AddTodo(addWindow.TodoTitle, addWindow.TodoDescription, addWindow.TodoDueDate);
            try
            {
                _todoService.AddTodo(addWindow.TodoTitle, addWindow.TodoDescription, addWindow.TodoDueDate);
                RefreshTodos();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Invalid input", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unexpected error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            
        }

        private void CompleteButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: selected todo ophalen
            var todo = todosListBox.SelectedItem as TodoItem;

            // TODO: try-catch
            try
            {
                _todoService.CompleteTodo(todo);
                RefreshTodos();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Invalid input", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unexpected error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: selected todo ophalen
            var todo = todosListBox.SelectedItem as TodoItem;

            // TODO: try-catch
            try
            {
                _todoService.DeleteTodo(todo);
                RefreshTodos();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Invalid input", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Unexpected error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshTodos();
        }

    }
}
