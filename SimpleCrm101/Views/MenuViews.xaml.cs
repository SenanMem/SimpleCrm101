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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleCrm101.Views
{
    /// <summary>
    /// Interaction logic for MenuViews.xaml
    /// </summary>
    public partial class MenuViews : UserControl
    {
        public static MouseButtonEventHandler MouseDownEvn;
        public MenuViews()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(sender is Button btn)
            {
                if (btn.Content is StackPanel stackPanel)
                {
                    if (stackPanel.Children[1] is TextBlock textBlock)
                    {
                        tbCurrentPage.Text = textBlock.Text;
                    }
                }
            }
        }


        private void Grid_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            MouseDownEvn?.Invoke(sender,e);
        }
    }
}
