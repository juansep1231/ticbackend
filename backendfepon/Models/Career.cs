﻿namespace backendfepon.Models
{
    public class Career
    {
        public int Career_Id { get; set; }
        public string Career_Name { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
