using System;
using System.Windows;
using System.Windows.Forms;
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
            var area = Screen.PrimaryScreen.WorkingArea;

            Left = area.Left + (area.Width - ActualWidth) / 2;
            Top = area.Top + (area.Height - ActualHeight) / 2;
        }

        private void NewProduct_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SelectedProduct = new Product
            {
                Quantity = 0,
                Price = 0,
                LowStockThreshold = 0
            };
        }

        private void SaveProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedProduct == null)
                return;

            if (string.IsNullOrWhiteSpace(ViewModel.SelectedProduct.Sku))
            {
                System.Windows.MessageBox.Show(
                    "SKU is required.",
                    "Validation",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            if (string.IsNullOrWhiteSpace(ViewModel.SelectedProduct.ProductName))
            {
                System.Windows.MessageBox.Show(
                    "Product Name is required.",
                    "Validation",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            if (ViewModel.SelectedProduct.Id == 0)
            {
                ViewModel.AddProduct(ViewModel.SelectedProduct);
            }
            else
            {
                ViewModel.UpdateProduct(ViewModel.SelectedProduct);
            }

            System.Windows.MessageBox.Show(
                "Product saved successfully.",
                "Success",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedProduct == null)
                return;

            var result = System.Windows.MessageBox.Show(
                $"Delete '{ViewModel.SelectedProduct.ProductName}'?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result != MessageBoxResult.Yes)
                return;

            ViewModel.DeleteProduct(ViewModel.SelectedProduct);

            ViewModel.SelectedProduct = null;

            System.Windows.MessageBox.Show(
                "Product deleted successfully.",
                "Success",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }
    }
}