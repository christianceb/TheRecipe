using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRecipe
{
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
