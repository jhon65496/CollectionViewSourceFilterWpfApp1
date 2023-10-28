using CollectionViewSourceFilterWpfApp1.Commands;
using CollectionViewSourceFilterWpfApp1.Data;
using CollectionViewSourceFilterWpfApp1.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;

namespace CollectionViewSourceFilterWpfApp1.ViewModels
{
    class CoursesStudentsJoinViewModel : BaseVM
    {
        MainWindowViewModel mainWindowViewModel;

        DataContextApp dc;

        public Stopwatch sw;

        #region Loger === === === === === ===
        Loger loger;
        string owner = "CoursesStudentsJoinViewModel";
        #endregion === === === === === ===

        // ctor
        public CoursesStudentsJoinViewModel(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            
            this.dc     = this.mainWindowViewModel.dc;
            this.loger  = this.mainWindowViewModel.loger;
            this.sw     = this.mainWindowViewModel.sw;
        }


        // CoursesStudentsJoins
        private ObservableCollection<CourseStudentJoin> _coursesStudentsJoin;

        public ObservableCollection<CourseStudentJoin> CoursesStudentsJoins
        {
            get { return _coursesStudentsJoin; }
            set
            {
                _coursesStudentsJoin = value;

                _CoursesStudentsJoinsViewSource = new CollectionViewSource();
                _CoursesStudentsJoinsViewSource.Source = value;
                _CoursesStudentsJoinsViewSource.Filter += OnCoursesStudentsJoinsFilter;
                _CoursesStudentsJoinsViewSource.View.Refresh(); // 

                // RaisePropertyChanged(nameof(CoursesStudentsJoins));
                // CoursesStudentsJoinsView
                RaisePropertyChanged(nameof(CoursesStudentsJoinsView));
            }
        }


        // SelectedCourse
        private CourseStudentJoin _selectedCourseStudentJoin;

        public CourseStudentJoin SelectedCoursesStudents
        {
            get { return _selectedCourseStudentJoin; }
            set
            {
                _selectedCourseStudentJoin = value;


                //if (_selectedCourseStudentJoin == null) return;
                //Debug.WriteLine($"--- --- --- --- --- --- --- --- ---");
                //Debug.WriteLine($"IndexesViewModel--selectedIndexCalculation -- {_selectedCourseStudentJoin.NameCourse}");
                //if (this._selectedCourseStudentJoin == null)
                //{
                //    Debug.WriteLine($"IndexesViewModel--selectedIndexCalculation -- managerIndexesViewModel = null\n");
                //    return;
                //}

                // this.managerIndexesViewModel.SelectedIndexCalculation = selectedIndexCalculation;


                RaisePropertyChanged(nameof(SelectedCoursesStudents));
            }
        }


        #region Filter == === === === === ==
        private Course _сourseFilter;

        public Course CourseFilter
        {
            get { return _сourseFilter; }
            set
            {
                _сourseFilter = value;

                // Debug.WriteLine("\n\n=== === === CoursesStudentsJoinViewModel === === ===");

                if (CourseFilter == null)
                {
                    // Debug.WriteLine($"CourseFilter.NameCourse -- Null !!!!");
                    return;
                }
                // Debug.WriteLine($"CourseFilter.NameCourse -- {CourseFilter.NameCourse}");

                _CoursesStudentsJoinsViewSource.View.Refresh();
                
            }
        }


        private void OnCoursesStudentsJoinsFilter(object sender, FilterEventArgs e)
        {
            // Debug.WriteLine($"\n\n === === === CoursesStudentsJoinViewModel === === === ");
            // Debug.WriteLine($"OnIndexProvidersFilter(object sender, FilterEventArgs e) ");


            if (!(e.Item is CourseStudentJoin courseStudentJoin)) return;


            if (CourseFilter == null) return;

            // Debug.WriteLine($"courseStudentJoin.IdCourse == CourseFilter.IdCourse -- {courseStudentJoin.IdCourse} = {CourseFilter.IdCourse} ");
            if (courseStudentJoin.IdCourse == CourseFilter.IdCourse)
            {
                e.Accepted = true;
                //  Debug.WriteLine($"e.Accepted = true");
            }
            else
            {
                e.Accepted = false;
                // Debug.WriteLine($"e.Accepted = false");
            }
        }

        #region CollectionView
        private CollectionViewSource _CoursesStudentsJoinsViewSource;

        public ICollectionView CoursesStudentsJoinsView => _CoursesStudentsJoinsViewSource?.View;
        #endregion

        #endregion

        #region Command TestCommand - Тестовая команда
        /// <summary>Тестовая команда</summary>
        private ICommand _TestCommand;

        /// <summary>Тестовая команда</summary>
        public ICommand TestCommand
        {
            get
            {
                if (_TestCommand == null)
                {
                    _TestCommand = new LambdaCommand(OnTestCommandExecuted, CanTestCommandExecute);
                }
                return _TestCommand;
            }
        }

        /// <summary>Проверка возможности выполнения - Тестовая команда</summary>
        private bool CanTestCommandExecute(object p) => true;

