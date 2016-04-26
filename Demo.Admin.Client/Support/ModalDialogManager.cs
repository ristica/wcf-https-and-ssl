using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Demo.Admin.Client.Support
{
    public class ModalDialogManager : Control
    {
        #region Fields

        private Window _window;
        private bool _internalClose;
        private bool _externalClose;

        #endregion

        #region Properties

        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        #endregion

        #region DependencyProperties for Commands

        public static readonly DependencyProperty CloseCommandProperty =
            DependencyProperty.Register("CloseCommand", typeof(ICommand), typeof(ModalDialogManager), new UIPropertyMetadata(null));

        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(ModalDialogManager), new UIPropertyMetadata(false, IsOpenChanged));

        #endregion

        #region DependencyProperties of the dialog

        public double DialogHeight
        {
            get { return (double)GetValue(DialogHeightProperty); }
            set { SetValue(DialogHeightProperty, value); }
        }
        public static readonly DependencyProperty DialogHeightProperty =
            DependencyProperty.Register("DialogHeight", typeof(double), typeof(ModalDialogManager));

        public double DialogWidth
        {
            get { return (double)GetValue(DialogWidthProperty); }
            set { SetValue(DialogWidthProperty, value); }
        }
        public static readonly DependencyProperty DialogWidthProperty =
            DependencyProperty.Register("DialogWidth", typeof(double), typeof(ModalDialogManager));

        public ResizeMode DialogResizeMode
        {
            get { return (ResizeMode)GetValue(DialogResizeModeProperty); }
            set { SetValue(DialogResizeModeProperty, value); }
        }
        public static readonly DependencyProperty DialogResizeModeProperty =
            DependencyProperty.Register("DialogResizeMode", typeof(ResizeMode), typeof(ModalDialogManager));

        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }
        public static readonly DependencyProperty IconProperty =
            DependencyProperty.Register("Icon", typeof(ImageSource), typeof(ModalDialogManager), new UIPropertyMetadata(null));

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(ModalDialogManager), new UIPropertyMetadata(null));

        #endregion

        #region C-Tor

        static ModalDialogManager()
        {

        }

        #endregion

        #region ICommands

        public ICommand CloseCommand
        {
            private get { return (ICommand)GetValue(CloseCommandProperty); }
            set { SetValue(CloseCommandProperty, value); }
        }

        #endregion

        #region Methods

        public static void IsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var m = d as ModalDialogManager;
            var newVal = (bool)e.NewValue;
            if (newVal)
            {
                if (m != null) m.Show();
            }
            else
            {
                if (m != null) m.Close();
            }
        }

        #endregion

        #region Events

        private void OnWindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this._internalClose) return;

            this._externalClose = true;

            if (this.CloseCommand != null)
            {
                this.CloseCommand.Execute(null);
            }

            this._externalClose = false;
        }

        private void Close()
        {
            this._internalClose = true;

            if (!this._externalClose)
            {
                this._window.Close();
            }

            this._window = null;
            this._internalClose = false;
        }

        #endregion

        #region Helpers

        private void Show()
        {
            if (this._window != null)
            {
                this.Close();
            }

            var w                   = new Window();

            this._window            = w;
            w.Owner                 = this.GetParentWindow(this);
            w.DataContext           = this.DataContext;
            w.SetBinding(ContentControl.ContentProperty, "");

            w.Title                 = this.Title;
            w.Icon                  = this.Icon;
            w.Height                = this.DialogHeight;
            w.Width                 = this.DialogWidth;
            w.ResizeMode            = this.DialogResizeMode;
            w.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            w.Closing               += OnWindowClosing;

            if (this._window != null)
            {
                w.ShowDialog();
            }
        }

        private Window GetParentWindow(FrameworkElement current)
        {
            if (current is Window)
            {
                return current as Window;
            }
            if (current.Parent is FrameworkElement)
            {
                return GetParentWindow(current.Parent as FrameworkElement);
            }
            return null;
        }

        #endregion

    }
}  