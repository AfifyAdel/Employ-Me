using EmployMe.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }
        public ActionResult Details(int JobId)
        {
            var job = db.Jobs.Find(JobId);
            if (job == null)
            {
                return HttpNotFound();
            }
            Session["JobId"] = JobId;
            return View(job);
        }
        [Authorize]
        public ActionResult Apply()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Apply(string Message)
        {
            var userId = User.Identity.GetUserId();
            var jobId = (int)Session["JobId"];
            var check = db.ApplyForJobs.Where(x => x.JobId == jobId && x.UserId == userId).ToList();
            if (check.Count < 1)
            {
                var job = new ApplyForJob();
                job.UserId = userId;
                job.JobId = jobId;
                job.Message = Message;
                job.ApllyDate = DateTime.Now;
                db.ApplyForJobs.Add(job);
                db.SaveChanges();
                ViewBag.Result = "Success";
            }
            else
            {
                ViewBag.Result = "You apply for this job before";
            }
            return View();
        }
        public ActionResult GetJobsByUser()
        {
            var userId = User.Identity.GetUserId();
            var jobs = db.ApplyForJobs.Where(x => x.UserId == userId);
            return View(jobs.ToList());
        }
        [Authorize]
        public ActionResult DetailsOfJob(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }
        // GET: jobs/Edit/5
        public ActionResult Edit(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
                return HttpNotFound();
            return View(job);
        }

        // POST: Roles/Edit/5
        [HttpPost]
        public ActionResult Edit(ApplyForJob job)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    job.ApllyDate = DateTime.Now;
                    db.Entry(job).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("GetJobsByUser");
                }
                else
                    return View(job);
            }
            catch
            {
                return View();
            }
        }
        // GET: Roles/Delete/5
        public ActionResult Delete(int id)
        {
            var job = db.ApplyForJobs.Find(id);
            if (job == null)
                return HttpNotFound();
            return View(job);
        }

        // POST: Roles/Delete/5
        [HttpPost]
        public ActionResult Delete(ApplyForJob job)
        {
            try
            {
                // TODO: Add delete logic here

                if (ModelState.IsValid)
                {
                    var deletedJob = db.ApplyForJobs.Find(job.Id);
                    db.ApplyForJobs.Remove(deletedJob);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                    return View(job);
            }
            catch
            {
                return View(job);
            }
        }
        [Authorize]
        public ActionResult GetJobsByPublisher()
        {
            var UserID = User.Identity.GetUserId();
            var Jobs = from app in db.ApplyForJobs
                       join job in db.Jobs
                       on app.JobId equals job.Id
                       where job.User.Id == UserID
                       select app;

            var grouped = from job in Jobs
                          group job by job.Job.JobTitle
                          into gr
                          select new JobsViewModel
                          {
                              JobTitle = gr.Key,
                              Items = gr
                          };
            return View(grouped.ToList());
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
       
    }
}