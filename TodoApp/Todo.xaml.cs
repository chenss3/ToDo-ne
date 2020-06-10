using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using TodoApp.Model;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TodoApp
{
    /// <summary>
    /// When you navigate from the main page, it Will display each ToDo List's tasks
    /// </summary>
    public sealed partial class Todo : Page
    {
        private ObservableCollection<ToDoItem> myData; //Holds each ToDo list's tasks 
        private ObservableCollection<ToDoItem> doneData; //Holds the finished items 
 
        //Constructor
        public Todo()
        {
            this.InitializeComponent();
            myData = ToDoData.GetItems();
            
        }

        //Will store the appropriate ToDoData object's ToDo list into myData, depending on which list you clicked on MainPage
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ToDoData newstuff = e.Parameter as ToDoData;
            myData = newstuff.ToDoItems;
            doneData = newstuff.DoneItems;
            ListName.Text = newstuff.Title;


        }

        //Add button to add a task 
        private void AddTask_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewThing), myData, new EntranceNavigationTransitionInfo());
        }

        //Back button to go back to mainpage 
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        //When a task is checkboxed, it will be removed from the task list. 
        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ItemsList.SelectedIndex >= 0)
            {
                ToDoItem item = myData[ItemsList.SelectedIndex];
                doneData.Add(item);
                myData.Remove(item);
            }
        }

        //Go to the page that will display the finished tasks
        private void CompletedItems_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Completed), doneData);
        }
    }

}
