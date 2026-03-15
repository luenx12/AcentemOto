using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcentemOto.Models
{
    public enum MessageStatus
    {
        Pending,
        Sent,
        Failed
    }

    public class MessageLog
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; } = string.Empty;
        public MessageStatus Status { get; set; } = MessageStatus.Pending;
        public string? ErrorMessage { get; set; }
        public DateTime? SentAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [NotMapped]
        public Dictionary<string, string> Parameters { get; set; } = new Dictionary<string, string>();
    }
}
