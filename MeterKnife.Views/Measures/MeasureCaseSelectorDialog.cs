﻿using System;
using System.Windows.Forms;
using MeterKnife.Models;
using MeterKnife.ViewModels;
using NKnife.ControlKnife;

namespace MeterKnife.Views.Measures
{
    public partial class MeasureCaseSelectorDialog : SimpleForm
    {
        private readonly MeasureCaseSelectorViewModel _ViewModel = new MeasureCaseSelectorViewModel();

        public MeasureCaseSelectorDialog()
        {
            InitializeComponent();
        }

        #region Overrides of Form

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            var map = _ViewModel.DiscoverMap;
            foreach (var pair in map)
            {
                var gatewayNode = new TreeNode($"{pair.Key}");
                gatewayNode.Tag = pair.Key;
                _InstrumentsTree.Nodes.Add(gatewayNode);
                foreach (Instrument instrument in pair.Value.Instruments)
                {
                    var node = new TreeNode(instrument.Name);
                    node.Tag = instrument;
                    gatewayNode.Nodes.Add(node);
                }
            }
            _InstrumentsTree.ExpandAll();
        }

        #endregion

        private void InstrumentsManagerButtonClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
            _ViewModel.CallInstrumentsManager();
        }
    }
}