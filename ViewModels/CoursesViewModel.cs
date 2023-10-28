using CollectionViewSourceFilterWpfApp1.Data;
using CollectionViewSourceFilterWpfApp1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CollectionViewSourceFilterWpfApp1.ViewModels
{   
    class CoursesViewModel : BaseVM
    {
        MainWindowViewModel mainWindowViewModel;

        DataContextApp dc;
        public Stopwatch sw;
        
        #region Loger === === === === === ===
        public Loger loger;
        string owner = "CoursesViewModel";
        #endregion === === === === === ===

        // ctor
        public CoursesViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;

            this.dc = this.mainWindowViewModel.dc;
            this.sw = this.mainWindowViewModel.sw;
            this.loger = this.mainWindowViewModel.loger;
        }


        // Courses
        private ObservableCollection<Course> _courses;

        public ObservableCollection<Course> Courses
        {
            get { return _courses; }
            set 
            { 
                _courses = value;
                
                

                RaisePropertyChanged(nameof(Courses));
            }
        }


        // SelectedCourse
        private Course _selectedCourse;

        public Course SelectedCourse
        {
            get { return _selectedCourse; }
            set
            {
                _selectedCourse = value;
                Debug.WriteLine("\n\n === === === CoursesViewModel.SelectedCourse === === ===");                
                if (SelectedCourse == null)
                {
                    Debug.WriteLine($"SelectedCourse = null !!!");
                    return;
                }
                Debug.WriteLine($"SelectedCourse.NameCourse -- {SelectedCourse.NameCourse}");
                this.mainWindowViewModel.SelectedCourse = SelectedCourse;
                RaisePropertyChanged(nameof(SelectedCourse));
            }
        }


        public void LoadDataTest()
        {
            // СalculationIndexs = this.DataContextApp.СalculationIndexes;
            loger.Log(owner, "78", "LoadDataTest() ", "Старт");
            sw.Start();
            Courses = dc.Courses;
            // return dc.Courses;
            sw.Stop();
            var elapsed = sw.ElapsedMilliseconds;
            loger.Log(owner, "78", "LoadDataTest() ", $"Финиш. Длительность : {elapsed} ms");

            //Debug.WriteLine($"\n\n === === === CoursesViewModel === === === ");
            //Debug.WriteLine($"LoadDataTest() -- длительность {elapsed} ms");
            
        }
    }
}
