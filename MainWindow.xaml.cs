using CollectionViewSourceFilterWpfApp1.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CollectionViewSourceFilterWpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowViewModel mainWindowViewModel;

        public MainWindow()
        {
            InitializeComponent();

            mainWindowViewModel = new MainWindowViewModel();
            
            // Test();
        }


        public void Test()
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // txtParametr.Text
            int indexCourse = Convert.ToInt16(txtParametr.Text);

            var selectedCourse = mainWindowViewModel.coursesViewModel.Courses[indexCourse-1];
            mainWindowViewModel.coursesViewModel.SelectedCourse = selectedCourse;
        }
    }
}
