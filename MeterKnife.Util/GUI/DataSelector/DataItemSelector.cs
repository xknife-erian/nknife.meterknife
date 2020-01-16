using System;
using System.Linq;
using System.Windows.Forms;
using MeterKnife.Util.GUI.DataSelector;

namespace NKnife.GUI.WinForm.DataSelector
{
    public abstract partial class DataItemSelector<T> : UserControl
        where T : class
    {
        private Button _Button;
        private TextBox _TextBox;
        private T _Entity;

        protected DataItemSelector()
        {
            InitializeComponent();
            RegistEvent();
        }

        public abstract Func<IQueryable<T>> QueryFunc { get; }

        /// <summary>获取当前的实体对象
        /// </summary>
        public T Entity
        {
            get { return _Entity; }
            set
            {
                if (value != null && !value.Equals(Entity))
                {
                    _Entity = value;
                    OnEntityChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>实体发生变化时的事件
        /// </summary>
        public event EventHandler EntityChangedEvent;

        protected virtual void OnEntityChanged(EventArgs e)
        {
            if (EntityChangedEvent != null)
                EntityChangedEvent(this, e);
        }

        private void RegistEvent()
        {
            _Button.Click += (sender, e) =>
            {
                var form = new SelectForm<T>(QueryFunc) {EntityName = ""};
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (form.ListView.CheckedItems.Count > 0)
                        Entity = (T) form.ListView.CheckedItems[0].Tag;
                }
            };
        }

    }
}