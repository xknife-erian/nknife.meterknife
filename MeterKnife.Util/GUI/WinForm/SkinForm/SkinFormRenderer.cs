using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;

namespace NKnife.GUI.WinForm.SkinForm
{
    /* 作者：Starts_2000
     * 日期：2009-09-20
     * 网站：http://www.csharpwin.com CS 程序员之窗。
     * 你可以免费使用或修改以下代码，但请保留版权信息。
     * 具体请查看 CS程序员之窗开源协议（http://www.csharpwin.com/csol.html）。
     */

    public abstract class SkinFormRenderer
    {
        #region Fields

        private EventHandlerList _events;

        private static readonly object EventRenderSkinFormCaption = new object();
        private static readonly object EventRenderSkinFormBorder = new object();
        private static readonly object EventRenderSkinFormBackground = new object();
        private static readonly object EventRenderSkinFormControlBox = new object();

        #endregion

        #region Constructors

        protected SkinFormRenderer()
        {
        }

        #endregion

        #region Properties

        protected EventHandlerList Events
        {
            get
            {
                if (_events == null)
                {
                    _events = new EventHandlerList();
                }
                return _events;
            }
        }

        #endregion

        #region Events

        public event SkinFormCaptionRenderEventHandler RenderSkinFormCaption
        {
            add { AddHandler(EventRenderSkinFormCaption, value); }
            remove { RemoveHandler(EventRenderSkinFormCaption, value); }
        }

        public event SkinFormBorderRenderEventHandler RenderSkinFormBorder
        {
            add { AddHandler(EventRenderSkinFormBorder, value); }
            remove { RemoveHandler(EventRenderSkinFormBorder, value); }
        }

        public event SkinFormBackgroundRenderEventHandler RenderSkinFormBackground
        {
            add { AddHandler(EventRenderSkinFormBackground, value); }
            remove { RemoveHandler(EventRenderSkinFormBackground, value); }
        }

        public event SkinFormControlBoxRenderEventHandler RenderSkinFormControlBox
        {
            add { AddHandler(EventRenderSkinFormControlBox, value); }
            remove { RemoveHandler(EventRenderSkinFormControlBox, value); }
        }

        #endregion

        #region Public Methods

        public abstract Region CreateRegion(SkinForm form);

        public abstract void InitSkinForm(SkinForm  form);

        public void DrawSkinFormCaption(
            SkinFormCaptionRenderEventArgs e)
        {
            OnRenderSkinFormCaption(e);
            SkinFormCaptionRenderEventHandler handle =
                Events[EventRenderSkinFormCaption]
                as SkinFormCaptionRenderEventHandler;
            if (handle != null)
            {
                handle(this, e);
            }
        }


        public void DrawSkinFormBorder(
            SkinFormBorderRenderEventArgs e)
        {
            OnRenderSkinFormBorder(e);
            SkinFormBorderRenderEventHandler handle =
                Events[EventRenderSkinFormBorder]
                as SkinFormBorderRenderEventHandler;
            if (handle != null)
            {
                handle(this, e);
            }
        }


        public void DrawSkinFormBackground(
            SkinFormBackgroundRenderEventArgs e)
        {
            OnRenderSkinFormBackground(e);
            SkinFormBackgroundRenderEventHandler handle =
                Events[EventRenderSkinFormBackground] 
                as SkinFormBackgroundRenderEventHandler;
            if (handle != null)
            {
                handle(this, e);
            }
        }

        public void DrawSkinFormControlBox(
            SkinFormControlBoxRenderEventArgs e)
        {
            OnRenderSkinFormControlBox(e);
            SkinFormControlBoxRenderEventHandler handle =
                Events[EventRenderSkinFormControlBox]
                as SkinFormControlBoxRenderEventHandler;
            if (handle != null)
            {
                handle(this, e);
            }
        }

        #endregion

        #region Protected Render Methods

        protected abstract void OnRenderSkinFormCaption(
            SkinFormCaptionRenderEventArgs e);

        protected abstract void OnRenderSkinFormBorder(
            SkinFormBorderRenderEventArgs e);

        protected abstract void OnRenderSkinFormBackground(
            SkinFormBackgroundRenderEventArgs e);

        protected abstract void OnRenderSkinFormControlBox(
            SkinFormControlBoxRenderEventArgs e);

        #endregion

        #region Protected Methods

        [UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
        protected void AddHandler(object key, Delegate value)
        {
            Events.AddHandler(key, value);
        }

        [UIPermission(SecurityAction.Demand, Window = UIPermissionWindow.AllWindows)]
        protected void RemoveHandler(object key, Delegate value)
        {
            Events.RemoveHandler(key, value);
        }

        #endregion
    }
}
