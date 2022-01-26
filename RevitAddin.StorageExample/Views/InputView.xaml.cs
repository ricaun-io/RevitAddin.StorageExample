using System;
using System.Windows;

namespace RevitAddin.StorageExample.Views
{
    public partial class InputView : Window
    {
        public string Text { get; set; }
        public InputView(string title = "")
        {
            InitializeComponent();
            InitializeWindow();
            this.MinWidth = 260;
            this.Title = title;
            this.textBox.Focus();
            this.okButton.Click += OkButton_Click;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.DialogResult = true;
            }
            catch { }
            this.Close();
        }

        #region InitializeWindow
        private void InitializeWindow()
        {
            this.SizeToContent = SizeToContent.WidthAndHeight;
            this.ShowInTaskbar = false;
            this.ResizeMode = ResizeMode.NoResize;
            this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            new System.Windows.Interop.WindowInteropHelper(this) { Owner = Autodesk.Windows.ComponentManager.ApplicationWindow };
        }
        #endregion
    }
}