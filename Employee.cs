using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace technical_assessment
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public DateTime BirthDate { get; set; }
        public string Graduation { get; set; }
        public string EmploymentType { get; set; }
        public decimal Salary { get; set; }
        public decimal HourlyRate { get; set; }
    }
    public class EmployeeManager
    {
        private List<Employee> employees = new List<Employee>();

        // Add employee
        public void AddEmployee(Employee employee)
        {
            employees.Add(employee);
        }

        // Edit employee
        public void EditEmployee(Employee employee)
        {
            // Implement edit logic here
        }

        // Delete employee
        public void DeleteEmployee(Employee employee)
        {
            employees.Remove(employee);
        }
    }
}