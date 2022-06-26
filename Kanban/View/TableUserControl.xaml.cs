using Kanban.DataAccessLayer.Entities;
using Kanban.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kanban.View
{
    /// <summary>
    /// Logika interakcji dla klasy TableUserControl.xaml
    /// </summary>
    public partial class TableUserControl : UserControl
    {
        public static readonly DependencyProperty TableProperty =
            DependencyProperty.Register(
              nameof(Table),
              typeof(Table),
              typeof(TableUserControl),
              new PropertyMetadata(null));

        /// <summary>
        /// Publiczna właściwość Text
        /// </summary>
        public Table Table
        {
            get { return (Table)GetValue(TableProperty); }
            set { SetValue(TableProperty, value); }
        }

        public TableUserControl()
        {
            InitializeComponent();
        }
    }
}
