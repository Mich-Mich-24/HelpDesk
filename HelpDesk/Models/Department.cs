﻿using System.ComponentModel;

namespace HelpDesk.Models
{
    public class Department : UserActivity
    {
        [DisplayName("No")]
        public int Id { get; set; }

        [DisplayName("Department Code")]
        public string Code { get; set; }

        [DisplayName("Department Name")]

        public string Name { get; set; }
    }
}
