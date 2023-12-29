namespace EMS.Models
{
    public class EmployeeListViewModel : IEnumerable<Employee>
    {
        public List<Employee> Employees { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems / PageSize);

        public IEnumerator<Employee> GetEnumerator()
        {
            return Employees.GetEnumerator();
        }
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
