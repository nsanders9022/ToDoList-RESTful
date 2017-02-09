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
      //Displays the list of the categories (returns the list called instances)
      Get["/all_categories"] = _ => {
        var allCategories = Category.GetAll();
        return View["all_categories.cshtml", allCategories];
      };
      //When we select "Add a new category" on the index page, open the category form page
      Get["/all_categories/new"] = _ => {
        return View["category_form.cshtml"];
      };
      //Takes info from the form and makes it into a new object. Then we get all of the category objects and show them all on the page
      Post["/all_categories"] = _ => {
        var newCategory = new Category(Request.Form["category-name"]);
        var allCategories = Category.GetAll();
        return View["all_categories.cshtml", allCategories];
      };
      //List of all of the tasks in a single category
      //Creates a dictionary with the key as either a 'categry' or a 'task', and the value as the applicable value and task
      Get["/all_categories/{id}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        var selectedCategory = Category.Find(parameters.id);
        var categoryTasks = selectedCategory.GetTasks();
        model.Add("category", selectedCategory);
        model.Add("tasks", categoryTasks);
        return View["category.cshtml", model];
      };
      //Adding a task to a selected category
      //Displays the form to add a task to that category
      Get["/all_categories/{id}/tasks/new"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Category selectedCategory = Category.Find(parameters.id);
        List<Task> allTasks = selectedCategory.GetTasks();
        model.Add("category", selectedCategory);
        model.Add("tasks", allTasks);
        return View["category_tasks_form.cshtml", model];
      };
      //list of all the tasks in a single category
      Post["/tasks"] = _ => {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Category selectedCategory = Category.Find(Request.Form["category-id"]);//strongly typed
        List<Task> categoryTasks = selectedCategory.GetTasks();// strongly typed. Both are just a different way to type var SelecteCategory etc from previous Get method.
        string taskDescription = Request.Form["task-description"];
        Task newTask = new Task(taskDescription);
        categoryTasks.Add(newTask);
        model.Add("tasks", categoryTasks);
        model.Add("category", selectedCategory);
        return View["category.cshtml", model];
      };


      //OLD CODE BEFORE WE ADDED THE CATEGORIES
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
