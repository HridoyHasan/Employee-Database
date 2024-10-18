namespace EmployeeManagement.Models
{
   public class Employee
{
    public int Id { get; set; }
    public string NID { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    // public DateTime? JoinDate { get; set; }
    public string Organization { get; set; }
    public string Expertise { get; set; }
    public string? CriminalRecord { get; set; }
    public string? ProfilePicturePath { get; set; } // Optional field for file path
    public bool IsDeleted { get; set; }
}

}
