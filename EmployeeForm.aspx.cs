using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI.WebControls;
using Microsoft.VisualBasic.FileIO;
using technical_assessment.Models;

namespace technical_assessment
{
    public partial class EmployeeForm : System.Web.UI.Page
    {
        private ApplicationDbContext dbContext = new ApplicationDbContext();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDropDownLists();

                try
                {
                    ReadAndParseFile("employees.txt");
                }
                catch (FileNotFoundException ex)
                {
                    lblErrorMessage.Text = "File not found. Please check the file path.";
                }

                List<Employee> employees = GetEmployeesFromDatabase();
                CalculateAndDisplayFilteredEmployees(employees);
            }
        }

        private List<Employee> GetEmployeesFromDatabase()
        {
            return dbContext.Employees.ToList();
        }

        private void CalculateAndDisplayFilteredEmployees(List<Employee> employees)
        {
            decimal x = 20;

            List<Employee> filteredEmployees = employees
                .Where(emp => CalculateOvertimeHourRate(emp.EmploymentType, emp.Salary) > x)
                .ToList();

            DisplayFilteredEmployees(filteredEmployees);
        }

        private void DisplayFilteredEmployees(List<Employee> employees)
        {
            gvEmployees.DataSource = employees;
            gvEmployees.DataBind();
        }

        private void BindDropDownLists()
        {
            lstMonthly.Items.Add(new ListItem("Option 1", "1"));
            lstMonthly.Items.Add(new ListItem("Option 2", "2"));

            lstHourly.Items.Add(new ListItem("Option A", "A"));
            lstHourly.Items.Add(new ListItem("Option B", "B"));

            lstFreeLancer.Items.Add(new ListItem("Option X", "X"));
            lstFreeLancer.Items.Add(new ListItem("Option Y", "Y"));
        }

        private void ReadAndParseFile(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException();
            }

            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters("\t");

                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();

                    string employeeName = fields[0];

                    // Add the employee name to the appropriate drop-down list
                    lstMonthly.Items.Add(new ListItem(employeeName));
                    // Add logic to fill other drop-down lists as needed
                }
            }
        }

        private void AddEmployee(string name, string address, string newGraduation, DateTime newBirthDate, string newEmploymentType)
        {
            Employee newEmployee = new Employee
            {
                Name = name,
                Address = address,
                BirthDate = newBirthDate,
                EmploymentType = newEmploymentType,
                Graduation = newGraduation
            };

            dbContext.Employees.Add(newEmployee);
            dbContext.SaveChanges();
        }

        private void EditEmployee(int employeeId, string newName, string newAddress, string newGraduation, DateTime newBirthDate, string newEmploymentType)
        {
            Employee employee = dbContext.Employees.Find(employeeId);
            if (employee != null)
            {
                employee.Name = newName;
                employee.Address = newAddress;
                employee.BirthDate = newBirthDate;
                employee.EmploymentType = newEmploymentType;
                employee.Graduation = newGraduation;

                dbContext.SaveChanges();
            }
        }

        private void DeleteEmployee(int employeeId)
        {
            Employee employee = dbContext.Employees.Find(employeeId);
            if (employee != null)
            {
                dbContext.Employees.Remove(employee);
                dbContext.SaveChanges();
            }
        }

        private decimal CalculateOvertimeHourRate(string employmentType, decimal salary)
        {
            decimal overtimeHourRate = 0;

            switch (employmentType)
            {
                case "Monthly":
                    overtimeHourRate = salary / 160;
                    break;
                case "Hourly":
                    overtimeHourRate = salary * 3 / 16;
                    break;
                case "Freelancer":
                    overtimeHourRate = salary * 1.5m;
                    break;
            }

            return overtimeHourRate;
        }
    }
}
