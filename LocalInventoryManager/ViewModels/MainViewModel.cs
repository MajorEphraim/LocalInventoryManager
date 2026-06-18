using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using LocalInventoryManager.Models;
using LocalInventoryManager.Services;

namespace LocalInventoryManager.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly ProductService _service;

        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged();
                UpdateMetrics();
            }
        }

        private Product _selectedProduct;
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                _selectedProduct = value;
                OnPropertyChanged();
            }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
            }
        }

        private decimal _totalAssetValue;
        public decimal TotalAssetValue
        {
            get => _totalAssetValue;
            set
            {
                _totalAssetValue = value;
                OnPropertyChanged();
            }
        }

        private int _totalStockUnits;
        public int TotalStockUnits
        {
            get => _totalStockUnits;
            set
            {
                _totalStockUnits = value;
                OnPropertyChanged();
            }
        }

        private int _lowStockCount;
        public int LowStockCount
        {
            get => _lowStockCount;
            set
            {
                _lowStockCount = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            _service = new ProductService();
            LoadProducts();
        }

        public void LoadProducts()
        {
            Products = new ObservableCollection<Product>(_service.GetAll());

            if (SelectedProduct != null)
            {
                SelectedProduct = Products.FirstOrDefault(
                    p => p.Id == SelectedProduct.Id
                );
            }
        }

        public void AddProduct(Product product)
        {
            _service.Add(product);
            LoadProducts();
        }

        public void UpdateProduct(Product product)
        {
            _service.Update(product);
            LoadProducts();
        }

        public void DeleteProduct(Product product)
        {
            _service.Delete(product.Id);

            SelectedProduct = null;

            LoadProducts();
        }

        public void CreateNewProduct()
        {
            SelectedProduct = new Product
            {
                Quantity = 0,
                Price = 0,
                LowStockThreshold = 0
            };
        }

        public bool CanSaveProduct()
        {
            return SelectedProduct != null
                && !string.IsNullOrWhiteSpace(SelectedProduct.Sku)
                && !string.IsNullOrWhiteSpace(SelectedProduct.ProductName)
                && SelectedProduct.Price >= 0
                && SelectedProduct.Quantity >= 0
                && SelectedProduct.LowStockThreshold >= 0;
        }

        private void UpdateMetrics()
        {
            if (Products == null)
                return;

            TotalAssetValue = Products.Sum(
                p => p.Price * p.Quantity
            );

            TotalStockUnits = Products.Sum(
                p => p.Quantity
            );

            LowStockCount = Products.Count(
                p => p.Quantity <= p.LowStockThreshold
            );
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(
            [CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(name)
            );
        }
    }
}