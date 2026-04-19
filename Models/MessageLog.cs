using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace AcentemOto.Models
{
    public enum MessageStatus
    {
        Pending,
        Sent,
        Failed
    }

    public class MessageLog : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;

        private MessageStatus _status = MessageStatus.Pending;
        public MessageStatus Status
        {
            get => _status;
            set => SetProperty(ref _status, value);
        }

        private string? _errorMessage;
        public string? ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        private DateTime? _sentAt;
        public DateTime? SentAt
        {
            get => _sentAt;
            set => SetProperty(ref _sentAt, value);
        }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [NotMapped]
        public Dictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetProperty<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
