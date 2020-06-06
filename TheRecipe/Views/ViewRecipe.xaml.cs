using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TheRecipe
{
  /// <summary>
  /// Interaction logic for ViewRecipe.xaml
  /// </summary>
  public partial class ViewRecipe : Window
  {
    public ViewRecipe()
    {
      InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
      FormRecipe formRecipe = new FormRecipe();
      formRecipe.ShowDialog();
    }

    private void btnBack_Click(object sender, RoutedEventArgs e)
    {
      Close();
    }
  }
}