        /// <summary>Логика выполнения - Тестовая команда</summary>
        private void OnTestCommandExecuted(object p)
        {
            //var value = _UserDialog.GetStringValue("Введите строку", "123", "Значение по умолчанию");
            //_UserDialog.ShowInformation($"Введено: {value}", "123");

            var df = (CourseStudentJoin)p;

            Debug.WriteLine("\n\n === === === CoursesStudentsJoinViewModel === === ===");
            Debug.WriteLine($"OnTestCommandExecuted(object p) -- p.NameStudent -- {df.NameStudent}");
        }
        #endregion

        /*
         // СalculationIndexs = this.DataContextApp.СalculationIndexes;
            loger.Log(owner, "78", "LoadDataTest() ", "Старт");
            sw.Start();
            Courses = dc.Courses;
            // return dc.Courses;
            sw.Stop();
            var elapsed = sw.ElapsedMilliseconds;
            loger.Log(owner, "78", "LoadDataTest() ", $"Финиш. Длительность : {elapsed} ms");
         */
        public void LoadDataTest()
        {            
            loger.Log(owner, "181", "LoadDataTest()--CoursesStudentsJoins", "Страт");
            sw.Start();
            CoursesStudentsJoins = dc.CoursesStudentsJoins;
            sw.Stop();
            var elapsed = sw.ElapsedMilliseconds;
            loger.Log(owner, "183", "LoadDataTest()--CoursesStudentsJoins", $"Финиш. Длительность : {elapsed} ms");

            Debug.WriteLine($"\n\n === === === CoursesStudentsJoinViewModel === === === ");
            Debug.WriteLine($"LoadDataTest() ");
            // Debug.WriteLine($"СalculationIndexs.Count -- {СalculationIndexs.Count}");
        }

        public ObservableCollection<CourseStudentJoin> GetCoursesStudentsJoin(Course course)
        {
            
            int IdCourse = course.IdCourse;

            loger.Log(owner, "195", "GetCoursesStudentsJoin(Course course)--ObservableCollection<CourseStudentJoin>", "Страт");
            sw.Start();
            var res = CoursesStudentsJoins.Where(cSJ => cSJ.IdCourse == IdCourse).ToList();
            var coursesStudentsJoins = new ObservableCollection<CourseStudentJoin>(res);
            sw.Stop();
            var elapsed = sw.ElapsedMilliseconds;
            loger.Log(owner, "198", "GetCoursesStudentsJoin(Course course)--ObservableCollection<CourseStudentJoin>", $"Финиш. Длительность : {elapsed} ms");

            return coursesStudentsJoins;

            // Debug.WriteLine($"\n\n === === === CoursesStudentsJoinViewModel === === === ");
            // Debug.WriteLine($"LoadDataTest() ");
            // Debug.WriteLine($"СalculationIndexs.Count -- {СalculationIndexs.Count}");
        }

        public List<CourseStudentJoin> GetCoursesStudentsJoin1(Course course)
        {
            int IdCourse = course.IdCourse;

            var res = CoursesStudentsJoins.Where(cSJ => cSJ.IdCourse == IdCourse).ToList();
            // var coursesStudentsJoins = new ObservableCollection<CourseStudentJoin>(res);

            return res;

            Debug.WriteLine($"\n\n === === === CoursesStudentsJoinViewModel === === === ");
            Debug.WriteLine($"LoadDataTest() ");
            // Debug.WriteLine($"СalculationIndexs.Count -- {СalculationIndexs.Count}");
        }

        public IEnumerable<CourseStudentJoin> GetCoursesStudentsJoin0()
        {
            // CollectionViewSource
            
            return (IEnumerable<CourseStudentJoin>)_CoursesStudentsJoinsViewSource.View;   // CollectionViewSource
            
        }

        public IEnumerable<CourseStudentJoin> GetCoursesStudentsJoin01()
        {
            // CollectionViewSource

            // return (IEnumerable<CourseStudentJoin>)_CoursesStudentsJoinsViewSource.View;   // CollectionViewSource
            return _CoursesStudentsJoinsViewSource.View.Cast<CourseStudentJoin>();

        }

        public IEnumerable<CourseStudentJoin> GetCoursesStudentsJoin2()
        {
            // CollectionViewSource
            // var dd = _CoursesStudentsJoinsViewSource.View;   // CollectionViewSource
            return (IEnumerable<CourseStudentJoin>)_CoursesStudentsJoinsViewSource.View;   // CollectionViewSource

            // ICollectionView
            // var dfdf = CoursesStudentsJoinsView.tol             // ICollectionView
            var filteredItems = CoursesStudentsJoinsView.Cast<CourseStudentJoin>();
        }

        public IEnumerable<CourseStudentJoin> GetCoursesStudentsJoin3()
        {
            var ава  = CoursesStudentsJoinsView.Cast<CourseStudentJoin>().Count<CourseStudentJoin>();
            return CoursesStudentsJoinsView.Cast<CourseStudentJoin>();
        }
        
    }
}
