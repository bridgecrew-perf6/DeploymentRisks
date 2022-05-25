using DeploymentRisks.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DeploymentRisks.DAL;

namespace DeploymentRisks.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = new ReadTaskModel() { };
            model.TaskList = new List<ToDoTask>();
            try
            {
                using (DeploymentRisksContext db = new DeploymentRisksContext())
                {                
                    var tasks = db.ToDoTasks.ToList();
                    if (tasks.Any())
                        model.TaskList = tasks;
                }
                return View(model);
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = "Index Error - " + e + Environment.NewLine + "---0---" + e.StackTrace });
            }
        }

        [HttpPost]
        public JsonResult AddNewTask(string inputText)
        {
            try
            {
                if(string.IsNullOrEmpty(inputText))
                    return Json(new { success = false, message = "Text was null"});

                using (DeploymentRisksContext db = new DeploymentRisksContext())
                {
                    ToDoTask task = new ToDoTask { Id = Guid.NewGuid(), Text = inputText };

                    db.ToDoTasks.Add(task);
                    db.SaveChanges();
                }
                return Json(new { success = true });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = "Error - " + e.Message });
            }
        }

        [HttpPost]
        public JsonResult CompliteTask(Guid? id)
        {
            try
            {
                using (DeploymentRisksContext db = new DeploymentRisksContext())
                {
                    if(id == null)
                        return Json(new { success = false, message = "id was null"});

                    var task = db.ToDoTasks.Where(x => x.Id == (Guid)id).FirstOrDefault();
                    if(task == null)
                        return Json(new { success = false, message = "task was null"});
                    db.ToDoTasks.Remove(task);
                    db.SaveChanges();
                }
                return Json(new { success = true, message = "task was complited" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = "Error - " + e.Message });
            }
        }

        [HttpPost]
        public JsonResult DeleteTask(Guid? id)
        {
            try
            {
                using (DeploymentRisksContext db = new DeploymentRisksContext())
                {
                    if (id == null)
                        return Json(new { success = false, message = "id was null" });

                    var task = db.ToDoTasks.Where(x => x.Id == (Guid)id).FirstOrDefault();
                    if (task == null)
                        return Json(new { success = false, message = "task was null" });
                    db.ToDoTasks.Remove(task);
                    db.SaveChanges();
                }
                return Json(new { success = true, message = "task was deleted" });
            }
            catch (Exception e)
            {
                return Json(new { success = false, message = "Error - " + e.Message });
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
