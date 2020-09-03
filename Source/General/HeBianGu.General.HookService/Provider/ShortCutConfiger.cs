using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeBianGu.General.HookService
{
    /// <summary> 配置信息 </summary>
    class ShortCutConfiger
    {
        /// <summary> 间隔字符 </summary>
        public const char SptitChar = '+';

        /// <summary> 间隔字符串 </summary>
        public const string SptitString = "+";

        /// <summary> 触发范围 </summary>
        public const int SplitMilliseconds = 500;

        /// <summary> 按下状态 </summary>
        public const char downChar = '↓';

        private static List<Keys> _alts = new List<Keys>() { Keys.LMenu, Keys.RMenu, Keys.Alt };
        /// <summary> 说明 </summary>
        public List<Keys> Alts
        {
            get { return _alts; }
        }

        private static List<Keys> _shifts = new List<Keys>() { Keys.LShiftKey, Keys.RShiftKey, Keys.Shift, Keys.ShiftKey };
        /// <summary> 说明 </summary>
        public static List<Keys> Shifts
        {
            get { return _shifts; }
        }

        private static List<Keys> _ctrls = new List<Keys>() { Keys.LControlKey, Keys.RControlKey, Keys.Control, Keys.ControlKey };
        /// <summary> 说明 </summary>
        public static List<Keys> Ctrls
        {
            get { return _ctrls; }
        }

        /// <summary> 是否匹配系统按键 并且已经按下系统按键 </summary>
        public static bool IsMatchSystem(Keys k, KeyEventArgs key)
        {
            if (_alts.Contains(k))
            {
                if (!key.Alt) return false;
            }
            else if (_shifts.Contains(k))
            {
                if (!key.Shift) return false;
            }
            else if (_ctrls.Contains(k))
            {
                if (!key.Control) return false;
            }

            return true;
        }
    }
}
