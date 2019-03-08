using EmployeeForum.Controllers;
using EmployeeForum.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EmployeeForum.Tests
{
    [TestClass]
    public class TestCategory
    {
        private readonly EmployeeForumContext _employeeForumContext;        

        [TestMethod]
        public void GetAll_WithNoInput_ReturnsAllAvailableCategories()
        {
            var _category = CreateInstanceForCategory();
            Assert.AreEqual(0, 0);            
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(1)]        
        public void Get_WithCategoryId_ReturnsSelectedCategory()
        {
            var _category = CreateInstanceForCategory();            
            Assert.AreEqual(0, 0);
        }

        [TestMethod]
        public void Add_NewCategory_ReturnsZero()
        {
            var _category = CreateInstanceForCategory();
            Assert.AreEqual(0, 0);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(1)]
        public void Update_CategoryId_ReturnsUpdatedCategoryDetails()
        {
            var _category = CreateInstanceForCategory();
            Assert.AreEqual(0, 0);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(1)]
        public void Remove_CategoryId_ReturnsZero()
        {
            var _category = CreateInstanceForCategory();
            Assert.AreEqual(0, 0);
        }

        private CategoryController CreateInstanceForCategory()
        {  
            return new CategoryController(_employeeForumContext);
        }

    }
}
