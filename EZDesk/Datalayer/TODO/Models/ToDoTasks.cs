using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EZDeskDataLayer.TODO.Models
{
    public class ToDoTasks
    {
        /// <summary>
        /// ID for this task;
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Name of the task
        /// </summary>
        public string TaskName { get; set; }

        /// <summary>
        /// True when the user marks the task as complete
        /// </summary>
        public bool Completed { get; set; }

        /// <summary>
        /// Date/Time the task is scheduled to be completed
        /// </summary>
        public DateTime? TargetDate { get; set; }

        /// <summary>
        /// True when the task is deleted
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// List ID that this task belongs to
        /// </summary>
        public int ListID { get; set; }
    }
}
