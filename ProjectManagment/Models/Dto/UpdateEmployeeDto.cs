namespace ProjectManagment.Models.Dto
{
    public class UpdateEmployeeDto
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Email { get; set; }
    }
}
