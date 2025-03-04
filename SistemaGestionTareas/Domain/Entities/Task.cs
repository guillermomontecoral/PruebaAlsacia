using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Task
    {
        private string _title;
        private string _description;
        private DateOnly _expirationDate;
        private Domain.Enums.TaskStatus _status;

        public int Id { get; set; }
        public string Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("Title cannot be null or empty.");
                }
                _title = value;
            }
        }
        public string Description
        {
            get => _description;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("Description cannot be null or empty.");
                }
                _description = value;
            }
        }
        public DateOnly ExpirationDate
        {
            get => _expirationDate;
            set
            {
                if (value < DateOnly.FromDateTime(DateTime.Now))
                {
                    throw new Exception("Expiration date cannot be in the past.");
                }
                _expirationDate = value;
            }
        }
        public Domain.Enums.TaskStatus Status
        {
            get => _status;
            set
            {
                if (!Enum.IsDefined(typeof(Domain.Enums.TaskStatus), value))
                {
                    throw new Exception("Invalid status.");
                }
                _status = value;
            }
        }
        public int UserId { get; set; }
        public User User { get; set; }

        public Task() 
        {  
            _status = Domain.Enums.TaskStatus.Created;

        }

        //Para pruebas unitarias
        public Task(int id, string title, string description, DateOnly expirationDate, Domain.Enums.TaskStatus status, int userId)
        {
            Id = id;
            Title = title;
            Description = description;
            ExpirationDate = expirationDate;
            Status = status;
            UserId = userId;
        }
    }

}
