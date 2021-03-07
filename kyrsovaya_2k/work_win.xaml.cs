using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace kyrsovaya_2k
{
    /// <summary>
    /// Interaction logic for work_win.xaml
    /// </summary>
    public partial class work_win : Window
    {
        public work_win(List <db_work.user> Kurisu)
        {
            InitializeComponent();
            name.Text = " "+ Kurisu[1].surname + " " + Kurisu[1].name + " " + Kurisu[1].patr;
        }
    }
}
