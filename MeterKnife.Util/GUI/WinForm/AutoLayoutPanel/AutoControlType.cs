namespace NKnife.GUI.WinForm.AutoLayoutPanel
{
    public enum AutoControlType
    {
        TextBox                 = 0,
        ListBox                 = 1,
        ComboBox                = 2,
        Button                  = 3,
        NumericUpDown           = 4,
        ColorGeneralButton      = 5,
        FileSelecterControl     = 6,
        SelectGroup             = 7,
        DateTimePicker          = 8,
        RegexTextBox            = 9,
        /// <summary>
        /// 不需要绑定数据源的CheckBox样式，仅能一个CheckBox被显示，根据IsCheckEd返回Bool值
        /// </summary>
        SimpleCheckBox          = 10,
        DepartmentControl       = 11,
        BuildNumberControl      = 12,
        ProjectPartControl      = 13,
        ProductImage             =14,
        ProductProperty          =15,
        Department               =16,
        ColorSelectorButton      =17,
        FontComboBox              =18,
        HTMLDesignControl        =20,
        ComboBoxGroupControl     =21,
        CheckBoxExControl        =22,
        BiddingAgent             =23,
        DepartmentNameControl    =24,
    }
}
