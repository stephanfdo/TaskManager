using System;
using System.Linq;
using System.Threading.Tasks; // This is the system Task namespace
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<TaskController> _logger;

        public TaskController(
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            ILogger<TaskController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        // GET: Task
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var tasks = await _context.UserTasks
                .Where(t => t.UserId == currentUser.Id)
                .OrderByDescending(t => t.CreatedDate)
                .ToListAsync();

            return View(tasks);
        }

        // GET: Task/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User);
            var task = await _context.UserTasks
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == currentUser.Id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // GET: Task/Create
        public IActionResult Create()
        {
            _logger.LogInformation("Create GET action called");
            return View();
        }

        // POST: Task/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,DueDate")] UserTask task)
        {
            _logger.LogInformation("Create POST action called with Title: {Title}", task.Title);

            // Remove UserId from ModelState validate it manually
            ModelState.Remove("UserId");

            // Log the model state
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState is invalid for Create action");
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    foreach (var error in state.Errors)
                    {
                        _logger.LogWarning("Validation error for {Property}: {Error}", key, error.ErrorMessage);
                    }
                }
            }
            else
            {
                _logger.LogInformation("ModelState is valid for Create action");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    var currentUser = await _userManager.GetUserAsync(User);
                    if (currentUser == null)
                    {
                        _logger.LogError("Failed to get current user in Create action");
                        return RedirectToAction("Index", "Home");
                    }

                    task.UserId = currentUser.Id;
                    task.CreatedDate = DateTime.Now;

                    _logger.LogInformation("Adding task to context");
                    _context.Add(task);

                    _logger.LogInformation("Saving changes to database");
                    await _context.SaveChangesAsync();

                    _logger.LogInformation("Task created successfully with ID: {Id}", task.Id);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating task");
                ModelState.AddModelError("", "An error occurred while saving the task. Please try again.");
            }

            return View(task);
        }

        // GET: Task/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            _logger.LogInformation("Edit GET action called with ID: {Id}", id);

            if (id == null)
            {
                _logger.LogWarning("Edit GET called with null ID");
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User);
            var task = await _context.UserTasks
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == currentUser.Id);

            if (task == null)
            {
                _logger.LogWarning("Task not found for ID: {Id} and user: {UserId}", id, currentUser.Id);
                return NotFound();
            }

            _logger.LogInformation("Returning Edit view for task ID: {Id}", id);
            return View(task);
        }

        // POST: Task/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,DueDate,IsComplete")] UserTask task)
        {
            _logger.LogInformation("Edit POST action called with ID: {Id}", id);

            if (id != task.Id)
            {
                _logger.LogWarning("ID mismatch in Edit POST. Route ID: {RouteId}, Task ID: {TaskId}", id, task.Id);
                return NotFound();
            }

            // Remove UserId from ModelState validation manually
            ModelState.Remove("UserId");

            // Log the model state
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("ModelState is invalid for Edit action");
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    foreach (var error in state.Errors)
                    {
                        _logger.LogWarning("Validation error for {Property}: {Error}", key, error.ErrorMessage);
                    }
                }
            }
            else
            {
                _logger.LogInformation("ModelState is valid for Edit action");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            var existingTask = await _context.UserTasks
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == currentUser.Id);

            if (existingTask == null)
            {
                _logger.LogWarning("Task not found for ID: {Id} and user: {UserId}", id, currentUser.Id);
                return NotFound();
            }

            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Updating task properties");
                    existingTask.Title = task.Title;
                    existingTask.Description = task.Description;
                    existingTask.DueDate = task.DueDate;
                    existingTask.IsComplete = task.IsComplete;
                    // UserId is already set in existingTask

                    _logger.LogInformation("Updating task in context");
                    _context.Update(existingTask);

                    _logger.LogInformation("Saving changes to database");
                    await _context.SaveChangesAsync();

                    _logger.LogInformation("Task updated successfully with ID: {Id}", id);
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!TaskExists(task.Id))
                {
                    _logger.LogWarning("Task not found during concurrency exception");
                    return NotFound();
                }
                else
                {
                    _logger.LogError(ex, "Concurrency exception occurred while updating task");
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating task");
                ModelState.AddModelError("", "An error occurred while updating the task. Please try again.");
            }

            return View(task);
        }

        // GET: Task/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User);
            var task = await _context.UserTasks
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == currentUser.Id);

            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Task/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var task = await _context.UserTasks
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == currentUser.Id);

            if (task == null)
            {
                return NotFound();
            }

            _context.UserTasks.Remove(task);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Toggle completion status
        public async Task<IActionResult> ToggleComplete(int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var task = await _context.UserTasks
                .FirstOrDefaultAsync(m => m.Id == id && m.UserId == currentUser.Id);

            if (task == null)
            {
                return NotFound();
            }

            task.IsComplete = !task.IsComplete;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(int id)
        {
            return _context.UserTasks.Any(e => e.Id == id);
        }
    }
}