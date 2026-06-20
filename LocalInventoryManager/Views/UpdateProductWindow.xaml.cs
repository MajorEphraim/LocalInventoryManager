using System.Windows;
using LocalInventoryManager.Models;

namespace LocalInventoryManager.Views
{
    public partial class UpdateProductWindow : Window
    {
        public Product Product { get; }

        public UpdateProductWindow(Product product)
        {
            InitializeComponent();

            Product = product;
            DataContext = Product;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}