using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ZOO_Accounting.Commands
{
    public static class Commands
    {
        public static readonly RoutedUICommand Exit = new RoutedUICommand
                (
                        "Exit",
                        "Exit",
                        typeof(Commands),
                        new InputGestureCollection()
                                {
                                        new KeyGesture(Key.F4, ModifierKeys.Alt)
                                }
                );
        public static readonly RoutedUICommand Authorize = new RoutedUICommand(
                        "Авторизоваться",
                        "Authorize",
                        typeof(Commands)
            );
        //Define more commands here, just like the one above
    }

}
