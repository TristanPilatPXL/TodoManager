using System;
using System.Windows;

namespace TodoManager.WPF
{
    public partial class AddTodoWindow : Window
    {
        public string TodoTitle { get; set; } 
        public string? TodoDescription { get; set; }
        public DateTime TodoDueDate { get; set; }

        public AddTodoWindow()
        {
            InitializeComponent();

            // gebruik de x:Name uit XAML (dueDatePicker)
            dueDatePicker.SelectedDate = DateTime.Today;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            this.TodoTitle = titleTextBox.Text;
            this.TodoDescription = descriptionTextBox.Text;
            this.TodoDueDate = dueDatePicker.SelectedDate!.Value;

            this.DialogResult = true;
            this.Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}