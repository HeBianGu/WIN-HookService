using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeBianGu.General.HookService
{

    /// <summary> 快捷按钮实体 </summary>
    public class ShortCutEntity
    {
        List<KeyEntity> _keys = new List<KeyEntity>();

        /// <summary> 正常按键 </summary>
        internal List<KeyEntity> Keys { get => _keys; set => _keys = value; }

        private List<KeyEntity> _downKeys = new List<KeyEntity>();
        /// <summary> 按下的按键 </summary>
        public List<KeyEntity> DownKeys
        {
            get { return _downKeys; }
            set { _downKeys = value; }
        }

        /// <summary> 容器的大小 </summary>
        int capacity = 3;


        /// <summary> 增加正常按键 </summary>
        public void Add(KeyEntity key)
        {
            Keys.Add(key);

            if (Keys.Count > 3)
            {
                Keys.RemoveAt(0);
            }
        }

        /// <summary> 增加按下的按键 </summary>
        public void AddDown(KeyEntity key)
        {
            if (this.DownKeys.Exists(l => l.Key == key.Key)) return;

            this.DownKeys.Add(key);
        }

        /// <summary> 删除按下的按键 </summary>
        public void RemoveDown(KeyEntity key)
        {
            this.DownKeys.RemoveAll(l => l.Key == key.Key);
        }

        /// <summary> 是否包含指定快捷键 </summary>
        public override bool Equals(object obj)
        {
            if (!(obj is ShortCutEntity)) return false;

            ShortCutEntity s = obj as ShortCutEntity;

            // Todo ：比较按下键 
            if (this.DownKeys.Count != s.DownKeys.Count) return false;

            foreach (var item in s.DownKeys)
            {
                if (!this.DownKeys.Exists(l => l.Key == item.Key)) return false;
            }

            // Todo ：非按下键 
            if (Keys.Count < s.Keys.Count) return false;

            for (int i = 0; i < s.Keys.Count; i++)
            {
                // Todo ：比较按键是否一样 
                if (this.Keys[this.Keys.Count - i - 1].Key != s.Keys[s.Keys.Count - i - 1].Key)
                {
                    return false;
                }

                // Todo ：判断时间间隔 
                if (i == s.Keys.Count - 1) continue;

                if ((this.Keys[this.Keys.Count - i - 1].Time - this.Keys[this.Keys.Count - i - 2].Time).TotalMilliseconds > ShortCutConfiger.SplitMilliseconds)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary> 转换可视化文本 </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in this._downKeys)
            {
                sb.Append(item.Key + ShortCutConfiger.downChar.ToString() + ShortCutConfiger.SptitString.ToString());
            }

            foreach (var item in this.Keys)
            {
                sb.Append(item.Key + ShortCutConfiger.SptitString);
            }

            return sb.ToString().Trim(ShortCutConfiger.SptitChar);
        }


        /// <summary> 根据文本生成规则 </summary>
        public static ShortCutEntity DeSerilse(string str)
        {
            var ss = str.Split(ShortCutConfiger.SptitChar);

            var ds = ss.ToList().FindAll(l => l.EndsWith(ShortCutConfiger.downChar.ToString()));

            var ns = ss.Except(ds);

            ShortCutEntity s = new ShortCutEntity();

            foreach (var item in ds)
            {
                KeyEntity ke = new KeyEntity();
                Keys k = (Keys)Enum.Parse(typeof(Keys), item.Trim(ShortCutConfiger.downChar));
                ke.Key = k;
                s.AddDown(ke);
            }


            foreach (var item in ns)
            {
                KeyEntity ke = new KeyEntity();
                Keys k = (Keys)Enum.Parse(typeof(Keys), item.Trim(ShortCutConfiger.downChar));
                ke.Key = k;
                s.Add(ke);
            }



            return s;
        }
    }
}
