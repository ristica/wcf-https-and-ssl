using Core.Common.UI.Core;
using System.Collections.ObjectModel;
using Demo.Client.Contracts;
using System;
using GalaSoft.MvvmLight.Messaging;
using Demo.Admin.Messages;
using System.ComponentModel.Composition;
using Core.Common.Contracts;
using Demo.Client.Entities;
using Core.Common;
using System.ServiceModel;

namespace Demo.Admin.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MaintainProductsViewModel : ViewModelBase
    {
        #region Fields

        private readonly IServiceFactory _serviceFactory;
        private ObservableCollection<Product> _products;
        private EditProductDialogViewModel _editProductDialog;
        private Product _selectedProduct;

        #endregion

        #region Properties

        public EditProductDialogViewModel EditProductDialog
        {
            get { return this._editProductDialog; }
            set
            {
                if (this._editProductDialog == value) return;
                this._editProductDialog = value;
                OnPropertyChanged(() => this.EditProductDialog);
            }
        }

        public ObservableCollection<Product> Products
        {
            get { return this._products; }
            set
            {
                if (this._products == value) return;
                this._products = value;
                OnPropertyChanged(() => this.Products);
            }
        }

        public Product SelectedProduct
        {
            get { return this._selectedProduct; }
            set
            {
                if (this._selectedProduct == value) return;
                this._selectedProduct = value;
                OnPropertyChanged(() => this.SelectedProduct);
            }
        }

        #endregion

        #region Events

        public event EventHandler<ErrorMessageEventArgs> ErrorOccured;

        #endregion

        #region Commands

        public DelegateCommand<Product> EditProductCommand { get; private set; }
        public DelegateCommand<object> AddProductCommand { get; private set; }
        public DelegateCommand<Product> DeactivateProductCommand { get; private set; }
        public DelegateCommand<Product> ActivateProductCommand { get; private set; }

        #endregion

        #region Overrides

        public override string ViewTitle
        {
            get
            {
                return "Products";
            }
        }

        protected override void OnViewLoaded()
        {
            this._products = new ObservableCollection<Product>();
            this.LoadProductsWithHardcodedEndpoint();
        }

        #endregion

        #region C-Tor

        [ImportingConstructor]
        public MaintainProductsViewModel(IServiceFactory serviceFactory)
        {
            this._serviceFactory = serviceFactory;

            this.RegisterCommands();
            this.RegisterMessengers();
        }

        #endregion

        #region Methods

        private void RegisterMessengers()
        {
            Messenger.Default.Register<ProductChangedMessage>(this, this.ReloadProducts);
        }

        private void ReloadProducts(ProductChangedMessage message)
        {
            this.Products.Clear();
            var products = this._serviceFactory.CreateClient<IInventoryService>().GetProducts();
            foreach( var p in products)
            {
                this.Products.Add(p);
            }
        }

        private void RegisterCommands()
        {
            this.EditProductCommand = new DelegateCommand<Product>(OnEditProductCommand);
            this.AddProductCommand = new DelegateCommand<object>(OnAddProductCommand);
            this.DeactivateProductCommand = new DelegateCommand<Product>(OnDeactivateProductCommand);
            this.ActivateProductCommand = new DelegateCommand<Product>(OnActivateProductCommand);
        }

        private void LoadProductsWithHardcodedEndpoint()
        {
            WithClient(this._serviceFactory.CreateClient<IInventoryService>(), inventoryClient =>
            {
                var products = inventoryClient.GetProducts();
                if (products != null && products.Length > 0)
                {
                    foreach (var p in products)
                    {
                        this._products.Add(p);
                    }
                }
            });
        }

        #endregion

        #region On...Command

        private void OnEditProductCommand(Product product)
        {
            this.EditProductDialog = Container.GetExportedValue<EditProductDialogViewModel>();
            this.EditProductDialog.Title = "Edit product...";
            this.EditProductDialog.Model = product;
            this.EditProductDialog.IsOpen = true;
        }

        private void OnAddProductCommand(object obj)
        {
            this.EditProductDialog = Container.GetExportedValue<EditProductDialogViewModel>();
            this.EditProductDialog.Title = "Add product...";
            this.EditProductDialog.Model = new Product();
            this.EditProductDialog.IsOpen = true;
        }

        private void OnDeactivateProductCommand(Product product)
        {
            try
            {
                WithClient(this._serviceFactory.CreateClient<IInventoryService>(), inventoryClient =>
                {
                    product.IsActive = false;
                    inventoryClient.UpdateProduct(product);
                });
            }
            catch (FaultException ex)
            {
                ErrorOccured?.Invoke(this, new ErrorMessageEventArgs(ex.Message));
            }
            catch (Exception ex)
            {
                ErrorOccured?.Invoke(this, new ErrorMessageEventArgs(ex.Message));
            }
        }

        private void OnActivateProductCommand(Product product)
        {
            try
            {
                WithClient(this._serviceFactory.CreateClient<IInventoryService>(), inventoryClient =>
                {
                    product.IsActive = true;
                    inventoryClient.UpdateProduct(product);
                });
            }
            catch (FaultException ex)
            {
                ErrorOccured?.Invoke(this, new ErrorMessageEventArgs(ex.Message));
            }
            catch (Exception ex)
            {
                ErrorOccured?.Invoke(this, new ErrorMessageEventArgs(ex.Message));
            }
        }

        #endregion
    }
}
