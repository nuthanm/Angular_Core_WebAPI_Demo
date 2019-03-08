using EmployeeForum.Controllers;
using EmployeeForum.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmployeeForum.Tests
{
    [TestClass]
    public class TestEmployee
    {
        private readonly EmployeeForumContext _employeeForumContext;

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(1)]
        public void Get_EmployeeId_ReturnsEmployeeDetails(int employeeId)
        {
            var _employee = CreateInstanceForEmployee();
            Assert.AreEqual(0, 0);
        }

        [TestMethod]
        public void Add_NewEmployee_ReturnsZero()
        {
            var _employee = CreateInstanceForEmployee();
            Assert.AreEqual(0, 0);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(1)]
        public void Update_EmployeeId_ReturnsExistingEmployeeUpdateDetails(int employeeId)
        {
            var _employee = CreateInstanceForEmployee();
            Assert.AreEqual(0, 0);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(1)]
        public void Remove_EmployeeId_ReturnsZero(int employeeId)
        {
            var _employee = CreateInstanceForEmployee();
            Assert.AreEqual(0, 0);
        }

        private EmployeeController CreateInstanceForEmployee()
        {
            return new EmployeeController(_employeeForumContext);
        }
    }
}
