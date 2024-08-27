using NUnit.Framework;
using Departments; 

namespace DepartmentTest
{
    public class Tests
    {
        private DepartmentRepository repository;
        Department department;

        [SetUp]
        public void Setup()
        {
     
            repository = new DepartmentRepository();
          department = new Department() { Id = 1, DepartmrntName = "IT", Head = "ram" };
            repository.Add(department);
        }

        [Test]
        public void Test1()
        {
           


       
     
        }
        [Test]
        public void AddFailTest()
        {
            //Arrange 
            Department department = new Department() {Id=2, DepartmrntName = "IT", Head = "sam" };
            repository.Add(department);

            department = new Department() { Id = 2, DepartmrntName = "IT", Head = "sam" };
            try
            {
                var result = repository.Add(department);
                Assert.IsNull(result);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
         

            //Assert

        }
        [TestCase(7)]
        [TestCase(2)]
        public void findelement(int id)
        {
            //Arrange 


              //  var result = repository.Get(id);



            //Assert.AreEqual(result.Id,id);
            Assert.Throws<ArgumentException>(() => repository.Get(id));




            //Assert

        }
    }
}
