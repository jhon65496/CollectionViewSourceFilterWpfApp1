using CollectionViewSourceFilterWpfApp1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CollectionViewSourceFilterWpfApp1.ViewModels;
// using CollectionViewSourceFilterWpfApp1.Views;
using System.Diagnostics;
using CollectionViewSourceFilterWpfApp1.Models;

namespace CollectionViewSourceFilterWpfApp1.ViewModels
{
    class MainWindowViewModel : BaseVM
    {
        public DataContextApp dc;

        public CoursesViewModel coursesViewModel;
        public CoursesStudentsJoinViewModel coursesStudentsJoinViewModel;
        public StudentsViewModel studentsViewModel;

        #region Loger === === === === === ===
        public Loger loger;
        string owner = "MainWindowViewModel";
        #endregion === === === === === ===
        public Stopwatch sw;
        long elapsedSw;
        public MainWindowViewModel()
        {
            this.dc = new DataContextApp();            
            loger = new Loger();            
            sw = new Stopwatch();

            // CoursesViewModel
            this.coursesViewModel = new CoursesViewModel(this);
            coursesViewModel.LoadDataTest();

            // CoursesView cView = new CoursesView();
            //cView.DataContext = coursesViewModel;
            CoursesView = coursesViewModel;

            // CoursesStudentsJoinViewModel
            coursesStudentsJoinViewModel = new CoursesStudentsJoinViewModel(this);
            coursesStudentsJoinViewModel.LoadDataTest();

            // CoursesStudentsJoinView csView = new CoursesStudentsJoinView();
            // csView.DataContext = coursesStudentsJoinViewModel;
            CoursesStudentsJoinView = coursesStudentsJoinViewModel;

            // StudentsViewModel
            studentsViewModel = new StudentsViewModel(this.dc, loger);
            studentsViewModel.LoadDataTest();

            // StudentsView sView = new StudentsView();
            // sView.DataContext = studentsViewModel;
            this.StudentsView = studentsViewModel;


            // Prop
            // this.SelectedCourse = coursesViewModel.SelectedCourse;
        }


        // SelectedCourse
        private Course selectedCourse;

        public Course SelectedCourse
        {
            get { return selectedCourse; }
            set
            {
                selectedCourse = value;
                Debug.WriteLine("\n\n === === === MainWindowViewModel.SelectedCourse === === ===");
                if (selectedCourse == null)
                {
                    Debug.WriteLine($"SelectedCourse = null !!!");
                    return;
                }
                Debug.WriteLine($"SelectedCourse.NameCourse -- {selectedCourse.NameCourse}");

                // Установить критерий фильтрации для `CoursesStudentsJoinViewModel`                
                loger.Log(owner, "82", "SelectedCourse -- coursesStudentsJoinViewModel.CourseFilter", "Страт");
                sw.Start();
                coursesStudentsJoinViewModel.CourseFilter = selectedCourse;
                sw.Stop();
                elapsedSw = sw.ElapsedMilliseconds;
                loger.Log(owner, "87", "SelectedCourse -- coursesStudentsJoinViewModel.CourseFilter", $"Финиш : {elapsedSw} ms");

                // --- --- --- --- --- --- --- --- --- --- --- ---
                // ObservableCollection<CourseStudentJoin> GetCoursesStudentsJoin(Course course)
                loger.Log(owner, "86", "SelectedCourse -- coursesStudentsJoinViewModel.GetCoursesStudentsJoin(selectedCourse)", "Страт");
                sw.Start();
                var fdf = coursesStudentsJoinViewModel.GetCoursesStudentsJoin(selectedCourse);
                sw.Stop();
                elapsedSw = sw.ElapsedMilliseconds;
                loger.Log(owner, "88", "SelectedCourse -- coursesStudentsJoinViewModel.GetCoursesStudentsJoin(selectedCourse)", $"Финиш : {elapsedSw} ms");

                loger.Log(owner, "98", "SelectedCourse -- studentsViewModel.LoadDataUnion(fdf)", "Страт");
                sw.Start();
                this.studentsViewModel.LoadDataUnion(fdf);
                sw.Stop();
                elapsedSw = sw.ElapsedMilliseconds;
                loger.Log(owner, "103", "SelectedCourse -- studentsViewModel.LoadDataUnion(fdf)", $"Финиш : {elapsedSw} ms");

                // CollectionViewSource --- --- --- --- --- --- --- --- --- --- ---
                // Установить критерий фильтрации для `StudentsViewModel`
                // var fdf = coursesStudentsJoinViewModel.
                // var cSJ = coursesStudentsJoinViewModel.GetCoursesStudentsJoin(selectedCourse);
                // this.studentsViewModel.LoadDataUnion(cSJ);

                // var fdfd = coursesStudentsJoinViewModel.GetCoursesStudentsJoin01();
                // this.studentsViewModel.LoadDataUnion(fdfd);

                // ICollectionView --- --- --- --- --- --- --- --- --- --- --- --- ---
                // var fdfd = coursesStudentsJoinViewModel.GetCoursesStudentsJoin0();
                // this.studentsViewModel.LoadDataUnion(fdfd);


                //--- ---
                // var fdfd =  coursesStudentsJoinViewModel.GetCoursesStudentsJoin2();
                // this.studentsViewModel.LoadDataUnion(fdfd);

                //--- ---
                // var fdfd =  coursesStudentsJoinViewModel.GetCoursesStudentsJoin3();
                // this.studentsViewModel.LoadDataUnion(fdfd);

                RaisePropertyChanged(nameof(SelectedCourse));
            }
        }


        /// <summary>
        /// View
        /// </summary>
        #region View === === === === === === === === ===
        // CoursesView
        private BaseVM coursesView;

        public BaseVM CoursesView
        {
            get { return coursesView; }
            set
            {
                coursesView = value;
                RaisePropertyChanged(nameof(CoursesView));
            }
        }


        // CourseStudentsView        
        private BaseVM _сoursesStudentsJoinView;

        public BaseVM CoursesStudentsJoinView
        {
            get { return _сoursesStudentsJoinView; }
            set
            {
                _сoursesStudentsJoinView = value;
                RaisePropertyChanged(nameof(CoursesStudentsJoinView));
            }
        }

        // CourseStudentsView
        private BaseVM _studentsView;

        public BaseVM StudentsView
        {
            get { return _studentsView; }
            set
            {
                _studentsView = value;
                RaisePropertyChanged(nameof(StudentsView));
            }
        }



        #endregion
    }
}
