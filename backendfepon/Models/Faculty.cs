﻿namespace backendfepon.Models
{
    public class Faculty
    {
        public int Faculty_Id { get; set; }
        public string Faculty_Name { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
