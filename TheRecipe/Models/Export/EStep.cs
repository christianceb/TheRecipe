namespace TheRecipe
{
  using System;
  using System.Runtime.Serialization;

  [Serializable]
  public partial class EStep : ISerializable
  {
    public int Id { get; set; }

    public string Content { get; set; }

    public byte? Order { get; set; }

    public int RecipeID { get; set; }

    public EStep() { }

    public EStep(SerializationInfo info, StreamingContext context)
    {
      Id = (int)info.GetValue("Id", typeof(int));
      Content = (string)info.GetValue("Content", typeof(string));
      Order = (byte)info.GetValue("Order", typeof(byte));
      RecipeID = (byte)info.GetValue("RecipeID", typeof(byte));
    }

    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
      info.AddValue("Id", Id);
      info.AddValue("Content", Content);
      info.AddValue("Order", Order);
      info.AddValue("RecipeID", RecipeID);
    }
  }
}
