using System;
using System.Windows;
using LocalInventoryManager.Models;
using LocalInventoryManager.ViewModels;

namespace LocalInventoryManager.Views
{
    public partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get; }

        public MainWindow()
        {
            InitializeComponent();

            ViewModel = new MainViewModel();
            DataContext = ViewModel;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(
                new Action(CenterWindowOnScreen),
                System.Windows.Threading.DispatcherPriority.ApplicationIdle
            );
        }

        private void CenterWindowOnScreen()
        {
            // Explicitly call Forms here to avoid namespace ambiguity errors
            var area = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;

            Left = area.Left + (area.Width - ActualWidth) / 2;
            Top = area.Top + (area.Height - ActualHeight) / 2;
        }

        private void NewProduct_Click(object sender, RoutedEventArgs e)
        {
            // Instantly instantiate the product here to keep it alive in the View context
            var newProduct = new Product
            {
                Id = 0,
                Sku = "",
                ProductName = "",
                Quantity = 0,
                Price = 0,
                LowStockThreshold = 0
            };

            ViewModel.SelectedProduct = newProduct;
        }

        private void SaveProduct_Click(object sender, RoutedEventArgs e)
        {
            // Fallback safety check: If it's null, see if the UI has data we can recover
            if (ViewModel.SelectedProduct == null)
            {
                System.Windows.MessageBox.Show(
                    "Please click 'New Product' first before entering details.",
                    "No Product Selected",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            var productToSave = ViewModel.SelectedProduct;

            // Strict text validation
            if (string.IsNullOrWhiteSpace(productToSave.Sku))
            {
                System.Windows.MessageBox.Show("SKU is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(productToSave.ProductName))
            {
                System.Windows.MessageBox.Show("Product Name is required.", "Validation", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Route to database operations via the ViewModel
            if (productToSave.Id == 0)
            {
                ViewModel.AddProduct(productToSave);
            }
            else
            {
                ViewModel.UpdateProduct(productToSave);
            }

            System.Windows.MessageBox.Show(
                "Product saved successfully to local database!",
                "Success",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedProduct == null)
            {
                System.Windows.MessageBox.Show("Please select a product to delete.", "Notice", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            var result = System.Windows.MessageBox.Show(
                $"Delete '{ViewModel.SelectedProduct.ProductName}'?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            ViewModel.DeleteProduct(ViewModel.SelectedProduct);

            System.Windows.MessageBox.Show("Product deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}