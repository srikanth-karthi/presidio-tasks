using System;
using System.Collections.Generic;

namespace Department
{

    internal class DepartmentNotFoundException : Exception
    {
        public DepartmentNotFoundException(string v) { }

    }

    public class DepartmentAlreadyFoundException : Exception
    {
        public DepartmentAlreadyFoundException(string v) { }

    }
    public class DepartmentRepository : IDepartment
    {
        readonly private Dictionary<int, Department> Departments;

        public DepartmentRepository()
        {
            Departments = new Dictionary<int, Department>();
        }

        public Department Add(Department department)
        {
            if (Departments.ContainsKey(department.Id))
            {
                throw new DepartmentAlreadyFoundException("Department with this ID already exists.");
            }

            Departments.Add(department.Id, department);
            return department;
        }

        public void Delete(int id)
        {
            if (!Departments.ContainsKey(id))
            {
                throw new DepartmentNotFoundException("Department with this ID not found.");
            }

            Departments.Remove(id);
            Console.WriteLine("Department deleted");
        }

        public Department Get(int id)
        {
            if (!Departments.ContainsKey(id))
            {
                throw new DepartmentNotFoundException("Department with this ID not found.");
            }

            return Departments[id];
        }

        public Dictionary<int,Department> GetAll()
        {
            return Departments;
        }

        public Department Update(Department department)
        {
            if (!Departments.ContainsKey(department.Id))
            {
                throw new DepartmentNotFoundException("Department with this ID not found.");
            }

            Departments[department.Id] = department;
            return department;
        }
    }


}
