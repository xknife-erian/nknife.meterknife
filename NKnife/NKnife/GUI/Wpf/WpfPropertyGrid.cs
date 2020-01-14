using System;
using System.Activities.Presentation;
using System.Activities.Presentation.Model;
using System.Activities.Presentation.View;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace NKnife.GUI.Wpf
{
    /// <summary>
    ///     WPF Native PropertyGrid class, taken from Workflow Foundation Designer
    /// </summary>
    public class WpfPropertyGrid : Grid
    {
        #region Private fields

        private readonly WorkflowDesigner _Designer;
        private readonly MethodInfo _OnSelectionChangedMethod;
        private readonly MethodInfo _RefreshMethod;
        private readonly TextBlock _SelectionTypeLabel;
        private object _TheSelectedObject;

        #endregion

        #region Public properties

        /// <summary>
        ///     Get or sets the selected object. Can be null.
        /// </summary>
        public object SelectedObject
        {
            get { return _TheSelectedObject; }
            set
            {
                _TheSelectedObject = value;

                if (value != null)
                {
                    var context = new EditingContext();
                    var mtm = new ModelTreeManager(context);
                    mtm.Load(value);
                    Selection selection = Selection.Select(context, mtm.Root);

                    _OnSelectionChangedMethod.Invoke(_Designer.PropertyInspectorView, new object[] {selection});
                    _SelectionTypeLabel.Text = value.GetType().Name;
                }
                else
                {
                    _OnSelectionChangedMethod.Invoke(_Designer.PropertyInspectorView, new object[] {null});
                    _SelectionTypeLabel.Text = string.Empty;
                }
            }
        }

        /// <summary>
        ///     XAML information with PropertyGrid's font and color information
        /// </summary>
        /// <seealso>Documentation for WorkflowDesigner.PropertyInspectorFontAndColorData</seealso>
        public string FontAndColorData
        {
            get { return string.Empty; }
            set { _Designer.PropertyInspectorFontAndColorData = value; }
        }

        #endregion

        /// <summary>
        ///     Default constructor, creates a hidden designer view and a property inspector
        /// </summary>
        public WpfPropertyGrid()
        {
            _Designer = new WorkflowDesigner();

            UIElement inspector = _Designer.PropertyInspectorView;
            Type inspectorType = inspector.GetType();

            inspector.Visibility = Visibility.Visible;
            Children.Add(inspector);

            _RefreshMethod = inspectorType.GetMethod("RefreshPropertyList",
                BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            _OnSelectionChangedMethod = inspectorType.GetMethod("OnSelectionChanged",
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            _SelectionTypeLabel = inspectorType.GetMethod("get_SelectionTypeLabel",
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance |
                BindingFlags.DeclaredOnly).Invoke(inspector, new object[0]) as TextBlock;

            _SelectionTypeLabel.Text = string.Empty;
        }

        /// <summary>
        ///     Updates the PropertyGrid's properties
        /// </summary>
        public void RefreshPropertyList()
        {
            _RefreshMethod.Invoke(_Designer.PropertyInspectorView, new object[] {false});
        }
    }
}