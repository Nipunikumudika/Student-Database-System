using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;

namespace Student_Database
{
    public partial class MainWindowVM : ObservableObject
    {
        [ObservableProperty]
        public string? firstName;

        [ObservableProperty]
        public string? lastName;

        [ObservableProperty]
        public string dateOfBirth;

        [ObservableProperty]
        public string image;

        [ObservableProperty]
        public int age;

        [ObservableProperty]
        public double gpa;

        [ObservableProperty]
        ObservableCollection<Student> student;

        [ObservableProperty]
        public Student selectedStudent = null;

        public Action CloseAction { get; internal set; }

        [RelayCommand]
        public void InsertStudent()
        {
            Student s = new Student()
            {
                FirstName = FirstName,
                LastName = LastName,
                DateOfBirth = DateOfBirth,
                Image = Image,
                GPA = Gpa,
                Age = Age
            };

            if (Gpa > 4 || Gpa<0)
            {
                MessageBox.Show("Error! GPA is not valid");
            }
            else
            {
                using (var db = new StudentContext())
                {
                    db.Students.Add(s);
                    db.SaveChanges();
                    if (s != null)
                    {
                        CloseAction();
                    }
                }
                LoadStudent();
            }
        }

        [RelayCommand]
        public void DeleteStudent()
        {
            using (var db = new StudentContext())
            {
                var selectedstudent = db.Students.Where(r => r.Id == SelectedStudent.Id);
                foreach (var student in selectedstudent)
                {
                    db.Students.Remove(student);
                }
                db.SaveChanges();
            }
            LoadStudent();
        }

        [RelayCommand]
        public void AddStudent()
        {
            var vm = new MainWindowVM();
            var addWindow = new AddStudent(vm);
            Application.Current.MainWindow.Opacity = 0.85;
            ((MainWindow)Application.Current.MainWindow).Closemain.IsEnabled = false;
            addWindow.Show();
            addWindow.Closed += (sender, e) =>
            {
                ((MainWindow)Application.Current.MainWindow).Closemain.IsEnabled = true;
                Application.Current.MainWindow.Opacity = 1;
                LoadStudent();
            };
        }


        public MainWindowVM(Student s)
        {
            SelectedStudent = s;

            FirstName = SelectedStudent.FirstName;
            LastName = SelectedStudent.LastName;
            Age = SelectedStudent.Age;
            Gpa = SelectedStudent.GPA;
            DateOfBirth = SelectedStudent.DateOfBirth;
            Image = SelectedStudent.Image;

        }



        [RelayCommand]
        public void EditStudentClick()
        {
            
             var vm = new MainWindowVM(SelectedStudent);
            EditStudent editStudent = new EditStudent(vm);
            editStudent.Show();
            Application.Current.MainWindow.Opacity = 0.85;
            ((MainWindow)Application.Current.MainWindow).Closemain.IsEnabled = false;
            editStudent.Closed += (sender, e) =>
            {
                LoadStudent();
                Application.Current.MainWindow.Opacity = 1;
                ((MainWindow)Application.Current.MainWindow).Closemain.IsEnabled = true;
            };
        }



        [RelayCommand]
        public void EditStudent()
        {
            using (var db = new StudentContext())
            {
                SelectedStudent.FirstName = FirstName;
                SelectedStudent.LastName = LastName;
                SelectedStudent.DateOfBirth = DateOfBirth;
                SelectedStudent.Image = Image;
                SelectedStudent.GPA = Gpa;
                SelectedStudent.Age = Age;



                if (SelectedStudent.GPA > 4 || SelectedStudent.GPA < 0)
                {
                    MessageBox.Show("Error! GPA is not valid");

                }
                else
                {
                    db.Students.Update(SelectedStudent);
                    db.SaveChanges();
                    if (SelectedStudent != null)
                    {
                        CloseAction();
                    }
                }
                LoadStudent();
                }

        }


        public void LoadStudent()
        {
            using (var db = new StudentContext())
            {
                var list = db.Students.ToList();
                Student = new ObservableCollection<Student>(list);
            }
        }

        [RelayCommand]
        public void SelectImage()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files | *.bmp; *.png; *.jpg";
            dialog.FilterIndex = 1;
            if (dialog.ShowDialog() == true)
            {
                Image = dialog.FileName;
            }
        }
        public MainWindowVM()
        {
            LoadStudent();
        }
    }
}
