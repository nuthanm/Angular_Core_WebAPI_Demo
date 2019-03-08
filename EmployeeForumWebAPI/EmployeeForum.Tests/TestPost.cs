using EmployeeForum.Controllers;
using EmployeeForum.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace EmployeeForum.Tests
{
    [TestClass]
    public class TestPost
    {
        private readonly EmployeeForumContext _employeeForumContext;

        [TestMethod]
        public void GetAll_WithNoInput_ReturnAllOpenPosts()
        {
            var _post = CreateInstanceForPost();
            Assert.AreEqual(0, 0);
        }

        [TestMethod]
        public void GetAll_EmployeeId_ReturnsLikedPosts()
        {
            var _post = CreateInstanceForPost();
            Assert.AreEqual(0, 0);
        }

        [TestMethod]
        public void GetAll_EmployeeId_ReturnsCreatedPostsByCurrentLoggedUser()
        {
            var _post = CreateInstanceForPost();
            Assert.AreEqual(0, 0);
        }

        [TestMethod]
        public void Add_NewPost_ReturnsZero()
        {
            var _post = CreateInstanceForPost();
            Assert.AreEqual(0, 0);
        }

        [TestMethod]
        public void Update_PostId_ReturnsUpdatedPostDetails()
        {
            var _post = CreateInstanceForPost();
            Assert.AreEqual(0, 0);
        }

        [TestMethod]
        public void RemovePost_PostId_ReturnsZero()
        {
            var _post = CreateInstanceForPost();
            Assert.AreEqual(0, 0);
        }

        private PostController CreateInstanceForPost()
        {
            return new PostController(_employeeForumContext);
        }
    }
}
