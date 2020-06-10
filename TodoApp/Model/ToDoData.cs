using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace TodoApp.Model
{
    //Collection of all the ToDo lists
    public class AllData
    {
       
        private static ObservableCollection<ToDoData> allData = new ObservableCollection<ToDoData>();

        public ObservableCollection<ToDoData> AllDataItems
        {

            get; set;
        }

        public static ObservableCollection<ToDoData> GetLists()
        {
            return allData;
        }
    }

    //Represents one ToDo list, where Title is the name of the ToDo list and ToDoItems is the collection of tasks. 
    public class ToDoData : INotifyPropertyChanged
    {
        private static ObservableCollection<ToDoItem> items = new ObservableCollection<ToDoItem>();

        private static ObservableCollection<ToDoItem> doneItems = new ObservableCollection<ToDoItem>();

        private string _title;


        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged("Title");
            }
        }
        public ObservableCollection<ToDoItem> ToDoItems
        {
            get; set;

        }

        public ObservableCollection<ToDoItem> DoneItems
        {
            get; set;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public static ObservableCollection<ToDoItem> GetItems()
        {
            return items;
        }
    }

    //Represents each task 
    public class ToDoItem
    {
        public string Title
        {
            get; set;
        }
        public DateTime Date
        {
            get; set;
        }

    }
}
