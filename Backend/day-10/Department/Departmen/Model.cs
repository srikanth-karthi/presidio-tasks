using System;

namespace Departments
{
    public class Department
    {
        public int Id { get; set; }
        public string DepartmrntName { get; set; }
        public string Head { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {DepartmrntName}, Head: {Head}";
        }
    }
}
