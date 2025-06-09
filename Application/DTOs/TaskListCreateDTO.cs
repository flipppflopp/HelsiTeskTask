using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class TaskListCreateDTO
    {
        public string? SenderId { get; set; }
        public string Name { get; set; }
        public List<string> Tasks { get; set; } = new List<string>();
        public string OwnerUserId { get; set; }
        public List<string> AllowedUserIds { get; set; } = new List<string>();
        public DateTime? CreatedAt { get; set; }
    }
}
