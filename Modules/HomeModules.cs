using Nancy;
using ToDoList.Objects;
using System.Collections.Generic;

namespace ToDoList
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };
      Get["/all_categories"] = _ => {
        var allCategories = Category.GetAll();
        return View["all_categories.cshtml", allCategories];
      };
      Get["/all_categories/new"] = _ => {
        return View["category_form.cshtml"];
      };
      Post["/all_categories"] = _ => {
        var newCategory = new Category(Request.Form["category-name"]);
        var allCategories = Category.GetAll();
        return View["all_categories.cshtml", allCategories];
      };
      Get["/all_categories/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        var selectedCategory = Category.Find(parameters.id);
        var categoryTasks = selectedCategory.GetTasks();
        model.Add("category", selectedCategory);
        model.Add("tasks", categoryTasks);
        return View["category.cshtml", model];
      };
      Get["/all_categories/{id}/tasks/new"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Category selectedCategory = Category.Find(parameters.id);
        List<Task> allTasks = selectedCategory.getTasks();
        model.Add("category", selectedCategory);
        model.Add("tasks", allTasks);
        return View["category_tasks_form.cshtml", model];
      };
      Post["/tasks"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object.();
        Category selectedCategory = Category.Find(Request.Form["category-id"]);
        List<Task> categoryTasks = selectedCategory.GetTasks();
        string taskDescription = Request.Form["task-description"];
        Task newTask = new Task(taskDescription);
        categoryTasks.Add(newTask);
        model.Add("tasks", categoryTasks);
        model.Add("category", selectedCategory);
        return View["category.cshtml", model];
      };
      // Get["/tasks"] = _ => {
      //   List<Task> allTasks = Task.GetAll();
      //   return View["tasks.cshtml", allTasks];
      // };
      // Get["/tasks/new"] = _ => {
      //   return View["task_form.cshtml"];
      // };
      // Get["/tasks/{id}"] = parameters => {
      //   Task task = Task.Find(parameters.id);
      //   return View["task.cshtml", task];
      // };
      // Post["/tasks"] = _ => {
      //   Task newTask = new Task(Request.Form["new-task"]);
      //   List<Task> allTasks = Task.GetAll();
      //   return View["tasks.cshtml", allTasks];
      // };
    }
  }
}
