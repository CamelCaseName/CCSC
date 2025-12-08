using CSC.Nodestuff;

namespace CSC.Components
{
    public partial class NodeGraphFilter : Form
    {
        static public readonly NodeGraphFilter Instance = new();
        private bool forceUpdate = true;

        public NodeGraphFilter()
        {
            InitializeComponent();

            Visible = false;

            foreach (var type in Enum.GetNames<NodeType>())
            {
                typelist.Items.Add(type);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            //we dont want this instance to be closed...
            e.Cancel = true;
            Visible = false;
        }

        private void TypeListCheckedChanged(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                Main.HiddenTypes.Add(Enum.Parse<NodeType>((string)typelist.Items[e.Index]));
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                Main.HiddenTypes.Remove(Enum.Parse<NodeType>((string)typelist.Items[e.Index]));
            }

            if (forceUpdate)
            {
                Main.ForceRedrawGraph();
            }
        }

        private void HideImported_CheckedChanged(object sender, EventArgs e)
        {
            Main.HideDuped = !Main.HideDuped;

            Main.ForceRedrawGraph();
        }

        private void SetAll_Click(object sender, EventArgs e)
        {
            forceUpdate = false;
            for (int i = 0; i < Enum.GetValues<NodeType>().Length; i++)
            {
                typelist.SetItemChecked(i, true);
            }
            forceUpdate = true;
            Main.ForceRedrawGraph();
        }

        private void DialogueOnly_Click(object sender, EventArgs e)
        {
            forceUpdate = false;
            NodeType[] values = Enum.GetValues<NodeType>();
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] is NodeType.AlternateText or NodeType.Criterion or NodeType.Dialogue or NodeType.EventTrigger or NodeType.Response)
                {
                    typelist.SetItemChecked(i, false);
                }
                else
                {
                    typelist.SetItemChecked(i, true);
                }
            }
            forceUpdate = true;
            Main.ForceRedrawGraph();
        }

        private void ClearAll_Click(object sender, EventArgs e)
        {
            forceUpdate = false;
            for (int i = 0; i < Enum.GetValues<NodeType>().Length; i++)
            {
                typelist.SetItemChecked(i, false);
            }
            forceUpdate = true;
            Main.ForceRedrawGraph();
        }
    }
}
