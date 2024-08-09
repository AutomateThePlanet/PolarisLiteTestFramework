namespace DemoSystemTests.Builder;
public class EmployeeBuilder
{
    private Employee _employee;

    private readonly EmployeeRepository _employeeRepository;

    public EmployeeBuilder(EmployeeRepository employeeRepository)
    {
        _employee = new Employee();
        _employeeRepository = employeeRepository;
    }

    public EmployeeBuilder WithDefaultConfiguration()
    {
        _employee = EmployeeFactory.GenerateEmployee();
        return this;
    }

    public EmployeeBuilder WithEmployeeId(long employeeId)
    {
        _employee.EmployeeId = employeeId;
        return this;
    }

    public EmployeeBuilder WithName(string firstName, string lastName)
    {
        _employee.FirstName = firstName;
        _employee.LastName = lastName;
        return this;
    }

    public EmployeeBuilder WithTitle(string title)
    {
        _employee.Title = title;
        return this;
    }

    public EmployeeBuilder ReportsTo(Employee manager)
    {
        _employee.ReportsToNavigation = manager;
        _employee.ReportsTo = manager?.EmployeeId;
        return this;
    }

    public EmployeeBuilder WithContactInfo(string email, string phone)
    {
        _employee.Email = email;
        _employee.Phone = phone;
        return this;
    }

    public EmployeeBuilder WithAddress(string address, string city, string state, string postalCode, string country)
    {
        _employee.Address = address;
        _employee.City = city;
        _employee.State = state;
        _employee.PostalCode = postalCode;
        _employee.Country = country;
        return this;
    }

    public Employee Build()
    {
        var savedEmployee = _employeeRepository.CreateAsync(_employee).Result;
        return savedEmployee;
    }
}
