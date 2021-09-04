﻿using Microsoft.EntityFrameworkCore;
using SynetecAssessment.Data.Interface;
using SynetecAssessment.Data.Model;
using System.Collections.Generic;
using System.Linq;

namespace SynetecAssessment.Data
{
    public class DbContextGenerator :IDbContextGenerator
    {
        
        public AppDbContext Create()
        {
            var context = new AppDbContext(GetOptions());
            if (context.Employees.Any()) return context;
            SeedData(context);
            return context;
        }

        private DbContextOptions<AppDbContext> GetOptions()
        {
            var dbContextOptionBuilder = new DbContextOptionsBuilder<AppDbContext>();
            dbContextOptionBuilder.UseInMemoryDatabase(databaseName: "HrDb");
            return dbContextOptionBuilder.Options;

        }
     
        public static void SeedData(AppDbContext context)
        {
            var departments = new List<Department>
            {
                new Department(1, "Finance", "The finance department for the company"),
                new Department(2, "Human Resources", "The Human Resources department for the company"),
                new Department(3, "IT", "The IT support department for the company"),
                new Department(4, "Marketing", "The Marketing department for the company")
            };

            var employees = new List<Employee>
            {
                new Employee(1, "John Smith", "Accountant (Senior)", 60000, 1),
                new Employee(2, "Janet Jones", "HR Director", 90000, 2),
                new Employee(3, "Robert Rinser", "IT Director", 95000, 3),
                new Employee(4, "Jilly Thornton", "Marketing Manager (Senior)", 55000, 4),
                new Employee(5, "Gemma Jones", "Marketing Manager (Junior)", 45000, 4),
                new Employee(6, "Peter Bateman", "IT Support Engineer", 35000, 3),
                new Employee(7, "Azimir Smirkov", "Creative Director", 62500, 4),
                new Employee(8, "Penelope Scunthorpe", "Creative Assistant", 38750, 4),
                new Employee(9, "Amil Kahn", "IT Support Engineer", 36000, 3),
                new Employee(10, "Joe Masters", "IT Support Engineer", 36500, 3),
                new Employee(11, "Paul Azgul", "HR Manager", 53000, 2),
                new Employee(12, "Jennifer Smith", "Accountant (Junior)", 48000, 1),
            };

            context.Departments.AddRange(departments);
            context.Employees.AddRange(employees);

            context.SaveChanges();
        }
    }
}
