using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;
using MeterKnife.Util.ShareResources;
using NKnife.Utility;
using NKnife.Wrapper;

namespace NKnife.Converts
{
    /// <summary>
    ///     定义一些基础的转换方法(对系统方法的一些扩展)
    ///     Defines some basic conversion routines.
    /// </summary>
    public static class UtilityConvert
    {
        #region ConvertMode enum

        /// <summary>
        ///     转换时的模式，一般指是严格转换还是宽松的转换
        /// </summary>
        public enum ConvertMode
        {
            /// <summary>
            ///     严格的
            /// </summary>
            Strict,

            /// <summary>
            ///     宽松的
            /// </summary>
            Relaxed
        }

        #endregion

        public static T EnumParse<T>(object obj, T defaultEnum) where T : struct
        {
            if (obj != null)
                return EnumParse(obj.ToString(), defaultEnum);
            return defaultEnum;
        }

        public static T EnumParse<T>(string value, T defaultEnum) where T : struct
        {
            T rtn;
            if (Enum.TryParse(value, out rtn))
                return rtn;
            return defaultEnum;
        }

        public static Guid GuidParse(object obj)
        {
            if (obj is DBNull || obj == null)
            {
                return Guid.Empty;
            }
            return GuidParse(obj.ToString());
        }

        /// <summary>
        ///     解析一个可能是Guid的字符串
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private static Guid GuidParse(string value)
        {
            Guid n;
            if (!Guid.TryParse(value, out n))
                return Guid.Empty;
            return n;
        }

        /// <summary>
        ///     解析一个可能是数字的字符串
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="whenParseFail">当解析失败时的返回值</param>
        /// <returns></returns>
        public static int Int32Parse(object obj, int whenParseFail = 0)
        {
            if (obj is DBNull)
            {
                return whenParseFail;
            }
            if (obj == null)
            {
                return whenParseFail;
            }
            return Int32Parse(obj.ToString(), whenParseFail);
        }

        /// <summary>
        ///     解析一个可能是数字的字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="whenParseFail">当解析失败时的返回值</param>
        /// <returns></returns>
        private static int Int32Parse(string value, int whenParseFail)
        {
            int n;
            if (!Int32.TryParse(value, out n))
                return whenParseFail;
            return n;
        }

        /// <summary>
        ///     解析一个可能是数字的字符串
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="whenParseFail">当解析失败时的返回值</param>
        /// <returns></returns>
        public static short Int16Parse(object obj, short whenParseFail = 0)
        {
            if (obj is DBNull || obj == null)
            {
                return whenParseFail;
            }
            return Int16Parse(obj.ToString(), whenParseFail);
        }

        /// <summary>
        ///     解析一个可能是数字的字符串
        /// </summary>
        /// <param name="value"></param>
        /// <param name="whenParseFail">当解析失败时的返回值</param>
        /// <returns></returns>
        private static short Int16Parse(string value, short whenParseFail)
        {
            short n;
            if (!Int16.TryParse(value, out n))
                return whenParseFail;
            return n;
        }

        /// <summary>
        ///     考虑得比较全面的字符串向Bool值的解析方法(如果是Int值，大于0均为True)
        /// </summary>
        public static bool BoolParse(object obj)
        {
            if (obj is DBNull)
            {
                return false;
            }
            if (obj == null)
            {
                return false;
            }
            return BoolParse(obj.ToString());
        }

        /// <summary>
        ///     考虑得比较全面的字符串向Bool值的解析方法(如果是Int值，大于0均为True)
        /// </summary>
        /// <param name="v">The v.</param>
        /// <returns></returns>
        public static bool BoolParse(string v)
        {
            if (string.IsNullOrWhiteSpace(v))
                return false;
            if (v.ToLower().Equals("true"))
                return true;
            var i = 0;
            int.TryParse(v, out i);
            return IntToBoolean(i);
        }

        #region FromString ToString

