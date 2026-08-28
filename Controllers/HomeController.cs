using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using MetalBandName.Models;
using MetalBandName.Services;
using Microsoft.AspNetCore.Mvc;

namespace MetalBandName.Controllers;

public class HomeController : Controller
{
    private readonly ITodoStore todoStore;

    public HomeController(ITodoStore todoStore)
    {
        this.todoStore = todoStore;
    }

    public IActionResult Index()
    {
        var items = todoStore.GetAll();
        return View(new KanbanBoardViewModel
        {
            Backlog = items.Where(item => item.Status == TodoStatus.Backlog).ToList(),
            InProgress = items.Where(item => item.Status == TodoStatus.InProgress).ToList(),
            Done = items.Where(item => item.Status == TodoStatus.Done).ToList()
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Add([FromForm, Required, StringLength(120)] string title)
    {
        if (!ModelState.IsValid)
        {
            TempData["Error"] = "Give your task a title (up to 120 characters).";
            return RedirectToAction(nameof(Index));
        }

        todoStore.Add(title.Trim());
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Move(int id, TodoStatus status)
    {
        if (!ModelState.IsValid || !Enum.IsDefined(status))
        {
            return BadRequest();
        }

        if (!todoStore.Move(id, status))
        {
            return NotFound();
        }

        return RedirectToAction(nameof(Index));
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
