using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRecipe
{
  public abstract class Collection<T> : ICollection<T>
  {
    protected RecipesModel Db;
    protected List<T> List;

    public abstract bool Add(T item);
    public abstract List<T> Browse();
    public abstract bool Delete(T item);
    public abstract bool Delete(int id);
    public abstract bool Edit(T item);
    public abstract void Read(int id);
  }
}
