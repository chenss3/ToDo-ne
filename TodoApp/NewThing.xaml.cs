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
using System.Data;
using System.Threading;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TodoApp
{

    /// <summary>
    /// When you navigate from the ToDo list page, it Will prompt the user to enter details about their new task
    /// </summary>
    public sealed partial class NewThing : Page
    {
        private ObservableCollection<ToDoItem> data; //Holds each ToDo list's tasks 


        //Constructor
        public NewThing()
        {
            this.InitializeComponent();
        }


        //Will store the appropriate ToDoData object's ToDo list into data
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            data = e.Parameter as ObservableCollection<ToDoItem>;
        }

        //Will add the item to the ToDo list and go back to the ToDo list page
        private void Done_Click(object sender, RoutedEventArgs e)
        {
            ToDoItem newItem = new ToDoItem();
            if(InputBox.Text != "" && Cal.Date != null)
            {
                newItem.Title = InputBox.Text;
                newItem.Date = Cal.Date.Value.Date;
                newItem.StringDate = Cal.Date.Value.Date.ToString("MM/dd/yyyy");
                addItem(newItem);
                
            }

            Frame.GoBack();
        }

        //Helper method to OnNavigatedTo that will add each task in date order 
        private void addItem(ToDoItem newItem)
        {
            Boolean done = false;
            int index = 0;
            if (data.Count == 0)
            {
                data.Add(newItem);
            }
            else
            {
                for (int i = 0; i < data.Count; ++i)
                {
                    if (done != true && DateTime.Compare(newItem.Date, data[i].Date) <= 0)
                    {
                        index = i;
                        done = true;
                    }
                }

                if (done != false)
                {
                    int origCount = data.Count;
                    for (int i = origCount - 1; i >= index; i--)
                    {
                        if (i == origCount - 1)
                        {
                            data.Add(data[origCount - 1]);
                        }
                        else
                        {
                            data[i + 1] = data[i];
                        }

                    }
                    data[index] = newItem;
                }
                else
                {
                    data.Add(newItem);
                }
            }
        }

        //Will go back to the ToDo list page
        private void DeleteConfirmation_Click(object sender, RoutedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
        }


    }
}
