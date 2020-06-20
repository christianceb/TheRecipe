namespace TheRecipe
{
  using System;
  using System.Runtime.Serialization;

  [Serializable]
  public partial class EIngredient : ISerializable
  {
    public int Id { get; set; }

    public string Name { get; set; }

    public EIngredient() { }

    public EIngredient(SerializationInfo info, StreamingContext context)
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
