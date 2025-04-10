using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiApp2.Models
{
    public class TodoTask : INotifyPropertyChanged
    {
        private string _id;
        private string _title;
        private string _description;
        private DateTime _dueDate;
        private bool _isCompleted;
        private Priority _priority;
        private DateTime _createdDate;
        private DateTime? _completedDate;
        private string _notes;
        private bool _isRecurring;
        private RecurrencePattern _recurrencePattern;
        private string _category;
        private TimeSpan? _reminderTime;

        // Implementing INotifyPropertyChanged for UI updates
        public event PropertyChangedEventHandler PropertyChanged;

        public TodoTask()
        {
            _id = Guid.NewGuid().ToString();
            _createdDate = DateTime.Now;
            _dueDate = DateTime.Today;
            _priority = Priority.Medium;
            _isCompleted = false;
            _isRecurring = false;
        }

        public string Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public DateTime DueDate
        {
            get => _dueDate;
            set => SetProperty(ref _dueDate, value);
        }

        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                if (SetProperty(ref _isCompleted, value))
                {
                    CompletedDate = value ? DateTime.Now : null;
                }
            }
        }

        public Priority TaskPriority
        {
            get => _priority;
            set => SetProperty(ref _priority, value);
        }

        public DateTime CreatedDate
        {
            get => _createdDate;
            set => SetProperty(ref _createdDate, value);
        }

        public DateTime? CompletedDate
        {
            get => _completedDate;
            set => SetProperty(ref _completedDate, value);
        }

        public string Notes
        {
            get => _notes;
            set => SetProperty(ref _notes, value);
        }

        public bool IsRecurring
        {
            get => _isRecurring;
            set => SetProperty(ref _isRecurring, value);
        }

        public RecurrencePattern RecurrencePattern
        {
            get => _recurrencePattern;
            set => SetProperty(ref _recurrencePattern, value);
        }

        public string Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

        public TimeSpan? ReminderTime
        {
            get => _reminderTime;
            set => SetProperty(ref _reminderTime, value);
        }

        // Display-friendly due date
        public string DueDateFormatted => DueDate.ToString("MMM d, yyyy");

        // Is the task overdue?
        public bool IsOverdue => !IsCompleted && DueDate.Date < DateTime.Today;

        // How many days until due (or overdue)
        public int DaysUntilDue => (DueDate.Date - DateTime.Today).Days;

        // Helper method for implementing INotifyPropertyChanged
        protected bool SetProperty<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
        {
            if (Equals(backingField, value))
                return false;

            backingField = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public enum Priority
    {
        Low,
        Medium,
        High,
        Urgent
    }

    public enum RecurrencePattern
    {
        None,
        Daily,
        Weekdays,
        Weekly,
        BiWeekly,
        Monthly,
        Yearly
    }
}