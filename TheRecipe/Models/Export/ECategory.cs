using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TheRecipe
{
  [Serializable]
  public class ECategory : ISerializable
  {
    public int Id { get; set; }
    public string Name { get; set; }
    
    public ECategory() { }

    public ECategory(SerializationInfo info, StreamingContext context)
    {
      Id = (int)info.GetValue("Id", typeof(int));
      Name = (string)info.GetValue("Name", typeof(string));
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      info.AddValue("Id", Id);
      info.AddValue("Name", Name);
    }
  }
}
