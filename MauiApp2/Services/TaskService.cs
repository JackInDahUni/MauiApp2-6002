using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MauiApp2.Models;

namespace MauiApp2.Services
{
    public interface ITaskService
    {
        System.Threading.Tasks.Task<IEnumerable<TodoTask>> GetAllTasksAsync();
        System.Threading.Tasks.Task<IEnumerable<TodoTask>> GetTasksForDateAsync(DateTime date);
        System.Threading.Tasks.Task<TodoTask> GetTaskByIdAsync(string id);
        System.Threading.Tasks.Task<bool> AddTaskAsync(TodoTask task);
        System.Threading.Tasks.Task<bool> UpdateTaskAsync(TodoTask task);
        System.Threading.Tasks.Task<bool> DeleteTaskAsync(string taskId);
    }

    public class TaskService : ITaskService
    {
        // This is a simple in-memory implementation
        // In a real app, you would use a database or cloud storage
        private readonly List<Models.TodoTask> _tasks = new List<Models.TodoTask>();

        public Task<IEnumerable<Models.TodoTask>> GetAllTasksAsync()
        {
            return Task.FromResult<IEnumerable<Models.TodoTask>>(_tasks);
        }

        public Task<IEnumerable<Models.TodoTask>> GetTasksForDateAsync(DateTime date)
        {
            var tasksForDate = _tasks.Where(t => t.DueDate.Date == date.Date);
            return Task.FromResult(tasksForDate);
        }

        public Task<Models.TodoTask> GetTaskByIdAsync(string id)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == id);
            return Task.FromResult(task);
        }

        public Task<bool> AddTaskAsync(Models.TodoTask task)
        {
            _tasks.Add(task);
            return Task.FromResult(true);
        }

        public Task<bool> UpdateTaskAsync(Models.TodoTask task)
        {
            var existingTask = _tasks.FirstOrDefault(t => t.Id == task.Id);
            if (existingTask == null)
                return Task.FromResult(false);

            var index = _tasks.IndexOf(existingTask);
            _tasks[index] = task;
            return Task.FromResult(true);
        }

        public Task<bool> DeleteTaskAsync(string taskId)
        {
            var task = _tasks.FirstOrDefault(t => t.Id == taskId);
            if (task == null)
                return Task.FromResult(false);

            _tasks.Remove(task);
            return Task.FromResult(true);
        }
    }
}