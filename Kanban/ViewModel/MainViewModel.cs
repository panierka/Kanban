using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Shapes;
using Kanban.ViewModel.Base;
using System.Windows;

namespace Kanban.ViewModel
{
    internal class MainViewModel : BaseViewModel
    {
        private RelayCommand mousemove = null;
        public void Student(object sender)
        {
            Rectangle rectangle = sender as Rectangle;
            if (rectangle != null && System.Windows.Input.Mouse.LeftButton == MouseButtonState.Pressed)
            {
                DragDrop.DoDragDrop(rectangle,
                                     rectangle.Fill.ToString(),
                                     DragDropEffects.Copy);
            }
            Console.WriteLine("std");
        }
        public RelayCommand p_MouseMove
        {
            get
            {
                if (mousemove == null)
                {
                    mousemove = new RelayCommand(Student, argument => true);
                }
                return mousemove;
            }
        }
    }
}
