using System;
using System.IO;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using MauiApp2.Services;

namespace MauiApp2.ViewModel
{
    [QueryProperty(nameof(TaskId), "id")]
    public partial class DetailViewModel : ObservableObject
    {
        private readonly ITaskService _taskService;

        public DetailViewModel(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [ObservableProperty]
        private string taskId;

        [ObservableProperty]
        private string text;

        [ObservableProperty]
        private string details;

        partial void OnTaskIdChanged(string value)
        {
            LoadTaskDetailsAsync(value);
        }

        private async void LoadTaskDetailsAsync(string id)
        {
            if (Guid.TryParse(id, out var guid))
            {
                var task = await _taskService.GetTaskByIdAsync(id);
                if (task != null)
                {
                    Text = task.Title;
                    Details = task.Description;
                }
                else
                {
                    Text = "Task not found";
                    Details = "";
                }
            }
            else
            {
                Text = "Invalid Task ID";
            }
        }
    }
}
