using NKnife.Configuring.UserData;

namespace MeterKnife.Common
{
    public class MeterKnifeUserData : UserApplicationData
    {
        public const string DATA_PATH = "dataPath";

        /// <summary>
        ///     本选项面向的持久化文件
        /// </summary>
        /// <value>The name of the file.</value>
        public override string FileName
        {
            get
            {
                if (string.IsNullOrEmpty(_FileName))
                    _FileName = UserApplicationDataPath + "\\" + GetType().Name + ".UserApplicationData";
                return _FileName;
            }
        }
    }
}