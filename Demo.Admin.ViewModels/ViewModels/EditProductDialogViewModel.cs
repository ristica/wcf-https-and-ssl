using Core.Common.UI.Core;
using System.Collections.Generic;
using Demo.Client.Contracts;
using GalaSoft.MvvmLight.Messaging;
using Demo.Admin.Messages;
using System.ComponentModel.Composition;
using Demo.Client.Entities;
using Core.Common.Core;

namespace Demo.Admin.ViewModels
{
    [Export]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class EditProductDialogViewModel : ViewModelBase
    {
        #region Fields

        private bool _isOpen;
        private string _title;
        private Product _model;
        private readonly IInventoryService _inventoryService;
        private Product _currentModel;
        private IMessenger _messenger;

        #endregion

        #region Properties

        public bool IsOpen
        {
            get { return this._isOpen; }
            set
            {
                this._isOpen = value;
                OnPropertyChanged(() => this.IsOpen);
            }
        }

        public string Title
        {
            get { return this._title; }
            set
            {
                this._title = value;
                OnPropertyChanged(() => this.Title);
            }
        }

        public Product Model
        {
            get { return this._model; }
            set
            {
                if (this._model == value) return;
                this._model = value;
                OnPropertyChanged(() => this.Model);
                this.SetCurrentProduct(this._model);
            }
        }

        public Product CurrentProduct
        {
            get { return this._currentModel; }
            set
            {
                if (this._currentModel == value) return;
                this._currentModel = value;
                OnPropertyChanged(() => this.CurrentProduct);
            }
        }

        #endregion

        #region Commands

        public DelegateCommand<object> OkCommand { get; private set; }
        public DelegateCommand<object> SaveCommand { get; private set; }
        public DelegateCommand<object> CancelCommand { get; private set; }

        #endregion

        #region C-Tor

        [ImportingConstructor]
        public EditProductDialogViewModel(IInventoryService inventoryService)
        {
            this._inventoryService = inventoryService;
            this.RegisterCommands();
            this.CurrentProduct = null;
        }

        /// <summary>
        /// only for testing purposes
        /// need to mock IMessenger
        /// </summary>
        /// <param name="inventoryService"></param>
        /// <param name="messenger"></param>
        public EditProductDialogViewModel(IInventoryService inventoryService, IMessenger messenger)
        {
            this._inventoryService = inventoryService;
            this.RegisterCommands();
            this.CurrentProduct = null;

            this._messenger = messenger;
        }

        #endregion

        #region Overrides

        protected override void AddModels(List<ObjectBase> models)
        {
            models.Add(this.CurrentProduct);
        }

        #endregion

        #region Methods

        private void RegisterCommands()
        {
            this.OkCommand = new DelegateCommand<object>(OnOkCommand);
            this.SaveCommand = new DelegateCommand<object>(OnSaveCommand);
            this.CancelCommand = new DelegateCommand<object>(OnCancelCommand);
        }

        private void Close()
        {
            this.IsOpen = false;
        }

        private void SetCurrentProduct(Product product)
        {
            this.CurrentProduct = new Product();

            this.CurrentProduct.ProductId       = product.ProductId;
            this.CurrentProduct.ArticleNumber   = product.ArticleNumber;
            this.CurrentProduct.Name            = product.Name;
            this.CurrentProduct.Description     = product.Description;
            this.CurrentProduct.IsActive        = product.IsActive;
            this.CurrentProduct.Price           = product.Price;
        }

        #endregion

        #region On...Command

        private void OnOkCommand(object obj)
        {
            this.Close();
        }

        private void OnCancelCommand(object obj)
        {
            this.CurrentProduct = null;
            this.OkCommand.Execute(null);
        }

        private void OnSaveCommand(object obj)
        {
            ValidateModel();

            if (!IsValid) return;

            var p = this._inventoryService.GetProductById(this.CurrentProduct.ProductId, true) ?? new Product();

            p.Name = this.CurrentProduct.Name;
            p.Description = this.CurrentProduct.Description;
            p.Price = this.CurrentProduct.Price;
            p.IsActive = this.CurrentProduct.IsActive;

            var result = this._inventoryService.UpdateProduct(p);

            if (this._messenger == null)
            {
                Messenger.Default.Send(new ProductChangedMessage());
            }            

            this.OkCommand.Execute(null);
        }

        #endregion        
    }
}
