using System;
using MeterKnife.Util.ShareResources;

namespace NKnife.Wrapper
{
    /// <summary>
    /// 智能日期类型,能够理解"空"日期,能够在日期和空日期之间进行比较。
    /// </summary>
    [Serializable()]
    public struct SmartDate : IComparable
    {
        #region 字段定义
        /// <summary>
        /// 默认格式字符串
        /// </summary>
        private static string _defaultFormat;
        /// <summary>
        /// 格式字符串
        /// </summary>
        private string _format;
        /// <summary>
        /// 日期
        /// </summary>
        private DateTime _date;
        /// <summary>
        /// 空日期的类型
        /// </summary>
        private EmptyValue _emptyValue;
        /// <summary>
        /// 是否已被初始化
        /// </summary>
        private bool _initialized;
        /// <summary>
        /// 最小日期,设置为1900年1月1日
        /// </summary>
        private static DateTime _minDate;

        #endregion

        #region 空日期类型枚举
        /// <summary>
        /// 表示空日期的取值类型，可取最大日期或最小日期。
        /// </summary>
        public enum EmptyValue
        {
            /// <summary>
            /// 将空日期转换为最小日期
            /// </summary>
            MinDate,
            /// <summary>            
            /// 将空日期转换为最大日期
            /// </summary>
            MaxDate
        }
        #endregion

        #region 构造函数

        #region 静态构造函数,为默认格式字符串赋值
        static SmartDate()
        {
            //为默认格式字符串赋值,d表示短日期型，比如1/3/2002
            _defaultFormat = "d";

            //设置最小日期
            _minDate = new DateTime(1900, 1, 1);
        }
        #endregion

        /// <summary>
        /// 创建SmartDate的实例
        /// </summary>
        /// <param name="emptyIsMin">空日期是否转换成最小日期</param>
        public SmartDate(bool emptyIsMin)
        {
            //将空日期的布尔标识转成枚举标识
            _emptyValue = GetEmptyValue(emptyIsMin);

            //设置格式字符串
            _format = null;

            //设置初始化标识
            _initialized = false;

            //将日期最小值赋给日期字段
            _date = _minDate;

            //设置空日期
            SetEmptyDate(_emptyValue);
        }

        /// <summary>
        /// 创建SmartDate的实例
        /// </summary>
        /// <param name="emptyValue">空日期的类型</param>
        public SmartDate(EmptyValue emptyValue)
        {
            //设置空日期的类型
            _emptyValue = emptyValue;

            //设置格式字符串
            _format = null;

            //设置初始化标识
            _initialized = false;

            //将日期最小值赋给日期字段
            _date = _minDate;

            //设置空日期
            SetEmptyDate(_emptyValue);
        }

        /// <summary>
        /// 创建SmartDate的实例
        /// </summary>
        /// <param name="value">要设置的日期</param>
        public SmartDate(DateTime value)
        {
            //设置空日期的类型
            _emptyValue = EmptyValue.MinDate;

            //设置格式字符串
            _format = null;

            //设置初始化标识
            _initialized = false;

            //将日期最小值赋给日期字段
            _date = _minDate;

            //设置日期
            Date = value;
        }

        /// <summary>
        /// 创建SmartDate的实例
        /// </summary>
        /// <param name="value">要设置的日期</param>
        /// <param name="emptyIsMin">空日期是否转换成最小日期</param>
        public SmartDate(DateTime value, bool emptyIsMin)
        {
            //将空日期的布尔标识转成枚举标识
            _emptyValue = GetEmptyValue(emptyIsMin);

            //设置格式字符串
            _format = null;

            //设置初始化标识
            _initialized = false;

            //将日期最小值赋给日期字段
            _date = _minDate;

            //设置日期
            Date = value;
        }

        /// <summary>
        /// 创建SmartDate的实例
        /// </summary>
        /// <param name="value">要设置的日期</param>
        /// <param name="emptyValue">空日期的类型</param>
        public SmartDate(DateTime value, EmptyValue emptyValue)
        {
            //设置空日期的类型
            _emptyValue = emptyValue;

            //设置格式字符串
            _format = null;

            //设置初始化标识
            _initialized = false;

            //将日期最小值赋给日期字段
            _date = _minDate;

            //设置日期
            Date = value;
        }

