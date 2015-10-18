﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MeterKnife.Scpis.ScpiTree
{
    public class SubjectGroupTreeNode : TreeNode
    {
        public SubjectGroupTreeNode()
            : this("")
        {
        }

        public SubjectGroupTreeNode(string name)
            : base(name)
        {
            ImageKey = "subject-group";
            SelectedImageKey = "subject-group";
        }
    }
}