        /// <summary>
        ///     转换指定的字符串为指定的类型，如转换不成功，将返回指定的类型的默认值
        ///     <param name="v">指定的字符串</param>
        ///     <param name="defaultValue">指定的类型的默认值</param>
        /// </summary>
        public static T FromString<T>(string v, T defaultValue)
        {
            if (string.IsNullOrEmpty(v))
                return defaultValue;
            if (typeof (T) == typeof (string))
                return (T) (object) v;
            try
            {
                var c = TypeDescriptor.GetConverter(typeof (T));
                return (T) c.ConvertFromInvariantString(v);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        /// <summary>
        ///     转换指定的类型为字符串，如转换不成功，将返回Null
        /// </summary>
        public static string ToString<T>(T val)
        {
            if (typeof (T) == typeof (string))
            {
                var s = (string) (object) val;
                return string.IsNullOrEmpty(s) ? null : s;
            }
            try
            {
                var c = TypeDescriptor.GetConverter(typeof (T));
                var s = c.ConvertToInvariantString(val);
                return string.IsNullOrEmpty(s) ? null : s;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        #region ConvertTo 将数据转换为指定类型

        #region 重载一

        /// <summary>
        ///     将数据转换为指定类型，一般用在实现了IConvertible接口的类型
        /// </summary>
        /// <param name="data">转换的数据</param>
        /// <param name="targetType">转换的目标类型</param>
        public static object ConvertTo(object data, Type targetType)
        {
            //如果数据为空，则返回
            if (Checker.IsNullOrEmpty(data))
            {
                return null;
            }

            try
            {
                //如果数据实现了IConvertible接口，则转换类型
                if (data is IConvertible)
                {
                    return Convert.ChangeType(data, targetType);
                }
                return data;
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region 重载二

        /// <summary>
        ///     将数据转换为指定类型，一般用在实现了IConvertible接口的类型
        /// </summary>
        /// <typeparam name="T">转换的目标类型</typeparam>
        /// <param name="data">转换的数据</param>
        public static T ConvertTo<T>(object data)
        {
            //如果数据为空，则返回
            if (Checker.IsNullOrEmpty(data))
            {
                return default(T);
            }

            try
            {
                //如果数据是T类型，则直接转换
                if (data is T)
                {
                    return (T) data;
                }

                //如果目标类型是枚举
                if (typeof (T).BaseType == typeof (Enum))
                {
                    return UtilityEnums.GetInstance<T>(data);
                }

                //如果数据实现了IConvertible接口，则转换类型
                if (data is IConvertible)
                {
                    return (T) Convert.ChangeType(data, typeof (T));
                }
                return default(T);
            }
            catch
            {
                return default(T);
            }
        }

        #endregion

        #endregion

        /// <summary>
        ///     填充数据表时将为Null的对象转换为DBNull，如果不是，原样返回原值
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns></returns>
        // ReSharper disable once InconsistentNaming
        public static object NullToDBNull(object obj)
        {
            object value = DBNull.Value;
            if (obj != null)
                value = obj;
            return value;
        }

        #region 将整型变量转化为布尔变量(True或False).

        /// <summary>
        ///     将整型变量转化为布尔变量(True或False).
        ///     规则：如果整型数值大于0,返回True,否则返回False.（非严格模式）
        /// </summary>
        /// <param name="intParam">整型数</param>
        /// <returns>如果整型数值大于0,返回True,否则返回False.</returns>
        public static bool IntToBoolean(int intParam)
        {
            return intParam > 0;
        }

        /// <summary>
        ///     将整型变量转化为布尔变量(True或False).
        ///     规则：如果整型数值大于0,返回True,否则返回False.
        /// </summary>
        /// <param name="intParam">The int param.</param>
        /// <param name="mode">严格模式：只能转换0或1；宽松模式：大于0,返回True,否则返回False.</param>
        /// <returns></returns>
        public static bool IntToBoolean(int intParam, ConvertMode mode)
        {
            switch (mode)
            {
                case ConvertMode.Strict:
                {
                    switch (intParam)
                    {
                        case 0:
                            return false;
                        case 1:
                            return true;
                    }
                    throw new ArgumentOutOfRangeException(string.Format(ArgumentValidationString.ValueMustIs0or1, "intParam"));
                }
                case ConvertMode.Relaxed:
                    return IntToBoolean(intParam);
                default:
                    Debug.Fail(mode.ToString());
                    return false;
            }
        }

        #endregion

        #region 将char转化为布尔变量(True或False).

        /// <summary>
        ///     将char转化为布尔变量(True或False).
        /// </summary>
        /// <param name="charParam">char值</param>
        /// <returns>如果char是0,返回False；如果char是1,返回True</returns>
        public static bool CharToBoolean(char charParam)
        {
            return CharToBoolean(charParam, ConvertMode.Relaxed);
        }

        /// <summary>
        ///     将char转化为布尔变量(True或False).
        /// </summary>
        /// <param name="charParam">char值</param>
        /// <param name="mode">选择是否严格转换模式，当宽松模式下，非0或1的char都将返回false</param>
        /// <returns>如果char是0,返回False；如果char是1,返回True</returns>
        public static bool CharToBoolean(char charParam, ConvertMode mode)
        {
            switch (charParam)
            {
                case '1':
                    return true;
                case '0':
                    return false;
                default:
                    if (mode == ConvertMode.Relaxed)
                    {
                        return false;
                    }
                    throw new ArgumentException(charParam + " , 参数应严格是1或0.");
            }
        }

        #endregion

        #region 各进制数间转换

        /// <summary>
        ///     实现各进制数间的转换。如：ConvertBase("15", 10, 16)表示将10进制数15转换为16进制的数。
        /// </summary>
        /// <param name="from">原值的进制,只能是2,8,10,16四个值。</param>
        /// <param name="value">要转换的值,即原值</param>
        /// <param name="to">要转换到的目标进制，只能是2,8,10,16四个值。</param>
        public static string ConvertBase(int from, string value, int to)
        {
            // 检查参数
            if (from != 2 && from != 8 && from != 10 && from != 16)
            {
                throw new ArgumentOutOfRangeException("from");
            }
            if (to != 2 && to != 8 && to != 10 && to != 16)
            {
                throw new ArgumentOutOfRangeException("to");
            }

            //将要转换的原值尝试转换成一个Int值
            int num;
            if (!int.TryParse(value, out num))
            {
                throw new ArgumentNullException("value");
            }

            var intValue = Convert.ToInt32(value, from); //先转成10进制
            var result = Convert.ToString(intValue, to); //再转成目标进制

            //if (to == 2)
            //{
            //    StringBuilder sb = new StringBuilder(8);
            //    switch (result.Length)
            //    {
            //        case 8:
            //            sb.Append(result);
            //            break;
            //        case 7:
            //            sb.Append("0").Append(result);
            //            break;
            //        case 6:
            //            sb.Append("00").Append(result);
            //            break;
            //        case 5:
            //            sb.Append("000").Append(result);
            //            break;
            //        case 4:
            //            sb.Append("0000").Append(result);
            //            break;
            //        case 3:
            //            sb.Append("00000").Append(result);
            //            break;
            //    }
            //    return sb.ToString();
            //}
            return result;
        }

        #endregion

        #region byte[]的相关转换

        /// <summary>
        ///     将字节数组转化为数值
        /// </summary>
        /// <param name="arrByte"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static int BytesToInt(byte[] arrByte, int offset)
        {
            return BitConverter.ToInt32(arrByte, offset);
        }

        /// <summary>
        ///     将数值转化为字节数组
        /// </summary>
        /// <param name="value"></param>
        /// <param name="reverse">是否需要把得到的字节数组反转，因为Windows操作系统中整形的高低位是反转转置后保存的。</param>
        /// <returns></returns>
        public static byte[] IntToBytes(int value, bool reverse)
        {
            var ret = BitConverter.GetBytes(value);
            if (reverse)
                Array.Reverse(ret);
            return ret;
        }

        /// <summary>
        ///     将字节数组转化为16进制字符串
        /// </summary>
        /// <param name="arrByte"></param>
        /// <param name="reverse">是否需要把得到的字节数组反转，因为Windows操作系统中整形的高低位是反转转置之后保存的。</param>
        /// <returns></returns>
        public static string BytesToHex(byte[] arrByte, bool reverse)
        {
            var sb = new StringBuilder();
            if (reverse)
                Array.Reverse(arrByte);
            foreach (var b in arrByte)
                sb.AppendFormat("{0:x2}", b);
            return sb.ToString();
        }

        /// <summary>
        ///     将16进制字符串转化为字节数组
        /// </summary>
        /// <param name="hexSrc"></param>
        /// <returns></returns>
        public static byte[] HexToBytes(string hexSrc)
        {
            var hex = hexSrc.Replace(" ", "");
            var byteArray = new byte[hex.Length / 2];
            for (int i = 0, j = 0; i < hex.Length; i = i + 2, j++)
            {
                try
                {
                    byteArray[j] = Convert.ToByte(hex.Substring(i, 2), 16);
                }
                catch (Exception)
                {
                    continue;
                }
            }
            return byteArray;
        }

        #endregion

        #region 使用指定字符集将string转换成byte[]

        /// <summary>
        ///     将string转换成byte[]
        /// </summary>
        /// <param name="text">要转换的字符串</param>
        public static byte[] StringToBytes(string text)
        {
            return Encoding.Default.GetBytes(text);
        }

        /// <summary>
        ///     使用指定字符集将string转换成byte[]
        /// </summary>
        /// <param name="text">要转换的字符串</param>
        /// <param name="encoding">字符编码</param>
        public static byte[] StringToBytes(string text, Encoding encoding)
        {
            return encoding.GetBytes(text);
        }

        #endregion

        #region 使用指定字符集将byte[]转换成string

        /// <summary>
        ///     将byte[]转换成string
        /// </summary>
        /// <param name="bytes">要转换的字节数组</param>
        public static string BytesToString(byte[] bytes)
        {
            return Encoding.Default.GetString(bytes);
        }

        /// <summary>
        ///     使用指定字符集将byte[]转换成string
        /// </summary>
        /// <param name="bytes">要转换的字节数组</param>
        /// <param name="encoding">字符编码</param>
        public static string BytesToString(byte[] bytes, Encoding encoding)
        {
            if (encoding == Encoding.UTF8)
            {
                if (bytes[0] == 239 && bytes[1] == 187 && bytes[2] == 191)
                {
                    return encoding.GetString(bytes, 3, bytes.Length - 3);
                }
            }
            return encoding.GetString(bytes);
        }

        #endregion

        #region 将流转换成字符串

        /// <summary>
        ///     将流转换成字符串,同时关闭该流
        /// </summary>
        /// <param name="stream">流</param>
        /// <param name="encoding">字符编码</param>
        public static string StreamToString(Stream stream, Encoding encoding)
        {
            //获取的文本
            string streamText;

            //读取流
            try
            {
                using (var reader = new StreamReader(stream, encoding))
                {
                    streamText = reader.ReadToEnd();
                }
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                stream.Close();
            }

            //返回文本
            return streamText;
        }

        /// <summary>
        ///     将流转换成字符串,同时关闭该流
        /// </summary>
        /// <param name="stream">流</param>
        public static string StreamToString(Stream stream)
        {
            return StreamToString(stream, Encoding.Default);
        }

        #endregion

        #region Image和base64之间的转换

        public static string ImageToBase64(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new ArgumentException(filePath + " is NOT exist.");
            }

            var ext = Path.GetExtension(filePath).ToLower();
            //jpg格式，则直接读内存。否则先读成Image，再转成jpg格式
            if (ext != ".jpg" && ext != ".jpeg")
            {
                try
                {
                    var image = Image.FromFile(filePath);
                    return ImageToBase64(image);
                }
                catch
                {
                    throw new OutOfMemoryException(filePath + " -- File is TOO LARGE!");
                }
            }
            var bytes = File.ReadAllBytes(filePath);
            return Convert.ToBase64String(bytes);
        }

        public static string ImageToBase64(Image image)
        {
            var memory = new MemoryStream();
            image.Save(memory, ImageFormat.Jpeg);
            var bytes = memory.ToArray();
            return Convert.ToBase64String(bytes);
        }

        public static Image Base64ToImage(string base64String)
        {
            var bytes = Convert.FromBase64String(base64String);
            var memory = new MemoryStream(bytes);
            try
            {
                if (memory.Length == 0)
                {
                    return null;
                }
                return Image.FromStream(memory);
            }
            finally
            {
                memory.Close();
            }
        }

        public static void Base64ToImage(string base64String, string filePath)
        {
            var bytes = Convert.FromBase64String(base64String);
            File.WriteAllBytes(filePath, bytes);
        }

        #endregion

        #region object和base64之间的转换

        public static string FileToBase64(string filePath)
        {
            var bytes = File.ReadAllBytes(filePath);
            return Convert.ToBase64String(bytes);
        }

        public static Icon Base64ToIcon(string base64String)
        {
            var bytes = Convert.FromBase64String(base64String);
            var memory = new MemoryStream(bytes);
            try
            {
                if (memory.Length == 0)
                {
                    return null;
                }
                return new Icon(memory);
            }
            finally
            {
                memory.Close();
            }
        }

        public static Cursor Base64ToCursor(string base64String)
        {
            var bytes = Convert.FromBase64String(base64String);
            var memory = new MemoryStream(bytes);
            try
            {
                if (memory.Length == 0)
                {
                    return null;
                }
                return new Cursor(memory);
            }
            finally
            {
                memory.Close();
            }
        }

        public static byte[] Base64ToByteArray(string base64String)
        {
            return Convert.FromBase64String(base64String);
        }

        #endregion
    }
}