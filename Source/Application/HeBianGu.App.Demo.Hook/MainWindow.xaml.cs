using HeBianGu.General.HookService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HeBianGu.App.Demo.Hook
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
        } 

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            Action<ShortCutEntity> action = l =>
              {
                  Debug.WriteLine(l);

                  this.tb_output.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - 注册事件被触发 ： " + l.ToString() + Environment.NewLine+ this.tb_output.Text;
              };

            {
                ShortCutEntity shortcut = new ShortCutEntity();

                KeyEntity keyEntity = new KeyEntity();
                keyEntity.Key = Keys.D;
                shortcut.Add(keyEntity);

                keyEntity = new KeyEntity();
                keyEntity.Key = Keys.D;
                shortcut.Add(keyEntity);

                ShortCutService.Instance.RegisterCommand(shortcut, action);

                this.tb.Text +="注册快捷键: " +shortcut.ToString() + Environment.NewLine;
            }


            {
                // Todo ：双击大小写切换 
                ShortCutEntity shortcut = new ShortCutEntity();

                shortcut = new ShortCutEntity();

                KeyEntity up = new KeyEntity();
                up.Key = Keys.Up;
                shortcut.Add(up);

                ShortCutService.Instance.RegisterCommand(shortcut, action);

                this.tb.Text += "注册快捷键: " + shortcut.ToString() + Environment.NewLine;
            }


            {
                ShortCutEntity shortcut = new ShortCutEntity();

                KeyEntity down = new KeyEntity();
                down.Key = Keys.Down;
                shortcut.Add(down);

                ShortCutService.Instance.RegisterCommand(shortcut, action);

                this.tb.Text += "注册快捷键: " + shortcut.ToString() + Environment.NewLine;
            }


            {
                // Todo ：双击Ctrl键 
                ShortCutEntity shortcut = new ShortCutEntity();

                KeyEntity c1 = new KeyEntity();
                c1.Key = Keys.LControlKey;
                shortcut.Add(c1);

                KeyEntity c2 = new KeyEntity();
                c2.Key = Keys.LControlKey;
                shortcut.Add(c2);

                ShortCutService.Instance.RegisterCommand(shortcut, action);

                this.tb.Text += "注册快捷键: " + shortcut.ToString() + Environment.NewLine;
            }

            {
                // Todo ：双击Ctrl键 
                ShortCutEntity shortcut = new ShortCutEntity();

                KeyEntity c1 = new KeyEntity();
                c1.Key = Keys.LShiftKey;
                shortcut.AddDown(c1);

                KeyEntity c2 = new KeyEntity();
                c2.Key = Keys.S;
                shortcut.Add(c2);

                ShortCutService.Instance.RegisterCommand(shortcut, action);

                this.tb.Text += "注册快捷键: " + shortcut.ToString() + Environment.NewLine;
            }
        }
    }


}
