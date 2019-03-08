using EmployeeForum.Controllers;
using EmployeeForum.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace EmployeeForum.Tests
{
    [TestClass]
    public class TestPostActivity
    {
        private readonly EmployeeForumContext _employeeForumContext;

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(1)]
        public void GetBookMark_EmpoyeeId_ReturnCountForBookMark(int employeeId)
        {
            var _postActivity = CreateInstanceForPostActivity();
            Assert.AreEqual(0, 0);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(1)]
        public void GetLike_EmployeeId_ReturnCountForLike(int employeeId)
        {
            var _postActivity = CreateInstanceForPostActivity();
            Assert.AreEqual(0, 0);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(1)]
        public void GetAll_EmployeeId_ReturnsActivityDetailsForLoggedUser(int employeeId)
        {
            var _postActivity = CreateInstanceForPostActivity();
            Assert.AreEqual(0, 0);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(1)]
        public void UpdateActitvity_PostId_ReturnsUpdatedPostDetailsWithActivity(int postId)
        {
            var _postActivity = CreateInstanceForPostActivity();
            Assert.AreEqual(0, 0);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(1)]
        public void Remove_PostId_ReturnsZero(int postActivityId)
        {
            var _postActivity = CreateInstanceForPostActivity();
            Assert.AreEqual(0, 0);
        }

        private PostActivityController CreateInstanceForPostActivity()
        {
            return new PostActivityController(_employeeForumContext);
        }
    }
}
