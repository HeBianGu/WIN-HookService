using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeBianGu.General.HookService
{
    /// <summary> 键盘按键 </summary>
    public class KeyEntity
    {
        private Keys _key;
        /// <summary> 按下的键 </summary>
        public Keys Key
        {
            get { return _key; }
            set { _key = value; }
        }

        private DateTime _time;
        /// <summary> 按下的时间 </summary>
        public DateTime Time
        {
            get { return _time; }
            set { _time = value; }
        }
    }
}
