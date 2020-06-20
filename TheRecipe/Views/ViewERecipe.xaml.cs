using System.Windows;

namespace TheRecipe
{
  /// <summary>
  /// Interaction logic for ViewRecipe.xaml
  /// </summary>
  public partial class ViewERecipe : Window
  {
    public ViewERecipe(ERecipe recipe)
    {
      DataContext = recipe;

      InitializeComponent();
    }

    private void BtnBack_Click(object sender, RoutedEventArgs e)
    {
      Close();
    }
  }
}
