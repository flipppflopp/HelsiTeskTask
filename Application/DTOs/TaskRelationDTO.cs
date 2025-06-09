using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class TaskRelationDTO
    {
        public string SenderId { get; set; }
        public string TaskListId { get; set; }
        public string AccessedUserId { get; set; }
    }
}
