using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Windows.Forms;
using NKnife.Events;

namespace NKnife.GUI.WinForm
{
    /// <summary>
    /// This toolstrip implements the multiple image provider interface and thus
    /// provides support for automatic image changes based upon the selected
    /// image size.
    /// </summary>
    public class MultipleImageToolStrip : ToolStrip, IMultipleImageProvider, IImageProvider
    {
        #region Construction and destruction

        public MultipleImageToolStrip()
        {
            HasImagesChanged = false;
            _ImageProvider = new ImageProviderCollection();
            DefaultImageProvider = this;
        }

        #endregion

        #region Events

        /// <summary>
        /// Raised when image size is about to be changed.
        /// </summary>
        public event ImageSizeChangingEventHandler ImageSizeChanging;

        /// <summary>
        /// Raised when image size is changed.
        /// </summary>
        public event ChangedEventHandler<ImageSize> ImageSizeChanged;

        /// <summary>
        /// Raised when the property 'UseUnknownImageSizeIcon' is changed.
        /// </summary>
        public event EventHandler UseUnknownImageSizeIconChanged;

        /// <summary>
        /// Invokes the 'ImageSizeChanging' event handler.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        /// <returns>Returns true when proposed change is accepted.</returns>
        protected virtual bool OnImageSizeChanging(ImageSizeChangingEventArgs e)
        {
            if (e.Cancel)
                return false;

            ImageSizeChangingEventHandler handler = ImageSizeChanging;
            if (handler != null)
            {
                handler(this, e);
                return !e.Cancel;
            }

            return true;
        }

        /// <summary>
        /// Invokes the 'ImageSizeChanged' event handler.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected virtual void OnImageSizeChanged(ChangedEventArgs<ImageSize> e)
        {
            ChangedEventHandler<ImageSize> handler = ImageSizeChanged;
            if (handler != null)
                ImageSizeChanged(this, e);
        }

        /// <summary>
        /// Invokes the 'UseUnknownImageSizeIconChanged' event handler.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnUseUnknownImageSizeIconChanged(EventArgs e)
        {
            // Refresh toolstrip images from providers?
            if (!IsUpdatingImages)
                RefreshItemImages();

            // Raise the associated event handler.
            EventHandler handler = UseUnknownImageSizeIconChanged;
            if (handler != null)
                UseUnknownImageSizeIconChanged(this, e);
        }

        #endregion

        #region IImageProvider Members

        /// <summary>
        /// Queries the image provider for support of a specific size.
        /// </summary>
        /// <param name="size">Indicated image size.</param>
        /// <returns>Returns true when the requested size is supported.</returns>
        public virtual bool IsImageSupported(ImageSize size)
        {
            if (DefaultImageProvider == null)
                return false;
            if (DefaultImageProvider == this)
                return true;

            return DefaultImageProvider.IsImageSupported(size);
        }

