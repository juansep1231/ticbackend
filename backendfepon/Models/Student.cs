namespace backendfepon.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public int FacultyId { get; set; }
        public int CareerId { get; set; }
        public int AdministrativeMemberId { get; set; }
        public int SemesterId { get; set; }
        public int ContributorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Faculty { get; set; }
        public string Career { get; set; }
        public string Semester { get; set; }
    }
}
