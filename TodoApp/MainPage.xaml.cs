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
using Windows.UI.ViewManagement;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace TodoApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<ToDoData> allStuff; //Holds a list of all ToDoData objects (each ToDoData has a title and a list of tasks) 
        private ToDoData handledDat; //The ToDo list that you've just right clicked on 
        private static Boolean haveExplainedMode = false; //Keeps track of whether we have seen the teaching tip yet. 
        private static Boolean haveExplainedList = false; 
       
        //Constuctor 
        public MainPage()
        {
            this.InitializeComponent();

            allStuff = AllData.GetLists();

            //We only want the teaching tip to show up the first time mainpage is naviated to. 
            if (!haveExplainedMode)
            {
                ModeTip.IsOpen = true;
                haveExplainedMode = true; 
            }



            /* Will be used when debugging - automatically adds a ToDo list called school when run. 
            ToDoData myData = new ToDoData
            {
                Title = "School",
                ToDoItems = new ObservableCollection<ToDoItem>()
            };
            allStuff.Add(myData);
            */


        }
        

        //Will prompt a dialog box for the user to enter in a new list. That list will get added as an element in allStuff. 
        private async void MakeNewList_Click(object sender, RoutedEventArgs e)
        {

            InputBox.Text = "";
            ContentDialogResult result = await ListDialog.ShowAsync();

            if (result == ContentDialogResult.Primary && InputBox.Text != "")
            {

                ToDoData myData = new ToDoData
                {
                    Title = InputBox.Text,
                    ToDoItems = new ObservableCollection<ToDoItem>(),
                    DoneItems = new ObservableCollection<ToDoItem>()
                };
                allStuff.Add(myData);

                if (!haveExplainedList)
                {
                    ListTip.IsOpen = true;
                    haveExplainedList = true;
                }

            }

        }
        

        //When you click on the list name, you will go to the ToDo page for that specific list
        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            
            Frame.Navigate(typeof(Todo), e.ClickedItem);
        }

        
        //When you right click on a list item, you have the option to delete or rename it. 
        private void StackPanel_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            StackPanel listView = (StackPanel)sender;
            Fly.ShowAt(listView, e.GetPosition(listView));
            var a = ((FrameworkElement)e.OriginalSource).DataContext;

            StackPanel sta = sender as StackPanel;
            handledDat = sta.DataContext as ToDoData;

        }

        private async void Rename_Click(object sender, RoutedEventArgs e)
        {
            NewNameBox.Text = "";
            ContentDialogResult result = await RenameDialog.ShowAsync();
            if (result == ContentDialogResult.Primary && NewNameBox.Text != "")
            {
                handledDat.Title = NewNameBox.Text;
             
            }
            handledDat = null;
        }

        //When delete is clicked on the flyout menu, then the list will be removed from the home screen. 
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            allStuff.Remove(handledDat);
            handledDat = null;
        }

        //When mode is toggled, you can switch from a day background to a night background
        private void Switch_Toggled(object sender, RoutedEventArgs e)
        {

            if (Switch.IsOn)
            {
                Day.Visibility = Visibility.Collapsed;
                Night.Visibility = Visibility.Visible;
                BGRect.Visibility = Visibility.Visible;
            }
            else if (!Switch.IsOn){
                Day.Visibility = Visibility.Visible;
                Night.Visibility = Visibility.Collapsed;
                BGRect.Visibility = Visibility.Collapsed;
            }
            
        }
    }

}
