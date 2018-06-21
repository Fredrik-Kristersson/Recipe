using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;

namespace Recipe
{
    public partial class AddRecipeDialog
    {
        public AddRecipeDialog()
        {
            InitializeComponent();

            // Insert code required on object creation below this point.
        }


        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = CheckValid(nameBox);
            if (isValid)
            {
                DialogResult = true;
            }
        }

        private static bool CheckValid(DependencyObject obj)
        {
            int childCount = VisualTreeHelper.GetChildrenCount(obj);
            if (Validation.GetHasError(obj))
            {
                return false;
            }
            for (int i = 0; i < childCount; i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (Validation.GetHasError(obj))
                {
                    return false;
                }
                if (!CheckValid(child))
                {
                    return false;
                }
            }
            return true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            nameBox.Focus();
        }

    }
}