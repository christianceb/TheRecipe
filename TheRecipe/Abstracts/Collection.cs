using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRecipe
{
  /// <summary>
  /// Abstract class to augment ICollection's interfaces
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public abstract class Collection<T> : ICollection<T>
  {
    /// <summary>
    /// EF DbContext to be used across the object
    /// </summary>
    protected RecipesModel Db;
    protected List<T> List;
    
    /// <summary>
    /// The object currently in focus
    /// </summary>
    public T Current;

    public abstract bool Add(T item);
    public abstract List<T> Browse();
    public abstract bool Delete(T item);
    public abstract bool Delete(int id);
    public abstract bool Edit(T item);
    public abstract void Read(int id);
    public abstract List<string> Validate(T item);


    /// <summary>
    /// Discard changes to Db
    /// </summary>
    public void Discard()
    {
      var changedEntries = Db.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList();

      foreach (var entry in changedEntries)
      {
        switch (entry.State)
        {
          case EntityState.Modified:
            entry.CurrentValues.SetValues(entry.OriginalValues);
            entry.State = EntityState.Unchanged;
            break;
          case EntityState.Added:
            entry.State = EntityState.Detached;
            break;
          case EntityState.Deleted:
            entry.State = EntityState.Unchanged;
            break;
        }
      }
    }

    /// <summary>
    /// Opposite of Discard(), a general purpose "change persister" function
    /// </summary>
    /// <returns>True if successfully saved, false otherwise</returns>
    public bool Save()
    {
      bool success = false;

      using (DbContextTransaction transaction = Db.Database.BeginTransaction())
      {
        try
        {
          Db.SaveChanges();
          transaction.Commit();
          success = true;
        }
        catch (Exception exception)
        {
          transaction.Rollback();

          // Should you need to explain what the exception was
          _ = exception.Message;
        }
      }

      return success;
    }
  }
}
