using EmployeeForum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeForum.Enums;
namespace EmployeeForum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostActivityController : ControllerBase
    {
        private readonly EmployeeForumContext context;

        public PostActivityController(EmployeeForumContext databaseContext)
        {
            context = databaseContext;
        }


        // GET: api/PostActivity/employeeId/open
        [HttpGet("{employeeId}/open"), Authorize]
        public IActionResult GetOpenPosts(int employeeId)
        {
            var openPosts = (from posts in context.Posts
                             join employee in context.Employees on posts.EmployeeId equals employee.EmployeeId
                             join category in context.Categories on posts.CategoryId equals category.CategoryId
                             join reply in context.PostReplies on posts.PostId equals reply.PostId into emptyreplies
                             from reply in emptyreplies.DefaultIfEmpty()
                             where reply == null || reply.EmployeeId != employeeId
                             select new
                             {
                                 posts.PostId,
                                 category.CategoryName,
                                 employee.FirstName,
                                 posts.Title,
                                 posts.Description,
                                 posts.CreatedDate
                             }).OrderByDescending(date => date.CreatedDate).ToList();

            return Ok(openPosts);
        }


        // GET: api/PostActivity/employeeId/bookmark
        [HttpGet("{employeeId}/bookmark"), Authorize]
        public IActionResult GetPostBookmarks(int employeeId)
        {
            var bookmarkPosts = (from posts in context.Posts
                                 join postActivities in context.PostActivities on posts.PostId equals postActivities.PostId
                                 join employee in context.Employees on postActivities.EmployeeId equals employee.EmployeeId
                                 join category in context.Categories on posts.CategoryId equals category.CategoryId
                                 where postActivities.EmployeeId == employeeId && postActivities.IsBookmark == true
                                 select new
                                 {
                                     postActivities.PostId,
                                     category.CategoryName,
                                     employee.FirstName,
                                     posts.Title,
                                     posts.Description,
                                     posts.CreatedDate
                                 }).OrderByDescending(date => date.CreatedDate).ToList();

            return Ok(bookmarkPosts);
        }

        // GET: api/PostActivity/employeeId/like
        [HttpGet("{employeeId}/like"), Authorize]
        public IActionResult GetPostLikes(int employeeId)
        {
            var likedPosts = (from posts in context.Posts
                              join postActivities in context.PostActivities on posts.PostId equals postActivities.PostId
                              join employee in context.Employees on postActivities.EmployeeId equals employee.EmployeeId
                              join category in context.Categories on posts.CategoryId equals category.CategoryId
                              where postActivities.EmployeeId == employeeId && postActivities.IsLike == true
                              select new
                              {
                                  postActivities.PostId,
                                  category.CategoryName,
                                  employee.FirstName,
                                  posts.Title,
                                  posts.Description,
                                  posts.CreatedDate
                              }).OrderByDescending(date => date.CreatedDate).ToList();

            return Ok(likedPosts);
        }


        // GET: api/PostActivity/employeeId/like
        [HttpGet("{employeeId}/answer"), Authorize]
        public IActionResult GetPostAnswers(int employeeId)
        {
            var answeredPosts = (from postReply in context.PostReplies
                                 join posts in context.Posts on postReply.PostId equals posts.PostId
                                 join employee in context.Employees on posts.EmployeeId equals employee.EmployeeId
                                 join category in context.Categories on posts.CategoryId equals category.CategoryId
                                 where postReply.EmployeeId == employeeId
                                 select new
                                 {
                                     posts.PostId,
                                     category.CategoryName,
                                     employee.FirstName,
                                     posts.Title,
                                     posts.Description,
                                     posts.CreatedDate
                                 }).OrderByDescending(date => date.CreatedDate).ToList();

            return Ok(answeredPosts);
        }

        //GET api/postactivity/id => id is employeeid        
        [HttpGet("{employeeId}"), Authorize]
        public List<UserActivity> GetPostActivities(int employeeId)
        {
            var employee = context.Employees.Find(employeeId);

            List<UserActivity> listUserActivity = new List<UserActivity>();
            if (employee != null)
            {
                int activityTypeCount = 0;

                activityTypeCount = context.PostActivities
                                         .Where(aType => aType.IsBookmark && aType.EmployeeId == employeeId)
                                         .Count();
                listUserActivity.Add(new UserActivity { Activity = Activity.Bookmarked.ToString(), ActivityType = ActivityType.bookmark.ToString(), Count = activityTypeCount });

                activityTypeCount = context.PostActivities
                                     .Where(aType => aType.IsLike && aType.EmployeeId == employeeId)
                                     .Count();
                listUserActivity.Add(new UserActivity { Activity = Activity.Liked.ToString(), ActivityType = ActivityType.like.ToString(), Count = activityTypeCount });

                activityTypeCount = (from postReply in context.PostReplies
                                     join post in context.Posts on postReply.PostId equals post.PostId
                                     where postReply.EmployeeId == employeeId
                                     select postReply).Count();
                listUserActivity.Add(new UserActivity { Activity = Activity.Answered.ToString(), ActivityType = ActivityType.answer.ToString(), Count = activityTypeCount });

                activityTypeCount = (from posts in context.Posts
                                     join reply in context.PostReplies on posts.PostId equals reply.PostId into emptyreplies
                                     from reply in emptyreplies.DefaultIfEmpty()
                                     where reply == null || reply.EmployeeId != employeeId
                                     select posts
                                    ).Count();
                listUserActivity.Add(new UserActivity { Activity = Activity.Open.ToString(), ActivityType = ActivityType.open.ToString(), Count = activityTypeCount });

            }

            return listUserActivity;
        }

        // GET: api/PostActivity
        [HttpGet("{postId}/{employeeId}"), Authorize]
        public IActionResult GetPostActivityId(int postId, int employeeId)
        {
            PostActivity postActivity = GetPostActivityDetails(postId, employeeId);
            return Ok(postActivity);
        }

        // PUT: api/postactivity/id => id is 
        // Updates the records of Like/Bookmark posts for logged employee details
        [HttpPut("{id}"), Authorize]
        public async Task<IActionResult> PutPostActivity(int id, PostActivity postActivity)
        {           

            context.Entry(postActivity).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostActivityExists(id))
                {
                    return NotFound(new { message = Properties.Resources.FailureMessage });
                }
                else
                {
                    return Ok(new { message = Properties.Resources.FailureMessage });
                }
            }

            return Ok(Properties.Resources.SuccessMessage);
        }

        // POST: api/postactivity/activityType => activityType is bookmark/like
        // Creates the records of Like/Bookmark posts for logged employee details
        [HttpPost("{activityType}"), Authorize]
        public async Task<ActionResult<PostActivity>> AddPostActivity(string activityType, PostActivity postActivity)
        {
            context.PostActivities.Add(postActivity);
            await context.SaveChangesAsync();
            return Ok(Properties.Resources.SuccessMessage);
        }

        private bool PostActivityExists(int id)
        {
            return context.PostActivities.Any(e => e.PostActivityId == id);
        }

        private int GetPostActivityId(PostActivity postActivityRecord)
        {
            return (from postActivity in context.PostActivities
                    where postActivity.EmployeeId == postActivityRecord.EmployeeId && postActivity.PostId == postActivityRecord.PostId
                    select postActivity.PostActivityId
                    ).FirstOrDefault();
        }

        private PostActivity GetPostActivityDetails(int postId, int employeeId)
        {
            return (from postActivity in context.PostActivities
                    where postActivity.EmployeeId == employeeId && postActivity.PostId == postId
                    select new PostActivity { PostActivityId = postActivity.PostActivityId, IsBookmark = postActivity.IsBookmark, IsLike = postActivity.IsLike }
                ).FirstOrDefault();

        }
        public class UserActivity
        {
            public string Activity { get; set; }
            public string ActivityType { get; set; }
            public int Count { get; set; }
        }
    }




}

