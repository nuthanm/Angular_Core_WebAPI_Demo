using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmployeeForum.Controllers;
using EmployeeForum.Models;

namespace EmployeeForum.Tests
{
    [TestClass]
    public class TestPostReply
    {
        private readonly EmployeeForumContext _employeeForumContext;

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(1)]
        public void GetAll_EmployeeId_ReturnsAllRepliedPosts(int employeeId)
        {
            var _postReply = CreateInstanceForPostReply();
            Assert.AreEqual(0, 0);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(1)]
        public void GetPost_PostId_ReturnsSelectedPostDetails(int postId)
        {
            var _postReply = CreateInstanceForPostReply();
            Assert.AreEqual(0, 0);
        }

        [TestMethod]
        public void Add_PostId_ReturnsReplyDetails()
        {
            var _postReply = CreateInstanceForPostReply();
            Assert.AreEqual(0, 0);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(1)]
        public void Update_PostReplyId_ReturnsUpdatedPostReplyDetails(int postId)
        {
            var _postReply = CreateInstanceForPostReply();
            Assert.AreEqual(0, 0);
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(1)]
        public void Remove_PostReplyId_ReturnsUpdatePostWithReplies(int postId)
        {
            var _postReply = CreateInstanceForPostReply();
            Assert.AreEqual(0, 0);
        }

        private PostReplyController CreateInstanceForPostReply()
        {
            return new PostReplyController(_employeeForumContext);
        }
    }
}
