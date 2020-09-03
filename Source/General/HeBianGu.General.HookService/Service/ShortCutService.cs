
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeBianGu.General.HookService
{

    /// <summary> 系统快捷键服务 </summary>
    public class ShortCutService:IDisposable
    {

        public static ShortCutService Instance = new ShortCutService();

        public ShortCutService()
        {
            HookKeyboardEngine.KeyUp += HookKeyboardEngine_KeyUp;

            HookKeyboardEngine.KeyDown += HookKeyboardEngine_KeyDown;
        }

        private void HookKeyboardEngine_KeyDown(object sender, KeyEventArgs e)
        {
            KeyEntity k = new KeyEntity();
            k.Key = e.KeyCode;
            k.Time = DateTime.Now;
            _current.AddDown(k);
        }

        private void HookKeyboardEngine_KeyUp(object sender, KeyEventArgs e)
        {
            KeyEntity k = new KeyEntity();
            k.Key = e.KeyCode;
            k.Time = DateTime.Now;
            _current.RemoveDown(k);
            _current.Add(k);

            foreach (var item in _collection)
            {
                // Todo ：匹配规则触发任务
                if (_current.Equals(item.Item1))
                {
                    item.Item2.Invoke(item.Item1);
                }
            }
        }

        // Todo ：当前按钮记录 
        ShortCutEntity _current = new ShortCutEntity();

        // Todo ：匹配规则记录 
        List<Tuple<ShortCutEntity, Action<ShortCutEntity>>> _collection = new List<Tuple<ShortCutEntity, Action<ShortCutEntity>>>();

        /// <summary> 注册执行命令 </summary>
        public void RegisterCommand(ShortCutEntity match, Action<ShortCutEntity> action)
        {
            Tuple<ShortCutEntity, Action<ShortCutEntity>> t = new Tuple<ShortCutEntity, Action<ShortCutEntity>>(match, action);
            _collection.Add(t);
        }

        public void Clear()
        {
            _collection.Clear();
        }

        public void Dispose()
        {
            this.Clear();

            HookKeyboardEngine.KeyUp -= HookKeyboardEngine_KeyUp;

            HookKeyboardEngine.KeyDown -= HookKeyboardEngine_KeyDown;
        }
    }





}
