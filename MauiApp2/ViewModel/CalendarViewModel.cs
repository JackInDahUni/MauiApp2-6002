using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp2.Models;
using MauiApp2.Services;

namespace MauiApp2.ViewModel
{
    public partial class CalendarViewModel : ObservableObject
    {
        private readonly ITaskService _taskService;

        [ObservableProperty]
        private DateTime selectedDate = DateTime.Today;

        [ObservableProperty]
        private string selectedDateText;

        [ObservableProperty]
        private ObservableCollection<Models.TodoTask> tasksOnSelectedDate = new();

        public CalendarViewModel(ITaskService taskService)
        {
            _taskService = taskService;
            UpdateSelectedDateText();
            LoadTasksForSelectedDate();
        }

        partial void OnSelectedDateChanged(DateTime value)
        {
            UpdateSelectedDateText();
            LoadTasksForSelectedDate();
        }

        private void UpdateSelectedDateText()
        {
            SelectedDateText = $"Tasks for {SelectedDate:MMMM d, yyyy}";
        }

        private async void LoadTasksForSelectedDate()
        {
            // Clear the current list
            TasksOnSelectedDate.Clear();

            // Load tasks for the selected date
            var tasks = await _taskService.GetTasksForDateAsync(SelectedDate);

            foreach (var task in tasks)
            {
                TasksOnSelectedDate.Add(task);
            }
        }

        [RelayCommand]
        private async Task AddTaskAsync()
        {
            // Navigate to the add task page, passing the selected date
            var parameters = new Dictionary<string, object>
            {
                { "date", SelectedDate }
            };

            await Shell.Current.GoToAsync("AddTaskPage", parameters);
        }

        [RelayCommand]
        private async Task ToggleTaskCompletionAsync(Models.TodoTask task)
        {
            task.IsCompleted = !task.IsCompleted;
            await _taskService.UpdateTaskAsync(task);
        }

        [RelayCommand]
        private async Task ViewTaskDetailsAsync(Models.TodoTask task)
        {
            // Navigate to task details page
            await Shell.Current.GoToAsync($"{nameof(DetailPage)}?id={task.Id}");
        }
    }
}