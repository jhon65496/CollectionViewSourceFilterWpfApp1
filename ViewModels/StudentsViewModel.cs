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

namespace CollectionViewSourceFilterWpfApp1.ViewModels
{
    class StudentsViewModel : BaseVM
    {
        // MainWindowViewModel mainWindowViewModel;
        #region Loger === === === === === ===
        Loger loger;
        string owner = "StudentsViewModel";
        #endregion === === === === === ===

        DataContextApp dc;
        // ctor
        public StudentsViewModel(DataContextApp dc, Loger loger)
        {
           //  this.mainWindowViewModel = mainWindowViewModel;

            this.dc = dc;
            this.loger = loger;
        }


        // Courses
        private ObservableCollection<Student> _students;

        public ObservableCollection<Student> Students
        {
            get { return _students; }
            set 
            {
                _students = value;
                
                                _StudentViewSource = new CollectionViewSource();
                _StudentViewSource.Source = value;
                _StudentViewSource.Filter += OnStudentsFilter;
                _StudentViewSource.View.Refresh(); // 

                RaisePropertyChanged(nameof(Students));
            }
        }


        private ObservableCollection<Student> _studentViews;

        public ObservableCollection<Student> StudentsView
        {
            get { return _studentViews; }
            set
            {
                _studentViews = value;



                RaisePropertyChanged(nameof(StudentsView));
            }
        }



        // SelectedCourse
        private Student _selectedStudent;

        public Student SelectedStudent
        {
            get { return _selectedStudent; }
            set
            {
                _selectedStudent = value;
                Debug.WriteLine("\n\n === === === CoursesViewModel.SelectedCourse === === ===");                
                if (SelectedStudent == null)
                {
                    Debug.WriteLine($"SelectedCourse = null !!!");
                    return;
                }
                Debug.WriteLine($"SelectedCourse.NameCourse -- {SelectedStudent.NameStudent}");
                // this.mainWindowViewModel.SelectedCourse = SelectedStudent;
                RaisePropertyChanged(nameof(SelectedStudent));
            }
        }

        #region Filter == === === === === ==
        private Student[] _studentsFilter;

        public Student[] StudentsFilter
        {
            get { return _studentsFilter; }
            set
            {
                _studentsFilter = value;

                if (_studentsFilter == null)
                {

                    return;
                }


                foreach (var item in _studentsFilter)
                {
                    StudentFilter = item;
                    _StudentViewSource.View.Refresh();
                }


            }
        }
        private Student _studentFilter;
        public Student StudentFilter
        {
            get { return _studentFilter; }
            set
            {
                _studentFilter = value;

                // Debug.WriteLine("\n\n=== === === CoursesStudentsJoinViewModel === === ===");

                if (_studentsFilter == null)
                {
                    // Debug.WriteLine($"CourseFilter.NameCourse -- Null !!!!");
                    return;
                }
                // Debug.WriteLine($"CourseFilter.NameCourse -- {CourseFilter.NameCourse}");

                _StudentViewSource.View.Refresh();

            }
        }

        private void OnStudentsFilter(object sender, FilterEventArgs e)
        {
            // Debug.WriteLine($"\n\n === === === CoursesStudentsJoinViewModel === === === ");
            // Debug.WriteLine($"OnIndexProvidersFilter(object sender, FilterEventArgs e) ");


            if (!(e.Item is Student student)) return;


            if (StudentFilter == null) return;

            // Debug.WriteLine($"courseStudentJoin.IdCourse == CourseFilter.IdCourse -- {courseStudentJoin.IdCourse} = {CourseFilter.IdCourse} ");
            if (student.IdStudent == StudentFilter.IdStudent)
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
        private CollectionViewSource _StudentViewSource;

        public ICollectionView StudentViewSourceView => _StudentViewSource?.View;
        #endregion

        #endregion

        public void LoadDataTest()
        {
            // СalculationIndexs = this.DataContextApp.СalculationIndexes;
            Students = dc.Students;
            // return dc.Courses;

            Debug.WriteLine($"\n\n === === === CoursesViewModel === === === ");
            Debug.WriteLine($"LoadDataTest()");
            
        }


        public void LoadDataUnion(ObservableCollection<CourseStudentJoin> courseStudentJoin)
        {

            // var result = Students.Where(cSJ => !courseStudentJoin
            //                     .Select(s => s.IdStudent).Contains(cSJ.IdStudent));
            loger.Log(owner, "106", "LoadDataUnion(ObservableCollection<CourseStudentJoin> courseStudentJoin)", "Страт");
            StudentsView = new ObservableCollection<Student>(Students
                                .Where(cSJ => !courseStudentJoin
                                    .Select(s => s.IdStudent).Contains(cSJ.IdStudent)));
            loger.Log(owner, "110", "LoadDataUnion(ObservableCollection<CourseStudentJoin> courseStudentJoin)", "Финиш");

            Debug.WriteLine($"\n\n=== === === ProvidersViewModel === === ===");
            Debug.WriteLine($"LoadDataUnion(...)  ");
            // Debug.WriteLine($"ProvidersView.Count -- {ProvidersView.Count}");
            // Debug.WriteLine($"Providers.Count -- {Providers.Count}");

            //foreach (var item in ProvidersView)
            //{
            //    Debug.WriteLine($"item.Id: {item.Id} | item.Name: {item.Name}");
            //}
        }

        public void LoadDataUnion(List<CourseStudentJoin> courseStudentJoin)
        {

            // var result = Students.Where(cSJ => !courseStudentJoin
            //                     .Select(s => s.IdStudent).Contains(cSJ.IdStudent));

            StudentsView = new ObservableCollection<Student>(Students
                                .Where(cSJ => !courseStudentJoin
                                    .Select(s => s.IdStudent).Contains(cSJ.IdStudent)));

            Debug.WriteLine($"\n\n=== === === ProvidersViewModel === === ===");
            Debug.WriteLine($"LoadDataUnion(...)  ");
            // Debug.WriteLine($"ProvidersView.Count -- {ProvidersView.Count}");
            // Debug.WriteLine($"Providers.Count -- {Providers.Count}");

            //foreach (var item in ProvidersView)
            //{
            //    Debug.WriteLine($"item.Id: {item.Id} | item.Name: {item.Name}");
            //}
        }

        public void LoadDataUnion(IEnumerable<CourseStudentJoin> courseStudentJoin)
        {

            // var result = Students.Where(cSJ => !courseStudentJoin
            //                     .Select(s => s.IdStudent).Contains(cSJ.IdStudent));

            StudentsView = new ObservableCollection<Student>(Students
                                .Where(cSJ => !courseStudentJoin
                                    .Select(s => s.IdStudent).Contains(cSJ.IdStudent)));

            Debug.WriteLine($"\n\n=== === === ProvidersViewModel === === ===");
            Debug.WriteLine($"LoadDataUnion(...)  ");
            // Debug.WriteLine($"ProvidersView.Count -- {ProvidersView.Count}");
            // Debug.WriteLine($"Providers.Count -- {Providers.Count}");

            //foreach (var item in ProvidersView)
            //{
            //    Debug.WriteLine($"item.Id: {item.Id} | item.Name: {item.Name}");
            //}
        }
    }
}