        /// <summary>
        /// 创建SmartDate的实例
        /// </summary>
        /// <param name="value">要设置的日期</param>
        public SmartDate(string value)
        {
            //设置空日期的类型
            _emptyValue = EmptyValue.MinDate;

            //设置格式字符串
            _format = null;

            //设置初始化标识
            _initialized = true;

            //将日期最小值赋给日期字段
            _date = _minDate;

            //设置日期
            this.Text = value;
        }

        /// <summary>
        /// 创建SmartDate的实例
        /// </summary>
        /// <param name="value">要设置的日期</param>
        /// <param name="emptyIsMin">空日期是否转换成最小日期</param>
        public SmartDate(string value, bool emptyIsMin)
        {
            //将空日期的布尔标识转成枚举标识
            _emptyValue = GetEmptyValue(emptyIsMin);

            //设置格式字符串
            _format = null;

            //设置初始化标识
            _initialized = true;

            //将日期最小值赋给日期字段
            _date = _minDate;

            //设置日期
            this.Text = value;
        }

        /// <summary>
        /// 创建SmartDate的实例
        /// </summary>
        /// <param name="value">要设置的日期</param>
        /// <param name="emptyValue">空日期的类型</param>
        public SmartDate(string value, EmptyValue emptyValue)
        {
            //设置空日期的类型
            _emptyValue = emptyValue;

            //设置格式字符串
            _format = null;

            //设置初始化标识
            _initialized = true;

            //将日期最小值赋给日期字段
            _date = _minDate;

            //设置日期
            this.Text = value;
        }

        #region 将空日期的布尔标识转成枚举标识
        /// <summary>
        /// 将空日期的布尔标识转成枚举标识
        /// </summary>
        /// <param name="emptyIsMin">空日期是否表示最小日期值</param>        
        private static EmptyValue GetEmptyValue(bool emptyIsMin)
        {
            if (emptyIsMin)
            {
                return EmptyValue.MinDate;
            }
            else
            {
                return EmptyValue.MaxDate;
            }
        }
        #endregion

        #region 为空日期赋值
        /// <summary>
        /// 为空日期赋值
        /// </summary>
        /// <param name="emptyValue">空日期的类型</param>
        private void SetEmptyDate(EmptyValue emptyValue)
        {
            if (emptyValue == EmptyValue.MinDate)
            {
                this.Date = _minDate;
            }
            else
            {
                this.Date = DateTime.MaxValue;
            }
        }
        #endregion

        #endregion

        #region 属性

        #region 获取或设置DateTime类型的日期
        /// <summary>
        /// 获取或设置DateTime类型的日期
        /// </summary>
        public DateTime Date
        {
            get
            {
                //如果还未初始化，则先对日期赋值
                if (!this._initialized)
                {
                    //给日期赋个最小值
                    this._date = _minDate;

                    //设置初始化标识
                    this._initialized = true;
                }
                //返回日期
                return this._date;
            }

            set
            {
                //设置日期
                this._date = value;

                //设置初始化标识
                this._initialized = true;
            }
        }
        #endregion

        #region 获取或设置Object类型的日期
        /// <summary>
        /// 获取或设置Object类型的日期，如果为空日期，则返回DBNull类型的空值
        /// </summary>
        public object DBValue
        {
            get
            {
                if (this.IsEmpty)
                {
                    return DBNull.Value;
                }
                else
                {
                    return this.Date;
                }
            }
        }
        #endregion

        #region 获取或设置string类型的日期
        /// <summary>
        /// 获取或设置string类型的日期字符串。
        /// </summary>
        public string Text
        {
            get
            {
                return DateToString(this.Date, FormatString, _emptyValue);
            }
            set
            {
                this.Date = StringToDate(value, _emptyValue);
            }
        }
        #endregion

