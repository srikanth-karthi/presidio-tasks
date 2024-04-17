namespace Request_tracker_library
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department_Head { get; set; }

        // Parameterized constructor
        public Department(int id, string name, string departmentHead)
        {
            Id = id;
            Name = name;
            Department_Head = departmentHead;
        }

        public override bool Equals(object? obj)
        {
            if (obj is Department otherDepartment)
            {
                return Id.Equals(otherDepartment.Id);
            }
            return false;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}, Department Head: {Department_Head}";
        }
    }
}