        /// <summary>
        /// Fetches an image of the requested size.
        /// </summary>
        /// <param name="size">Size of image to obtain.</param>
        /// <returns>If supported, returns requested image. A value of null
        /// indicates that the requested size is not supported.</returns>
        public virtual Image GetImage(ImageSize size)
        {
            if (DefaultImageProvider == null)
                throw new NullReferenceException();

            if (DefaultImageProvider == this)
            {
                Size iconSize = IconImageProvider.GetIconSize(size);
                Icon iconResult = null; // new Icon(Resources.stop, iconSize);
                return iconResult.ToBitmap();
            }

            return DefaultImageProvider.GetImage(size);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Call to begin a batch image provider update more efficiently.
        /// Each 'BeginImageProviderUpdate' call <b>MUST</b> be paired with
        /// an 'EndImageProviderUpdate' call.
        /// </summary>
        public virtual void BeginUpdateImages()
        {
            IsUpdatingImages = true;
        }

        /// <summary>
        /// Call to end a batch image provider update. Please note that any
        /// image refreshements only occur when all nested updates are ended.
        /// </summary>
        /// <param name="refresh">Indicates if image sizes are to be refreshed.</param>
        public virtual void EndUpdateImages(bool refresh)
        {
            if (!IsUpdatingImages)
                throw new NotSupportedException();

            IsUpdatingImages = false;

            // Only apply updates when image providers have been changed.
            if (HasImagesChanged)
            {
                HasImagesChanged = false;

                // If no longer updating image providers (i.e. no nested calls), then
                // refresh the image sizes.
                if (!IsUpdatingImages && refresh)
                    RefreshItemImages();
            }
        }

        /// <summary>
        /// Call to end a batch image provider update. Please note that
        /// image refreshements only occur when all nested updates are ended.
        /// </summary>
        public void EndUpdateImages()
        {
            EndUpdateImages(true);
        }

        /// <summary>
        /// Assigns an image provider for the specified item.
        /// </summary>
        /// <param name="item">Associated toolstrip item.</param>
        /// <param name="provider">Image provider.</param>
        /// <returns>Returns true when successful.</returns>
        public bool AssignImage(ToolStripItem item, IImageProvider provider)
        {
            if (item == null || provider == null)
                throw new ArgumentException("One or more arguments were null references.");
            if (ContainsImage(item))
                return false;

            ImageProvider.Add(item, provider);
            HasImagesChanged = true;

            if (!IsUpdatingImages)
                RefreshItemImages();

            return true;
        }

        /// <summary>
        /// Assigns an image provider for the specified item.
        /// </summary>
        /// <param name="item">Associated toolstrip item.</param>
        /// <param name="item">Associated multi-icon.</param>
        /// <returns>Returns true when successful.</returns>
        public bool AssignImage(ToolStripItem item, Icon icon)
        {
            return AssignImage(item, new IconImageProvider(icon));
        }

        /// <summary>
        /// Unregisters an image provider.
        /// </summary>
        /// <param name="item">Associated toolstrip item.</param>
        /// <returns>Returns true when successful.</returns>
        public bool RemoveImage(ToolStripItem item)
        {
            if (ImageProvider.Remove(item))
            {
                HasImagesChanged = true;

                if (!IsUpdatingImages)
                    RefreshItemImages();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Remove image providers which are not referenced with a <c>ToolStripItem</c>.
        /// </summary>
        /// <returns>Returns count of items removed.</returns>
        public int RemoveUnusedImages()
        {
            var removeList = new List<ToolStripItem>();
            int count = 0;

            // Compile a list of all items which are to be removed.
            foreach (ToolStripItem key in ImageProvider.Keys)
                if (!Items.Contains(key))
                    removeList.Add(key);
            count = removeList.Count;

            // Remove each item from provider collection.
            foreach (ToolStripItem item in removeList)
                RemoveImage(item);

            // Make sure that the removal list is disposed of.
            removeList = null;
            return count;
        }

        /// <summary>
        /// Searches for the a provider which is associated with a toolstrip item.
        /// </summary>
        /// <param name="item">Toolstrip item.</param>
        /// <returns>Returns true when an associated provider is found.</returns>
        public bool ContainsImage(ToolStripItem item)
        {
            return ImageProvider.ContainsKey(item);
        }

        /// <summary>
        /// Forces all images sizes to be refreshed from the respective providers.
        /// </summary>
        protected void RefreshItemImages()
        {
            Size imageSize = IconImageProvider.GetIconSize(ImageSize);
            ImageScalingSize = imageSize;

            bool changesMade = false;
            IImageProvider imageProvider = null;

            SuspendLayout();

            foreach (ToolStripItem item in Items)
            {
                if (item.Size != imageSize)
                {
                    imageProvider = null;

                    // If an image provider was registered with the toolstrip then...
                    if (ContainsImage(item))
                    {
                        if (IsImageSupported(item, ImageSize))
                            item.Image = GetImage(item, ImageSize);
                        else if (UseUnknownImageSizeIcon && IsImageSupported(ImageSize))
                            item.Image = GetImage(ImageSize);

                        changesMade = true;
                    }
                    else if (item is IImageProvider)
                    {
                        imageProvider = item as IImageProvider;
                    }
                    else if (item.Tag is IImageProvider)
                    {
                        imageProvider = item.Tag as IImageProvider;
                    }

                    // If an alternative image provider was found, attempt to use that.
                    if (!changesMade && imageProvider != null)
                    {
                        if (imageProvider.IsImageSupported(ImageSize))
                        {
                            item.Image = imageProvider.GetImage(ImageSize);
                            changesMade = true;
                        }
                    }

                    // Were changes made?
                    if (changesMade)
                    {
                        // Automatically adjust the image scaling mode.
                        if (item.Image != null && item.Image.Size == imageSize)
                            item.ImageScaling = ToolStripItemImageScaling.None;
                        else
                            item.ImageScaling = ToolStripItemImageScaling.SizeToFit;
                    }
                }
            }

            ResumeLayout();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the default image provider.
        /// </summary>
        public IImageProvider DefaultImageProvider { get; set; }

        /// <summary>
        /// Gets the active multiple image provider.
        /// </summary>
        protected ImageProviderCollection ImageProvider
        {
            get { return _ImageProvider; }
        }

        /// <summary>
        /// Gets or sets the active toolstrip item images sizes.
        /// </summary>
        public ImageSize ImageSize
        {
            get { return _ImageSize; }
            set
            {
                if (value != _ImageSize)
                {
                    var e = new ImageSizeChangingEventArgs(_ImageSize, value);
                    if (OnImageSizeChanging(e))
                    {
                        // Adjust image scaling mode.
                        ImageScalingSize = IconImageProvider.GetIconSize(value);

                        // Adjust image size as specified.
                        _ImageSize = value;
                        RefreshItemImages();
                        OnImageSizeChanged(new ChangedEventArgs<ImageSize>(e.CurrentValue, value));
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets whether a default icon is used to represent unsupported
        /// image sizes.
        /// </summary>
        public bool UseUnknownImageSizeIcon
        {
            get { return _UseUnknownIcon; }
            set
            {
                if (value != _UseUnknownIcon)
                {
                    _UseUnknownIcon = value;
                    OnUseUnknownImageSizeIconChanged(EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Gets a value indicating if image providers are being updated.
        /// </summary>
        public bool IsUpdatingImages
        {
            get { return _UpdatingProviders > 0; }
            private set { _UpdatingProviders += value ? +1 : -1; }
        }

        /// <summary>
        /// Gets or sets a value indicating if one or more image providers have been changed.
        /// </summary>
        protected bool HasImagesChanged { get; set; }

        #endregion

        #region Attributes

        private readonly ImageProviderCollection _ImageProvider;
        private ImageSize _ImageSize = ImageSize.Small;
        private int _UpdatingProviders;
        private bool _UseUnknownIcon;

        #endregion

        #region IMultipleImageProvider Members

        /// <summary>
        /// Queries the image provider for support of a specific size.
        /// </summary>
        /// <param name="key">Key used to identify an image.</param>
        /// <param name="size">Indicated image size</param>
        /// <returns>Returns true when the requested size is supported.</returns>
        public virtual bool IsImageSupported(object key, ImageSize size)
        {
            return ImageProvider.IsImageSupported(key, size);
        }

        /// <summary>
        /// Fetches an image of the requested size.
        /// </summary>
        /// <param name="key">Key used to identify an image.</param>
        /// <param name="size">Size of image to obtain.</param>
        /// <returns>If supported, returns requested image. A value of null
        /// indicates that the requested size is not supported.</returns>
        public virtual Image GetImage(object key, ImageSize size)
        {
            return ImageProvider.GetImage(key, size);
        }

        /// <summary>
        /// Gets count of registered image providers.
        /// </summary>
        public virtual int ImageProviderCount
        {
            get { return ImageProvider.Count; }
        }

        #endregion
    }

    public enum ImageSize
    {
        /// <summary>
        /// An image of 16x16 pixels.
        /// </summary>
        Small,
        /// <summary>
        /// An image of 24x24 pixels.
        /// </summary>
        Medium,
        /// <summary>
        /// An image of 32x32 pixels.
        /// </summary>
        Large,
        /// <summary>
        /// An image of 48x48 pixels.
        /// </summary>
        ExtraLarge,
    }

    /// <summary>
    /// Implementations of this interface must indicate whether or not the
    /// requested image size is supported. A default image override could
    /// be enforced should the provider report with no support.
    /// </summary>
    public interface IImageProvider
    {
        #region Methods

        /// <summary>
        /// Queries the image provider for support of a specific size.
        /// </summary>
        /// <param name="size">Indicated image size.</param>
        /// <returns>Returns true when the requested size is supported.</returns>
        bool IsImageSupported(ImageSize size);

        /// <summary>
        /// Fetches an image of the requested size.
        /// </summary>
        /// <param name="size">Size of image to obtain.</param>
        /// <returns>If supported, returns requested image. A value of null
        /// indicates that the requested size is not supported.</returns>
        Image GetImage(ImageSize size);

        #endregion
    }

    /// <summary>
    /// Implementations of this interface can provide access to multiple
    /// different images of multiple different sizes. A default image override
    /// could be enforced should the provider report with no support.
    /// </summary>
    public interface IMultipleImageProvider
    {
        #region Methods

        /// <summary>
        /// Queries the image provider for support of a specific size.
        /// </summary>
        /// <param name="key">Key used to identify an image.</param>
        /// <param name="size">Indicated image size</param>
        /// <returns>Returns true when the requested size is supported.</returns>
        bool IsImageSupported(object key, ImageSize size);

        /// <summary>
        /// Fetches an image of the requested size.
        /// </summary>
        /// <param name="key">Key used to identify an image.</param>
        /// <param name="size">Size of image to obtain.</param>
        /// <returns>If supported, returns requested image. A value of null
        /// indicates that the requested size is not supported.</returns>
        Image GetImage(object key, ImageSize size);

        #endregion

        #region Properties

        /// <summary>
        /// Gets count of registered image providers.
        /// </summary>
        int ImageProviderCount { get; }

        #endregion
    }

    /// <summary>
    /// Provides a collection which pairs image providers with a key object.
    /// The collection can be used to provide access to different images of
    /// different sizes.
    /// </summary>
    public class ImageProviderCollection : Dictionary<object, IImageProvider>, IMultipleImageProvider
    {
        #region Construction and destruction

        public ImageProviderCollection()
        {
        }

        public ImageProviderCollection(int capacity)
            : base(capacity)
        {
        }

        public ImageProviderCollection(ImageProviderCollection collection)
            : base(collection)
        {
        }

        public ImageProviderCollection(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        #endregion

        #region IMultipleImageProvider Members

        /// <summary>
        /// Queries the image provider for support of a specific size.
        /// </summary>
        /// <param name="key">Key used to identify an image.</param>
        /// <param name="size">Indicated image size</param>
        /// <returns>Returns true when the requested size is supported.</returns>
        public bool IsImageSupported(object key, ImageSize size)
        {
            if (!ContainsKey(key))
                return false;
            return this[key].IsImageSupported(size);
        }

        /// <summary>
        /// Fetches an image of the requested size.
        /// </summary>
        /// <param name="key">Key used to identify an image.</param>
        /// <param name="size">Size of image to obtain.</param>
        /// <returns>If supported, returns requested image. A value of null
        /// indicates that the requested size is not supported.</returns>
        public Image GetImage(object key, ImageSize size)
        {
            if (!ContainsKey(key))
                throw new NullReferenceException();
            return this[key].GetImage(size);
        }

        /// <summary>
        /// Gets count of registered image providers.
        /// </summary>
        int IMultipleImageProvider.ImageProviderCount
        {
            get { return Count; }
        }

        #endregion
    }

    /// <summary>
    /// Allows an icon to be used to provide images of different sizes.
    /// </summary>
    public class IconImageProvider : IImageProvider
    {
        #region Construction and destruction

        public IconImageProvider(Icon icon)
        {
            _SourceIcon = icon;
        }

        public IconImageProvider(Stream stream)
        {
            _SourceIcon = new Icon(stream);
        }

        public IconImageProvider(string fileName)
        {
            _SourceIcon = new Icon(fileName);
        }

        public IconImageProvider(Type type, string resource)
        {
            _SourceIcon = new Icon(type, resource);
        }

        #endregion

        #region Global Utility Methods

        /// <summary>
        /// A utility method which transforms an enumerated image value into
        /// a two-dimensional size.
        /// </summary>
        /// <param name="size">Requested image size.</param>
        /// <returns>Returns a two-dimensional size.</returns>
        public static Size GetIconSize(ImageSize size)
        {
            switch (size)
            {
                case ImageSize.Small:
                    return new Size(16, 16);
                case ImageSize.Medium:
                    return new Size(24, 24);
                case ImageSize.Large:
                    return new Size(32, 32);
                case ImageSize.ExtraLarge:
                    return new Size(48, 48);
                default:
                    throw new NotSupportedException("Invalid image size requested.");
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Raised when the icon property is changed.
        /// </summary>
        public event ChangedEventHandler<Icon> IconChanged;

        /// <summary>
        /// Invoked the 'IconChanged' event.
        /// </summary>
        /// <param name="e">Provides access to old and new icons.</param>
        public virtual void OnIconChanged(ChangedEventArgs<Icon> e)
        {
            if (IconChanged != null)
                IconChanged(this, e);
        }

        #endregion

        #region IImageProvider Members

        /// <summary>
        /// Queries the image provider for support of a specific size.
        /// </summary>
        /// <param name="size">Indicated image size.</param>
        /// <returns>Returns true when the requested size is supported.</returns>
        public bool IsImageSupported(ImageSize size)
        {
            return true;
        }

        /// <summary>
        /// Fetches an image of the requested size.
        /// </summary>
        /// <param name="size">Size of image to obtain.</param>
        /// <returns>If supported, returns requested image. A value of null
        /// indicates that the requested size is not supported.</returns>
        public Image GetImage(ImageSize size)
        {
            var desiredSize = new Icon(SourceIcon, GetIconSize(size));
            return desiredSize.ToBitmap();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the source icon.
        /// </summary>
        public Icon SourceIcon
        {
            get { return _SourceIcon; }
            set
            {
                if (value != _SourceIcon)
                {
                    Icon oldIcon = _SourceIcon;
                    _SourceIcon = value;
                    OnIconChanged(new ChangedEventArgs<Icon>(oldIcon, value));
                }
            }
        }

        #endregion

        #region Attributes

        private Icon _SourceIcon;

        #endregion
    }

    /// <summary>
    /// Provides event handlers with the current and proposed image sizes.
    /// Event handlers can decide whether or not to cancel this procedure.
    /// </summary>
    public class ImageSizeChangingEventArgs : CancelEventArgs
    {
        #region Construction and destruction

        public ImageSizeChangingEventArgs(ImageSize oldValue, ImageSize newValue)
            : base(false)
        {
            _CurrentValue = oldValue;
            _NewValue = newValue;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the current image size.
        /// </summary>
        public ImageSize CurrentValue
        {
            get { return _CurrentValue; }
        }

        /// <summary>
        /// Gets the proposed image size.
        /// </summary>
        public ImageSize NewValue
        {
            get { return _NewValue; }
        }

        #endregion

        #region Attributes

        private readonly ImageSize _CurrentValue;
        private readonly ImageSize _NewValue;

        #endregion
    }

    public delegate void ImageSizeChangingEventHandler(object sender, ImageSizeChangingEventArgs e);

    public delegate void ChangedEventHandler<T>(object sender, ChangedEventArgs<T> e);
}