using Core.Common.Core;
using FluentValidation;

namespace Demo.Client.Entities
{
    public class Product : ObjectBase
    {
        #region Fields

        private int _productId;
        private string _articleNumber;
        private string _name;
        private string _description;
        private decimal _price;
        private byte[] _image;
        private bool _isActive;

        #endregion

        #region Properties

        public int ProductId
        {
            get { return this._productId; }
            set
            {
                if (this._productId == value) return;
                this._productId = value;
                OnPropertyChanged(() => ProductId);
            }
        }

        public string ArticleNumber
        {
            get { return this._articleNumber; }
            set
            {
                if (this._articleNumber == value) return;
                this._articleNumber = value;
                OnPropertyChanged(() => ArticleNumber);
            }
        }

        public string Name
        {
            get { return this._name; }
            set
            {
                if (this._name == value) return;
                this._name = value;
                OnPropertyChanged(() => Name);
            }
        }

        public string Description
        {
            get { return this._description; }
            set
            {
                if (this._description == value) return;
                this._description = value;
                OnPropertyChanged(() => Description);
            }
        }

        public decimal Price
        {
            get { return this._price; }
            set
            {
                if (this._price == value) return;
                this._price = value;
                OnPropertyChanged(() => ProductId);
            }
        }

        public byte[] Image
        {
            get { return this._image; }
            set
            {
                if (this._image == value) return;
                this._image = value;
                OnPropertyChanged(() => Image);
            }
        }

        public bool IsActive
        {
            get { return this._isActive; }
            set
            {
                if (this._isActive == value) return;
                this._isActive = value;
                OnPropertyChanged(() => IsActive);
            }
        }
        #endregion

        #region Validation

        private class ProductValidator : AbstractValidator<Product>
        {
            public ProductValidator()
            {
                RuleFor(product => product.Name).NotEmpty().Length(2, 20);
                RuleFor(product => product.Description).NotEmpty().Length(2, 50);
                RuleFor(product => product.Price).NotEmpty().GreaterThan(0);
            }
        }

        protected override IValidator GetValidator()
        {
            return new ProductValidator();
        }

        #endregion
    }
}