        #region 是否空日期
        /// <summary>
        /// 是否空日期,是空日期返回true
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                if (_emptyValue == EmptyValue.MinDate)
                {

                    return this.Date.Equals(_minDate);
                }
                else
                {
                    return this.Date.Equals(DateTime.MaxValue);
                }
            }
        }
        #endregion

        #region 空日期是否表示最小日期
        /// <summary>
        /// 空日期是否表示最小日期，如果表示最小日期返回true
        /// </summary>
        public bool EmptyIsMin
        {
            get
            {
                return (_emptyValue == EmptyValue.MinDate);
            }
        }
        #endregion

        #region 格式字符串
        /// <summary>
        /// 格式字符串
        /// </summary>
        public string FormatString
        {
            get
            {
                //如果格式字符串为空，则将默认格式字符串返回
                if (_format == null)
                {
                    _format = _defaultFormat;
                }
                return _format;
            }
            set
            {
                _format = value;
            }
        }
        #endregion

        #region 设置默认格式字符串
        /// <summary>
        /// 设置默认格式字符串,原始默认格式字符串为"d"
        /// </summary>
        /// <param name="formatString">格式字符串</param>
        public static void SetDefaultFormatString(string formatString)
        {
            _defaultFormat = formatString;
        }
        #endregion

        #endregion

        #region 日期类型转换

        #region 将字符串转换为SmartDate类型

        #region Parse方法，将字符串转换为SmartDate类型
        /// <summary>
        /// 将字符串转换为SmartDate类型
        /// </summary>
        /// <param name="date">字符串形式的日期</param>
        public static SmartDate Parse(string date)
        {
            return new SmartDate(date);
        }

        /// <summary>
        /// 将字符串转换为SmartDate类型,可指定空日期的取值类型
        /// </summary>
        /// <param name="date">字符串形式的日期</param>
        /// <param name="emptyDateType">空日期的取值类型</param>        
        public static SmartDate Parse(string date, EmptyValue emptyDateType)
        {
            return new SmartDate(date, emptyDateType);
        }

        /// <summary>
        /// 将字符串转换为SmartDate类型,可指定空日期是否取最小日期
        /// </summary>
        /// <param name="date">字符串形式的日期</param>
        /// <param name="emptyIsMin">空日期是否取最小日期</param>        
        public static SmartDate Parse(string date, bool emptyIsMin)
        {
            return new SmartDate(date, emptyIsMin);
        }
        #endregion

        #region TryParse方法，先判断字符串是否能转换为一个SmartDate日期,并返回转换成功后的日期
        /// <summary>
        /// 判断字符串是否能转换为一个SmartDate日期,并返回转换成功后的日期,
        /// 如果字符串为空，则返回一个最小日期。
        /// </summary>
        /// <param name="value">要转换的日期字符串</param>
        /// <param name="result">返回转换成功后的日期</param>        
        public static bool TryParse(string value, ref SmartDate result)
        {
            return TryParse(value, EmptyValue.MinDate, ref result);
        }

        /// <summary>
        /// 判断字符串是否能转换为一个SmartDate日期,并返回转换成功后的日期
        /// </summary>
        /// <param name="value">要转换的日期字符串</param>
        /// <param name="emptyValue">空日期的取值类型</param>
        /// <param name="result">返回转换成功后的日期</param>        
        public static bool TryParse(string value, EmptyValue emptyValue, ref SmartDate result)
        {
            //定义用于存储日期字符串转换的DateTime日期
            DateTime dateResult = _minDate;

            //将日期字符串转换为DateTime日期
            if (TryStringToDate(value, emptyValue, ref dateResult))
            {
                //如果转换成功，则返回该日期的SmartDate类型
                result = new SmartDate(dateResult, emptyValue);

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 判断字符串是否能转换为一个Datetime日期,并返回转换成功后的日期
        /// </summary>
        /// <param name="value">要转换的日期字符串</param>
        /// <param name="emptyValue">空日期的取值类型</param>
        /// <param name="result">返回转换成功后的日期</param>        
        private static bool TryStringToDate(string value, EmptyValue emptyValue, ref DateTime result)
        {
            //定义一个临时日期
            DateTime temp;

            #region 如果日期字符串为空，则转成最大或最小的日期
            if (string.IsNullOrEmpty(value))
            {
                //如果空日期的取值类型为MinDate,则转成最小日期,否则最大日期
                if (emptyValue == EmptyValue.MinDate)
                {
                    result = _minDate;
                }
                else
                {
                    result = DateTime.MaxValue;
                }

                //返回true
                return true;
            }
            #endregion

            #region 将日期字符串转换为DateTime
            if (DateTime.TryParse(value, out temp))
            {
                //如果转换成功，则返回true
                result = temp;
                return true;
            }
            #endregion

            #region 如果日期字符串是自定义的值，则进行转换
            //将日期字符串转成大写
            string date = value.Trim().ToLower();
            //转换成今天的日期
            if (date == SmartDateString.SmartDateT || date == SmartDateString.SmartDateToday || date == ".")
            {
                result = DateTime.Now;
                return true;
            }
            //转换成昨天的日期
            if (date == SmartDateString.SmartDateY || date == SmartDateString.SmartDateYesterday || date == "-")
            {
                result = DateTime.Now.AddDays(-1);
                return true;
            }
            //转换成明天的日期
            if (date == SmartDateString.SmartDateTom || date == SmartDateString.SmartDateTomorrow || date == "+")
            {
                result = DateTime.Now.AddDays(1);
                return true;
            }
            #endregion

            //转换失败，返回false
            return false;
        }
        #endregion

        #endregion

        #region 将字符串转换为DateTime类型
        /// <summary>
        /// 将字符串转换为DateTime类型日期,如果是空字符串，则转换为最小日期
        /// </summary>
        /// <param name="value">日期字符串</param>
        public static DateTime StringToDate(string value)
        {
            return StringToDate(value, true);
        }

        /// <summary>
        /// 将字符串转换为DateTime类型日期
        /// </summary>
        /// <param name="value">日期字符串</param>
        /// <param name="emptyIsMin">空日期是否取最小日期</param>        
        public static DateTime StringToDate(string value, bool emptyIsMin)
        {
            return StringToDate(value, GetEmptyValue(emptyIsMin));
        }

        /// <summary>
        /// 将字符串转换为DateTime类型日期
        /// </summary>
        /// <param name="value">日期字符串</param>
        /// <param name="emptyValue">空日期的取值类型</param>        
        public static DateTime StringToDate(string value, EmptyValue emptyValue)
        {
            //要返回的的日期
            DateTime result = _minDate;

            //如果string成功转换为DateTime，则返回
            if (TryStringToDate(value, emptyValue, ref result))
            {
                return result;
            }
            else
            {
                //转换失败，抛出异常
                throw new ArgumentException(SmartDateString.StringToDateException);
            }
        }
        #endregion

        #region 将DateTime类型日期转换为指定格式的字符串
        /// <summary>
        /// 将DateTime类型日期转换为指定格式的字符串
        /// </summary>
        /// <param name="value">要转换的日期</param>
        /// <param name="formatString">格式字符串</param>        
        public static string DateToString(DateTime value, string formatString)
        {
            return DateToString(value, formatString, true);
        }

        /// <summary>
        /// 将DateTime类型日期转换为指定格式的字符串
        /// </summary>
        /// <param name="value">要转换的日期</param>
        /// <param name="formatString">格式字符串</param>
        /// <param name="emptyIsMin">空日期是否取最小日期</param>        
        public static string DateToString(DateTime value, string formatString, bool emptyIsMin)
        {
            return DateToString(value, formatString, GetEmptyValue(emptyIsMin));
        }

        /// <summary>
        /// 将DateTime类型日期转换为指定格式的字符串
        /// </summary>
        /// <param name="value">要转换的日期</param>
        /// <param name="formatString">格式字符串</param>
        /// <param name="emptyValue">空日期的取值类型</param>        
        public static string DateToString(DateTime value, string formatString, EmptyValue emptyValue)
        {
            //如果日期为最大日期或最小日期，则转换为空字符串
            if (emptyValue == EmptyValue.MinDate)
            {
                if (value == _minDate)
                {
                    return string.Empty;
                }
            }
            else
            {
                if (value == DateTime.MaxValue)
                {
                    return string.Empty;
                }
            }

            //将日期以指定格式的字符串返回
            return string.Format("{0:" + formatString + "}", value);
        }
        #endregion

        #endregion

        #region 日期运算

        #region 日期相减
        /// <summary>
        /// 从本日期中减去一个指定的日期，返回一个时间间隔
        /// </summary>
        /// <param name="value">减去的日期值</param>        
        public TimeSpan Subtract(DateTime value)
        {
            if (IsEmpty)
            {
                return TimeSpan.Zero;
            }
            else
            {
                return this.Date.Subtract(value);
            }
        }

        /// <summary>
        /// 从本日期中减去一个指定的时间间隔，返回一个日期
        /// </summary>
        /// <param name="value">减去的时间间隔</param>        
        public DateTime Subtract(TimeSpan value)
        {
            if (IsEmpty)
            {
                return this.Date;
            }
            else
            {
                return this.Date.Subtract(value);
            }
        }
        #endregion

        #region 日期相加
        /// <summary>
        /// 将本日期与指定时间间隔相加，返回一个新的日期
        /// </summary>
        /// <param name="value">加上的时间间隔</param>        
        public DateTime Add(TimeSpan value)
        {
            if (IsEmpty)
            {
                return this.Date;
            }
            else
            {
                return this.Date.Add(value);
            }
        }
        #endregion

        #endregion

        #region 日期比较
        /// <summary>
        /// 比较当前日期与指定日期的大小。
        /// 如果相等则返回0;
        /// 如果当前日期大于指定日期，则返回1;
        /// 如果当前日期小于指定日期，则返回-1;
        /// </summary>
        /// <param name="value">要比较的日期</param>
        public int CompareTo(SmartDate value)
        {
            if (this.IsEmpty && value.IsEmpty)
            {
                return 0;
            }
            else
            {
                return _date.CompareTo(value.Date);
            }
        }
        /// <summary>
        /// 比较当前日期与指定日期的大小。
        /// 如果相等则返回0;
        /// 如果当前日期大于指定日期，则返回1;
        /// 如果当前日期小于指定日期，则返回-1;
        /// </summary>
        /// <param name="value">要比较的日期</param>
        public int CompareTo(string value)
        {
            return this.Date.CompareTo(StringToDate(value, _emptyValue));
        }
        /// <summary>
        /// 比较当前日期与指定日期的大小。
        /// 如果相等则返回0;
        /// 如果当前日期大于指定日期，则返回1;
        /// 如果当前日期小于指定日期，则返回-1;
        /// </summary>
        /// <param name="value">要比较的日期</param>
        public int CompareTo(DateTime value)
        {
            return this.Date.CompareTo(value);
        }
        /// <summary>
        /// 比较当前日期与指定日期的大小。
        /// 如果相等则返回0;
        /// 如果当前日期大于指定日期，则返回1;
        /// 如果当前日期小于指定日期，则返回-1;
        /// </summary>
        /// <param name="value">要比较的日期</param>
        int IComparable.CompareTo(object value)
        {
            //该方法只能使用接口的多态进行调用
            if (value is SmartDate)
            {
                return CompareTo((SmartDate)value);
            }
            else
            {
                throw new ArgumentException(SmartDateString.ValueNotSmartDateException);
            }
        }
        #endregion

        #region 重载运算符

        #region 比较两个日期是否相等
        /// <summary>
        /// 比较两个日期是否相等
        /// </summary>
        /// <param name="date1">日期1</param>
        /// <param name="date2">日期2</param>        
        public static bool operator ==(SmartDate date1, SmartDate date2)
        {
            return date1.Equals(date2);
        }

        /// <summary>
        /// 比较两个日期是否相等
        /// </summary>
        /// <param name="date1">日期1</param>
        /// <param name="date2">日期2</param>        
        public static bool operator ==(SmartDate date1, DateTime date2)
        {
            return date1.Equals(date2);
        }

        /// <summary>
        /// 比较两个日期是否相等
        /// </summary>
        /// <param name="date1">日期1</param>
        /// <param name="date2">日期2</param>        
        public static bool operator ==(SmartDate date1, string date2)
        {
            return date1.Equals(date2);
        }

        #endregion

        #region 比较两个日期是否不相等
        /// <summary>
        /// 比较两个日期是否不相等
        /// </summary>
        /// <param name="date1">日期1</param>
        /// <param name="date2">日期2</param>        
        public static bool operator !=(SmartDate date1, SmartDate date2)
        {
            return !date1.Equals(date2);
        }

        /// <summary>
        /// 比较两个日期是否不相等
        /// </summary>
        /// <param name="date1">日期1</param>
        /// <param name="date2">日期2</param>        
        public static bool operator !=(SmartDate date1, DateTime date2)
        {
            return !date1.Equals(date2);
        }

        /// <summary>
        /// 比较两个日期是否不相等
        /// </summary>
        /// <param name="date1">日期1</param>
        /// <param name="date2">日期2</param>        
        public static bool operator !=(SmartDate date1, string date2)
        {
            return !date1.Equals(date2);
        }

        #endregion

        #region 比较日期1是否大于日期2
        /// <summary>
        /// 比较日期1是否大于日期2
        /// </summary>
        /// <param name="date1">日期1</param>
        /// <param name="date2">日期2</param>        
        public static bool operator >(SmartDate date1, SmartDate date2)
        {
            return date1.CompareTo(date2) > 0;
        }

        /// <summary>
        /// 比较日期1是否大于日期2
        /// </summary>
        /// <param name="date1">日期1</param>
        /// <param name="date2">日期2</param>        
        public static bool operator >(SmartDate date1, DateTime date2)
        {
            return date1.CompareTo(date2) > 0;
        }

        /// <summary>
        /// 比较日期1是否大于日期2
        /// </summary>
        /// <param name="date1">日期1</param>
        /// <param name="date2">日期2</param>        
        public static bool operator >(SmartDate date1, string date2)
        {
            return date1.CompareTo(date2) > 0;
        }

        #endregion

        #region 比较日期1是否小于日期2
        /// <summary>
        /// 比较日期1是否小于日期2
        /// </summary>
        /// <param name="date1">日期1</param>
        /// <param name="date2">日期2</param>        
        public static bool operator <(SmartDate date1, SmartDate date2)
        {
            return date1.CompareTo(date2) < 0;
        }

        /// <summary>
        /// 比较日期1是否小于日期2
        /// </summary>
        /// <param name="date1">日期1</param>
        /// <param name="date2">日期2</param>        
        public static bool operator <(SmartDate date1, DateTime date2)
        {
            return date1.CompareTo(date2) < 0;
        }

        /// <summary>
        /// 比较日期1是否小于日期2
        /// </summary>
        /// <param name="date1">日期1</param>
        /// <param name="date2">日期2</param>        
        public static bool operator <(SmartDate date1, string date2)
        {
            return date1.CompareTo(date2) < 0;
        }

        #endregion

        #region 比较日期1是否大于等于日期2
        /// <summary>
        /// 比较日期1是否大于等于日期2
        /// </summary>
        /// <param name="date1">日期1</param>
        /// <param name="date2">日期2</param>        
        public static bool operator >=(SmartDate date1, SmartDate date2)
        {
            return date1.CompareTo(date2) >= 0;
        }

        /// <summary>
        /// 比较日期1是否大于等于日期2
        /// </summary>
        /// <param name="date1">日期1</param>
        /// <param name="date2">日期2</param>        
        public static bool operator >=(SmartDate date1, DateTime date2)
        {
            return date1.CompareTo(date2) >= 0;
        }

        /// <summary>
        /// 比较日期1是否大于等于日期2
        /// </summary>
        /// <param name="date1">日期1</param>
        /// <param name="date2">日期2</param>        
        public static bool operator >=(SmartDate date1, string date2)
        {
            return date1.CompareTo(date2) >= 0;
        }

        #endregion

        #region 比较日期1是否小于等于日期2
        /// <summary>
        /// 比较日期1是否小于等于日期2
        /// </summary>
        /// <param name="date1">日期1</param>
        /// <param name="date2">日期2</param>        
        public static bool operator <=(SmartDate date1, SmartDate date2)
        {
            return date1.CompareTo(date2) <= 0;
        }

        /// <summary>
        /// 比较日期1是否小于等于日期2
        /// </summary>
        /// <param name="date1">日期1</param>
        /// <param name="date2">日期2</param>        
        public static bool operator <=(SmartDate date1, DateTime date2)
        {
            return date1.CompareTo(date2) <= 0;
        }

        /// <summary>
        /// 比较日期1是否小于等于日期2
        /// </summary>
        /// <param name="date1">日期1</param>
        /// <param name="date2">日期2</param>        
        public static bool operator <=(SmartDate date1, string date2)
        {
            return date1.CompareTo(date2) <= 0;
        }

        #endregion

        #region 日期相加
        /// <summary>
        /// 原始日期加上一个时间间隔,返回一个新日期
        /// </summary>
        /// <param name="startTime">原始日期</param>
        /// <param name="span">时间间隔</param>        
        public static SmartDate operator +(SmartDate startTime, TimeSpan span)
        {
            return new SmartDate(startTime.Add(span), startTime.EmptyIsMin);
        }

        #endregion

        #region 日期相减
        /// <summary>
        /// 原始日期减去一个时间间隔,返回一个新日期
        /// </summary>
        /// <param name="startTime">原始日期</param>
        /// <param name="span">时间间隔</param>        
        public static SmartDate operator -(SmartDate startTime, TimeSpan span)
        {
            return new SmartDate(startTime.Subtract(span), startTime.EmptyIsMin);
        }

        /// <summary>
        /// 原始日期减去一个结束日期,返回一个时间间隔
        /// </summary>
        /// <param name="startTime">原始日期</param>
        /// <param name="finishTime">结束日期</param>        
        public static TimeSpan operator -(SmartDate startTime, SmartDate finishTime)
        {
            return startTime.Subtract(finishTime.Date);
        }

        /// <summary>
        /// 原始日期减去一个结束日期,返回一个时间间隔
        /// </summary>
        /// <param name="startTime">原始日期</param>
        /// <param name="finishTime">结束日期</param>        
        public static TimeSpan operator -(SmartDate startTime, DateTime finishTime)
        {
            return startTime.Subtract(finishTime);
        }

        /// <summary>
        /// 原始日期减去一个结束日期,返回一个时间间隔
        /// </summary>
        /// <param name="startTime">原始日期</param>
        /// <param name="finishTime">结束日期</param>        
        public static TimeSpan operator -(DateTime startTime, SmartDate finishTime)
        {
            //将原始日期转成SmartDate类型
            SmartDate time = new SmartDate(startTime);

            return time.Subtract(finishTime.Date);
        }

        #endregion

        #endregion

        #region 重写GetHashCode方法
        /// <summary>
        /// 获取哈希代码
        /// </summary>        
        public override int GetHashCode()
        {
            return this.Date.GetHashCode();
        }
        #endregion

        #region 重写Equals方法
        /// <summary>
        /// 将SmartDate实例与SmartDate实例、DateTime实例、string实例进行相等比较,
        /// 相等返回true.
        /// </summary>
        /// <param name="obj">要比较的对象</param>        
        public override bool Equals(object obj)
        {
            //如果是与SmartDate实例比较
            if (obj is SmartDate)
            {
                //将要比较的对象转换为SmartDate
                SmartDate temp = (SmartDate)obj;

                if (this.IsEmpty && temp.IsEmpty)
                {
                    //如果两个实例都为空，返回true
                    return true;
                }
                else
                {
                    //比较两个实例的DateTime值
                    return this.Date.Equals(temp.Date);
                }
            }

            //如果是与DateTime实例进行比较
            if (obj is DateTime)
            {
                return this.Date.Equals((DateTime)obj);
            }

            //如果是与字符串进行比较
            if (obj is string)
            {
                return (this.CompareTo(obj.ToString()) == 0);
            }

            return false;
        }
        #endregion

        #region 重写ToString方法
        /// <summary>
        /// 获取日期字符串
        /// </summary>        
        public override string ToString()
        {
            return this.Text;
        }

        /// <summary>
        ///  获取指定格式字符串的日期字符串
        /// </summary>
        /// <param name="format">格式字符串</param>        
        public string ToString(string format)
        {
            return DateToString(this.Date, format, _emptyValue);
        }
        #endregion
    }
}
