using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace NKnife.GUI.WinForm.AutoLayoutPanel
{
    public partial class AutoLayoutPanel : FlowLayoutPanel
    {
        #region 属性定义

        /// <summary>
        /// 是否实时保存（未完成）by lisuye on 2008年5月28日
        /// </summary>
        public bool RealTimeSave { get; set; }
        /// <summary>
        /// 是否针对单个对象
        /// </summary>
        public bool SingleObject { get; set; }
        /// <summary>
        /// 是否针对静态属性
        /// </summary>
        private bool IsStatic { get; set; }
        /// <summary>
        /// 本控件所依据:类型与该类型中实现的接口集合
        /// </summary>
        private TypeAndInterfaceArr OwnTypeAndInterfaceArr { get; set; }
        internal static string ResourcesPath { get; private set; }
        private Type _autoAttributeType;

        //做一个标识如果是第一次加载数据，不触发Changed事件内容
        public bool IsNewOpenForAddEvent{get;set;}
        /// <summary>
        /// 所有ValueControl的集合
        /// </summary>
        internal List<ValueControl> ValueControls = new List<ValueControl>();

        const int WM_CTLCOLOREDIT = 0x0133;
        const int WM_NCPAINT = 0x085;
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_NCPAINT)
            {
                this.AutoScroll = true;
            }
            base.WndProc(ref m);

        }
        /// <summary>
        ///判断 ValueControls内容是否有修改 by lisuye on 2008年5月28日
        /// </summary>
        public bool IsModified
        {
            get { return ValueControls.Any(item => item.IsModified); }
        }

        #endregion

        static public void Initialize(string resourcesPath)
        {
            ResourcesPath = resourcesPath;
        }

        #region 构造函数 简单的一些事件 控件初始化

        /// <summary>
        /// 构造函数: 
        /// 根据Type构建控件本身及子控件
        /// </summary>
        protected AutoLayoutPanel(Type attributeType, TypeAndInterfaceArr typeAndInterface, bool isStatic, bool singleObject)
        {
            this.IsNewOpenForAddEvent = true;
            OwnTypeAndInterfaceArr = typeAndInterface;
            IsStatic = isStatic;
            this._autoAttributeType = attributeType;
            this.SingleObject = singleObject;

            //this.Padding = new Padding(10);//可进行设置FlowLayoutPanel的边距来改变所有控件的起始位置的不同,以消除控件太靠近边缘的美观
           
            CreateControlForPanel(OwnTypeAndInterfaceArr);
        }

        /// <summary>
        /// 在OnCreateControl事件里完成初始化
        /// （在构造函数里无法获得宽度与高度）
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

        }

        /// <summary>
        /// 由于FlowLayoutPanel的特殊性，无法完成其子控件的Dock效果，
        /// 故在Resize事件里完成子控件的宽度设置
        /// </summary>
        protected override void OnResize(EventArgs eventargs)
        {
            base.OnResize(eventargs);
            //在Resize的时候将DockTop的GroupBoxEx的宽度进行控制
            foreach (Control ctr in this.Controls)
            {
                if (ctr.Dock == DockStyle.Top)
                {
                    ctr.Width = this.Width - 6;
                    ResizeControlSubMethod(ctr, ctr.Width - 2);
                }
            }
        }
        /// <summary>
        /// Resize事件的的子方法
        /// </summary>
        private void ResizeControlSubMethod(Control ctr, int width)
        {
            foreach (Control subCtr in ctr.Controls)
            {
                if (subCtr is GroupBox)
                {
                    subCtr.Width = ctr.Width;
                }
            }
        }

        /// <summary>
        /// 初始化控件
        /// </summary>
        private void CreateControlForPanel(TypeAndInterfaceArr typeAndInterface)
        {
            #region 取得一个名字赋给This
            StringBuilder nameSb;
            {
                nameSb = new StringBuilder();
                nameSb.Append("Control_").Append(typeAndInterface.ClassType).Append("_");
                if (typeAndInterface.Interfaces != null)
                {
                    foreach (Type ty in typeAndInterface.Interfaces)
                    {
                        nameSb.Append(ty.Name).Append("_");
                    }
                }
            }
            this.Name = nameSb.ToString();
            #endregion

            //根据Type来生成控件
            this.BuildControlForType(typeAndInterface);

            #region 根据接口来生成控件

            //todo:根据接口来生成控件未完成
            Type[] listInterface = typeAndInterface.Interfaces;

            #endregion

            this.AutoScroll = true;
            this.Dock = DockStyle.Fill;
        }

        #endregion

        #region 一些虚方法

        protected virtual GroupBoxEx CreateGroup(GroupAttsData data)
        {
            return new GroupBoxEx(data,this);
        }

        protected internal virtual ValueControl CreateValueControl(AutoAttributeData data,GroupBoxEx group)
        {
            return new ValueControl(data, group);
        }

        /// <summary>
        /// 从资源文件里获取当前语言字符串
        /// </summary>
        static public string GetLanguageText(AutoLayoutPanelXmlDocument doc, string str)
        {
            string currLangText;
            if (!doc.TextDic.TryGetValue(str, out currLangText))
            {
                ///资源文件默认在:Debug\CHS\AutoLayoutPanelResource.xml里
                Debug.Fail("此文本没有对应的资源文本:" + str);
                return str;
            }
            return currLangText;
        }
        /// <summary>
        /// 从资源文件里获取当前语言字符串
        /// </summary>
        static public string GetLanguageText(string strKey)
        {
            return GetLanguageText(AutoLayoutPanelXmlDocument.Singler, strKey);
        }

        /// <summary>
        /// 将AutoAttributeData的集合整理成GroupAttsData的集合 by lisuye on 2008年5月28日
        /// </summary>
        protected virtual SortedDictionary<int, GroupAttsData> ToGroupDatas(List<AutoAttributeData> objectKeyList)
        {
            SortedDictionary<int, GroupAttsData> dicGroupAttsData = new SortedDictionary<int, GroupAttsData>();

            foreach (var item in objectKeyList)
            {
                int groupBoxIndex = item.Attribute.GroupBoxIndex;
                GroupAttsData groupData;

                ///找此groupBoxIndex对应的Data是否在dic里已存在
                if (!dicGroupAttsData.TryGetValue(groupBoxIndex, out groupData))
                {
                    ///没有找到，则构造并添加
                    groupData = new GroupAttsData(groupBoxIndex);
                    dicGroupAttsData.Add(groupBoxIndex, groupData);
                }

                groupData.AutoAttributeDatas.Add(item);
            }

            return dicGroupAttsData;
        }

        #endregion


        #region 重点：根据Type来生成控件
        /// <summary>
        /// 根据Type来生成控件
        /// </summary>
        private void BuildControlForType(TypeAndInterfaceArr typeAndInterface)
        {
            Type type = typeAndInterface.ClassType;

            //PropertyInfo[] properties = null;

            MemberInfo[] memberInfos = null;

            #region GetProperties
            if (IsStatic)
            {
                memberInfos = type.GetMembers(
                    BindingFlags.Static |
                    BindingFlags.Public |
                    BindingFlags.GetProperty |
                    BindingFlags.SetProperty |
                    BindingFlags.InvokeMethod);
                //properties = type.GetProperties(
                //     BindingFlags.Static |
                //     BindingFlags.Public |
                //     BindingFlags.GetProperty |
                //     BindingFlags.SetProperty);
            }
            else
            {
                memberInfos = type.GetMembers(
                    BindingFlags.Instance |
                    BindingFlags.Public |
                    BindingFlags.GetProperty |
                    BindingFlags.SetProperty |
                    BindingFlags.InvokeMethod);
                /////取当前类的属性
                //properties = type.GetProperties(
                //     BindingFlags.Instance |
                //     BindingFlags.Public |
                //     BindingFlags.GetProperty |
                //     BindingFlags.SetProperty);
                /////取当前类的属性
                //properties = type.GetProperties(
                //     BindingFlags.Instance |
                //     BindingFlags.Public |
                //     BindingFlags.GetProperty |
                //     BindingFlags.SetProperty |
                //     BindingFlags.DeclaredOnly);

                /////取派生类的属性
                //PropertyInfo[] baseProperties = type.BaseType.GetProperties(
                //     BindingFlags.Instance |
                //     BindingFlags.Public |
                //     BindingFlags.GetProperty |
                //     BindingFlags.SetProperty |
                //     BindingFlags.DeclaredOnly);

                /////将dedaoshu得到的属性合成一个数组
                //PropertyInfo[] tempNew = new PropertyInfo[properties.Length + baseProperties.Length];
                //Array.Copy(properties, tempNew, properties.Length);
                //Array.Copy(baseProperties, 0, tempNew, properties.Length, baseProperties.Length);
                //properties = tempNew;
            }
            #endregion

            List<AutoAttributeData> attList = new List<AutoAttributeData>();
            //利用反射得到属性中的定制特性 by lisuye on 2008年5月29日

            //foreach类型里的所有属性是否有定制特性
            foreach (MemberInfo info in memberInfos)
            {
                object[] propertyPadValues = info.GetCustomAttributes(_autoAttributeType, true);
                if (propertyPadValues.Length <= 0)
                {
                    continue;//无定制特性
                }

                //foreach单个属性里的定制特性,Add到attList中
                foreach (AutoLayoutPanelAttribute att in propertyPadValues)
                {
                    attList.Add(new AutoAttributeData(info,att));
                }
            }
            //根据定制特性绘制groupbox控件 by lisuye on 2008年5月29日
            //如果attList.Count小于或等于0的话证明该类型没有定制特性
            if (attList.Count > 0)
            {
                ///根据AutoAttributeData集合获得GroupAttsData集合
                SortedDictionary<int, GroupAttsData> groupBoxList = ToGroupDatas(attList);

                if (groupBoxList != null)
                {
                    foreach (var pair in groupBoxList)
                    {
                        //设置GroupBox的时候，则需要将groupBox的相关属性放在第个组的第一项(规定)
                        AutoAttributeData lastAutoAtt = pair.Value.AutoAttributeDatas[0];
                        bool isGroupDockTop = lastAutoAtt.Attribute.GroupBoxDockTop; //当前的组合框是否要置顶
                        string groupText = lastAutoAtt.Attribute.GroupBoxUseWinStyleText; //当前的组合框的显示文本
                        bool useGroup = lastAutoAtt.Attribute.GroupBoxUseWinStyle;
                        //创建groupbox 
                        GroupBoxEx box = CreateGroup(pair.Value);

                        //处理当需要将Group置为Dock.Top时的办法（如果置控件的Dock属性是无效的）
                        if (isGroupDockTop)
                        {
                            box.Dock = DockStyle.Top;
                            
                        }
                        //处理控件的语言文本
                        if (!string.IsNullOrEmpty(groupText))
                        {
                            if (box.InnerGroupBox != null)
                            {
                                //获得语言文本
                                box.InnerGroupBox.Text = GetLanguageText(
                                        AutoLayoutPanelXmlDocument.Singler, groupText);
                            }
                        }
                        this.Controls.Add(box);
                    }
                }
            }//if
        }

        #endregion


        #region 取值存值相关 Save FillValue
        /// <summary>
        /// 为控件取值并填充
        /// 根据传递来的object获取数据填充入数据主控件中去
        /// </summary>
        public void FillValue(object[] objs)
        {
            Debug.Assert(!IsStatic);

            if (objs == null || objs.Length <= 0)
            {
                return;
            }

            FillValueCore(this, objs);
            
        }

        /// <summary>
        /// 为控件取值并填充(一般IsStatic时使用此方法)
        /// 根据传递来的object获取数据填充入数据主控件中去
        /// </summary>
        public void FillValue()
        {
            Debug.Assert(IsStatic);

            FillValueCore(this);
        }
        //获取控件所对应的值，AutoPanel中的所有控件赋值 by lisuye on 2008年5月29日
        private void FillValueCore(Control control,object[] objs)
        {
            foreach (var item in ValueControls)
            {
                item.FillValue(objs);
            }
        }
        private void FillValueCore(Control control)
        {
            foreach (var item in ValueControls)
            {
                item.FillValue();
            }
        }

        /// <summary>
        /// 将控件的值保存到对象。(注意:由于控件不知道对象对应的文件，
        /// 所以这里的保存只是将界面的值保存到对象中，与文件无关)
        /// </summary>
        public virtual void Save()
        {
            foreach (var item in ValueControls)
            {
                item.Save();
            }
        }

        public event EventHandler<ValueSaveEventArgs> Saved;
        protected internal virtual void OnSaved(ValueSaveEventArgs e)
        {
            if (Saved != null)
            {
                Saved(this, e);
            }
        }


        #endregion


        #region 内部类：TypeAndInterfaceArr 类型与该类型中实现的接口集合，实现了比较

        /// <summary>
        /// 类型与该类型中实现在的接口集合
        /// </summary>
        /// 

        protected internal class TypeAndInterfaceArr
        {
            public TypeAndInterfaceArr(Type type, Type[] interfaces,string panel)
            {
                _classType = type;
                _interfaces = interfaces;
                this.Panel = panel;
            }
            public TypeAndInterfaceArr(Type type, string panel)
                : this(type, null, panel)
            {
            }

            public string Panel { get; set; }

            private Type _classType;
            public Type ClassType
            {
                get { return _classType; }
                set { _classType = value; }
            }

            private Type[] _interfaces;
            public Type[] Interfaces
            {
                get { return _interfaces; }
                set { _interfaces = value; }
            }

            // override object.Equals
            public override bool Equals(object obj)
            {
                if (obj == null || GetType() != obj.GetType())
                {
                    return false;
                }

                TypeAndInterfaceArr targetObj = (TypeAndInterfaceArr)obj;

                if (this.ClassType != targetObj.ClassType)
                {
                    return false;
                }

                //if (!Utility.IsAllEquals<Type>(this.Interfaces, targetObj.Interfaces))
                //{
                //    return false;
                //}

                if (this.Panel != targetObj.Panel)
                {
                    return false;
                }

                return true;
            }

            // override object.GetHashCode
            public override int GetHashCode()
            {
                return this.ClassType.GetHashCode();
            }
        }

        #endregion
    }

    /// <summary>
    /// Auto的定制特性和属性(数据类)
    /// </summary>
    /// 
    //定制特性中相关Type的保存留有一会取得属性所对应的值  by lisuye on 2008年5月29日
    public class AutoAttributeData
    {
        public AutoLayoutPanelAttribute Attribute { get; set; }
        public MemberInfo Property { get; set; }

        public AutoAttributeData(MemberInfo propertyInfo, AutoLayoutPanelAttribute attribute)
        {
            this.Attribute = attribute;
            this.Property = propertyInfo;
        }
        public AutoAttributeData()
        {
        }
    }

    /// <summary>
    /// 一个Group对应的数据(GroupBoxIndex和一个AutoAttributeData的集合)
    /// 对每个groupbox对应的数据存储（包括一个Index和一个数据集合）by lisuye on 2008年5月29日
    /// </summary>
    public class GroupAttsData
    {
        public int GroupBoxIndex { get; private set; }
        public List<AutoAttributeData> AutoAttributeDatas { get; private set; }
        public GroupAttsData(int groupBoxIndex)
        {
            this.GroupBoxIndex = groupBoxIndex;
            AutoAttributeDatas = new List<AutoAttributeData>();
        }
    }

    public class ValueSaveEventArgs : EventArgs
    {
        public object Value { get; private set; }
        public object TargetObj { get; private set; }
        public ValueSaveEventArgs(object value,object targetObj)
        {
            this.Value = value;
            this.TargetObj = targetObj;
        }
    }
}