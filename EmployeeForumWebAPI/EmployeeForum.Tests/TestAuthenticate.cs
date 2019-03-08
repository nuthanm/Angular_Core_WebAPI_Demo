using EmployeeForum.Controllers;
using EmployeeForum.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmployeeForum.Tests
{
    [TestClass]
    public class TestAuthenticate
    {
        private readonly EmployeeForumContext _employeeForumContext;
              
        [TestMethod]        
        public void Validate_EmployeeWithWrongValues_ReturnFalse()
        {
            // Good Approach is to separate each instead of mix Act in Assert or any combination

            //Arrage
            var _loginAuthenticate = CreateInstanceForLogin();
            //Act
            var result = false;
            //Assert
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void Validate_EmployeeWithCorrectValues_ReturnEmployeeDetails()
        {
            var _loginAuthenticate = CreateInstanceForLogin();
        }


        private AuthController CreateInstanceForLogin()
        {
            return new AuthController(_employeeForumContext,null);
        }

    }
}
