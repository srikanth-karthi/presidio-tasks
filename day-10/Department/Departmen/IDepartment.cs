using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Departments
{
    public interface IDepartment
    {
        Department Add(Department doctor);
        Department Get(int key);
        Dictionary<int, Department> GetAll();
        Department Update(Department doctor);
        void Delete(int key);

    }
}
