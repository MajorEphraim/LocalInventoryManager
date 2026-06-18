using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using LocalInventoryManager.Models;
using LocalInventoryManager.Services;

namespace LocalInventoryManager.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly ProductService _service; // instance of the service to interact with the database

        private ObservableCollection<Product> _products; // collection of products that will be displayed in the UI
        public ObservableCollection<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
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
            Products = new ObservableCollection<Product>(_service.GetAll()); //calling the service to get all products from the database and assigning it to the Products collection
        }

        // Methods to add, update, and delete products
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
            LoadProducts();
        }

        public event PropertyChangedEventHandler PropertyChanged; // event that is triggered when a property value changes


        // Reads the changes in properties and notifies the UI to update accordingly
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}