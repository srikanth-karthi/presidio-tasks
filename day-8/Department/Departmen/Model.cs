using System;

namespace Department
{
    public class Department
    {
        public int Id { get; set; }
        public string DepartmrntName { get; set; }
        public string Head { get; set; }

        public override string ToString()
        {
            return $"Department ID: {Id}, Name: {DepartmrntName}, Head: {Head}";
        }
    }
}
