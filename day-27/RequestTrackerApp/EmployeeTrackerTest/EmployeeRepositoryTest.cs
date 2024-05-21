using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using RequestTrackerApp.Context;
using RequestTrackerApp.Interface;
using RequestTrackerApp.Interfaces;
using RequestTrackerApp.Model;

using RequestTrackerApp.Repository;
using RequestTrackerApp.Service;
using RequestTrackerApp.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeTrackerTest
{
    public class EmployeeRepositoryTest 
    {
        private RequestTrackercontext _context;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<RequestTrackercontext>()
                .UseInMemoryDatabase("dummyDB")
                .Options;
            _context = new RequestTrackercontext(options);
        }

        [Test]
        public async Task GetEmployeeTest()
        {
            // Arrange
            var employeeRepo = new EmployeeRepository(_context);
            var emp = await employeeRepo.Add(new Employee
            {
                Name = "Test1",
                Email = "Srikanthk",
                State = "Active",
                Role = "Admin",
            });

            // Mock the other dependencies
            var requestRepoMock = new Mock<IRepository<int, Requests>>();
            var tokenServiceMock = new Mock<ITokenService>();
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            var solutionRepoMock = new Mock<IRepository<int, RequestSolution>>();
            var feedbackRepoMock = new Mock<IRepository<int, SolutionFeedback>>();
            var responseRepoMock = new Mock<IRepository<int, SolutionResposnse>>();

            // Create EmployeeService with all dependencies
            var employeeService = new EmployeeService(
                requestRepoMock.Object,
                httpContextAccessorMock.Object,
                tokenServiceMock.Object,
                employeeRepo,
                solutionRepoMock.Object,
                feedbackRepoMock.Object,
                responseRepoMock.Object
            );

            // Action
            var result = await employeeService.GetByEmail(emp.Email);

            // Assert
            Assert.IsNotNull(result);
        }

        
    }
}
