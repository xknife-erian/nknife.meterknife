using System;
using System.Runtime.InteropServices;

namespace NKnife.Mp3
{
    /// <summary>
    ///     一个基本的MP3的播放类
    /// </summary>
    public class Mp3Player
    {
        #region 定义API函数使用的字符串变量 

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)] 
        private string _Name = "";
        [MarshalAs(UnmanagedType.LPTStr, SizeConst = 128)] 
        private string _TemStr = "";
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)] 
        private string _DurLength = "";

        #endregion

        /// <summary>
        ///     播放状态枚举变量
        /// </summary>
        public enum State
        {
            Playing = 1,
            Puase = 2,
            Stop = 3
        };

        protected StructMCI _Mci = new StructMCI();

        /// <summary>
        ///     MP3文件
        /// </summary>
        public string FileName
        {
            get { return _Mci.iName; }
            set
            {
                try
                {
                    _TemStr = "";
                    _TemStr = _TemStr.PadLeft(127, Convert.ToChar(" "));
                    _Name = _Name.PadLeft(260, Convert.ToChar(" "));
                    _Mci.iName = value;
                    APIClass.GetShortPathName(_Mci.iName, _Name, _Name.Length);
                    _Name = GetCurrPath(_Name);
                    _Name = "open " + Convert.ToChar(34) + _Name + Convert.ToChar(34) + " alias media";
                    APIClass.mciSendString("close all", _TemStr, _TemStr.Length, 0);
                    APIClass.mciSendString(_Name, _TemStr, _TemStr.Length, 0);
                    APIClass.mciSendString("set media time format milliseconds", _TemStr, _TemStr.Length, 0);
                    _Mci.state = State.Stop;
                }
                catch
                {
                }
            }
        }

        /// <summary>
        ///     总时间
        /// </summary>
        public int Duration
        {
            get
            {
                _DurLength = "";
                _DurLength = _DurLength.PadLeft(128, Convert.ToChar(" "));
                APIClass.mciSendString("status media length", _DurLength, _DurLength.Length, 0);
                _DurLength = _DurLength.Trim();
                if (_DurLength == "") return 0;
                return (int) (Convert.ToDouble(_DurLength)/1000f);
            }
        }

        /// <summary>
        ///     当前时间
        /// </summary>
        public int CurrentPosition
        {
            get
            {
                _DurLength = "";
                _DurLength = _DurLength.PadLeft(128, Convert.ToChar(" "));
                APIClass.mciSendString("status media position", _DurLength, _DurLength.Length, 0);
                _Mci.iPos = (int) (Convert.ToDouble(_DurLength)/1000f);
                return _Mci.iPos;
            }
        }

        /// <summary>
        ///     播放
        /// </summary>
        public void Play()
        {
            _TemStr = "";
            _TemStr = _TemStr.PadLeft(127, Convert.ToChar(" "));
            APIClass.mciSendString("play media", _TemStr, _TemStr.Length, 0);
            _Mci.state = State.Playing;
        }

        /// <summary>
        ///     停止
        /// </summary>
        public void Stop()
        {
            _TemStr = "";
            _TemStr = _TemStr.PadLeft(128, Convert.ToChar(" "));
            APIClass.mciSendString("close media", _TemStr, 128, 0);
            APIClass.mciSendString("close all", _TemStr, 128, 0);
            _Mci.state = State.Stop;
        }

        public void Puase()
        {
            _TemStr = "";
            _TemStr = _TemStr.PadLeft(128, Convert.ToChar(" "));
            APIClass.mciSendString("pause media", _TemStr, _TemStr.Length, 0);
            _Mci.state = State.Puase;
        }

        private string GetCurrPath(string name)
        {
            if (name.Length < 1) return "";
            name = name.Trim();
            name = name.Substring(0, name.Length - 1);
            return name;
        }

        private class APIClass
        {
            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
            public static extern int GetShortPathName(
                string lpszLongPath,
                string shortFile,
                int cchBuffer
                );

            [DllImport("winmm.dll", EntryPoint = "mciSendString", CharSet = CharSet.Auto)]
            public static extern int mciSendString(
                string lpstrCommand,
                string lpstrReturnString,
                int uReturnLength,
                int hwndCallback
                );
        }

        /// <summary>
        ///     结构变量
        /// </summary>
        public struct StructMCI
        {
            public bool bMut;
            public int iBal;
            public int iDur;
            public string iName;
            public int iPos;
            public int iVol;
            public State state;
        };
    }
}