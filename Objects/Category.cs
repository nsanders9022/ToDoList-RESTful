using System.Collections.Generic;

namespace ToDoList.Objects
{
  public class Category
  {
    private static List<Category> _instances = new List<Category>{};
    private string _name;
    private int _id;
    private List<Task> _tasks;
  }
}
