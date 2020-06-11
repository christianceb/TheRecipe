using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRecipe
{
  /// <summary>
  /// Interface for standardizing BREAD on collections such as List of things
  /// </summary>
  /// <typeparam name="T">Type to be expected across the class</typeparam>
  interface ICollection<T>
  {
    List<T> Browse();
    void Read(int id);
    bool Add(T item);
    bool Edit(T item);
    bool Delete(T item);
    bool Delete(int id);
    bool Save();
    List<string> Validate(T item);
  }
}
