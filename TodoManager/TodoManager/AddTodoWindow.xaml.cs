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
using TodoManager.Infrastructure.Repositories;

namespace TodoManager.WPF
{
    /// <summary>
    /// Interaction logic for AddTodoWindow.xaml
    /// </summary>
    public partial class AddTodoWindow : Window
    {
        public string TodoTitle { get; private set; } = "";
        public string? TodoDescription { get; private set; }
        public DateTime TodoDueDate { get; private set; }

        private readonly TodoService _todoService;


        public AddTodoWindow()
        {
            InitializeComponent();

            TodoJsonRepository jsonRepository = new TodoJsonRepository();
            _todoService = new TodoService(jsonRepository);

            dueDatePicker.SelectedDate = DateTime.Today;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
            string title = titleTextBox.Text;
            string description = descriptionTextBox.Text;
            DateTime dueDate = dueDatePicker.SelectedDate!.Value;



            _todoService.AddTodo(title, description, dueDate);


            this.DialogResult = true;
            this.Close();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Ongeldige invoer", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Fout", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Er ging iets mis:\n" + ex.Message, "Onverwachte fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
