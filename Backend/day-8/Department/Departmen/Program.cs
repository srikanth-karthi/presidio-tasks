using System;

namespace Department
{
    class Program
    {
        static void Main(string[] args)
        {
            DepartmentRepository departmentRepository = new DepartmentRepository();

            Department department1 = new Department { Id = 1, DepartmrntName = "Department 1", Head = "Head of Department 1" };
            Department department2 = new Department { Id = 2, DepartmrntName = "Department 2", Head = "Head of Department 2" };

            departmentRepository.Add(department1);
            departmentRepository.Add(department2);

            try
            {
                Department retrievedDepartment = departmentRepository.Get(1);
                Console.WriteLine(retrievedDepartment);
            }
            catch (DepartmentNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }

         
            Department updatedDepartment = new Department { Id = 1, DepartmrntName = "Updated Department 1", Head = "Updated Head of Department 1" };
            Console.WriteLine( departmentRepository.Update(updatedDepartment));

           
            try
            {
                departmentRepository.Delete(4);
            }
            catch (DepartmentNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }

       
            var allDepartments = departmentRepository.GetAll();
            foreach (var dept in allDepartments)
            {
                Console.WriteLine(dept);
            }
        }
    }
}
