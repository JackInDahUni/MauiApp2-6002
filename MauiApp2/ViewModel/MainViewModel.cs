using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp2.Models;
using MauiApp2.Services;
using TaskModel = MauiApp2.Models.TodoTask;

namespace MauiApp2.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly IConnectivity _connectivity;
        private readonly ITaskService _taskService;

        public MainViewModel(IConnectivity connectivity, ITaskService taskService)
        {
            _connectivity = connectivity;
            _taskService = taskService;

            // Initialize with empty collection until tasks are loaded
            Tasks = new ObservableCollection<TodoTask>();

            // Load tasks when the ViewModel is created
            LoadTasksAsync();
        }

        [ObservableProperty]
        private ObservableCollection<TodoTask> tasks;

        [ObservableProperty]
        private string taskTitle;

        [ObservableProperty]
        private string taskDescription;

        [ObservableProperty]
        private DateTime taskDueDate = DateTime.Today;

        [ObservableProperty]
        private Priority taskPriority = Priority.Medium;

        [RelayCommand]
        private async System.Threading.Tasks.Task LoadTasksAsync()
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Warning", "Working in offline mode", "OK");
                // Consider loading from local storage here
            }

            var loadedTasks = await _taskService.GetAllTasksAsync();

            Tasks.Clear();
            foreach (var task in loadedTasks)
            {
                Tasks.Add(task);
            }
        }

        [RelayCommand]
        private async System.Threading.Tasks.Task AddTaskAsync()
        {
            if (string.IsNullOrWhiteSpace(TaskTitle))
            {
                await Shell.Current.DisplayAlert("Error", "Task title is required", "OK");
                return;
            }

            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("Error", "No internet connection", "OK");
                return;
            }

            var newTask = new TodoTask
            {
                Title = TaskTitle,
                Description = TaskDescription,
                DueDate = TaskDueDate,
                TaskPriority = TaskPriority,
                CreatedDate = DateTime.Now
            };

            // Save the task
            await _taskService.AddTaskAsync(newTask);

            // Add to the observable collection
            Tasks.Add(newTask);

            // Clear input fields
            TaskTitle = string.Empty;
            TaskDescription = string.Empty;
            TaskDueDate = DateTime.Today;
            TaskPriority = Priority.Medium;
        }

        [RelayCommand]
        private async System.Threading.Tasks.Task DeleteTaskAsync(TodoTask task)
        {
            if (Tasks.Contains(task))
            {
                // Delete from storage
                await _taskService.DeleteTaskAsync(task.Id);

                // Remove from the observable collection
                Tasks.Remove(task);
            }
        }

        [RelayCommand]
        private async System.Threading.Tasks.Task ViewTaskDetailsAsync(TodoTask task)

        {
            // Navigate to details page with the task ID
            await Shell.Current.GoToAsync($"{nameof(DetailPage)}?id={task.Id}");
        }

        
    }
}