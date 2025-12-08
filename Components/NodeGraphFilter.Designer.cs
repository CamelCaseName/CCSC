namespace CSC.Components
{
    partial class NodeGraphFilter
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            typelist = new CheckedListBox();
            HideImported = new CheckBox();
            setAll = new Button();
            dialogueOnly = new Button();
            clearAll = new Button();
            SuspendLayout();
            // 
            // typelist
            // 
            typelist.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            typelist.BackColor = Color.FromArgb(40, 40, 40);
            typelist.BorderStyle = BorderStyle.None;
            typelist.ForeColor = Color.Silver;
            typelist.FormattingEnabled = true;
            typelist.Location = new Point(0, 36);
            typelist.Name = "typelist";
            typelist.Size = new Size(334, 252);
            typelist.TabIndex = 0;
            typelist.ItemCheck += TypeListCheckedChanged;
            // 
            // HideImported
            // 
            HideImported.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            HideImported.AutoSize = true;
            HideImported.ForeColor = Color.FromArgb(224, 224, 224);
            HideImported.Location = new Point(0, 292);
            HideImported.Name = "HideImported";
            HideImported.Size = new Size(172, 19);
            HideImported.TabIndex = 1;
            HideImported.Text = "Hide Nodes from other files";
            HideImported.UseVisualStyleBackColor = true;
            HideImported.CheckedChanged += HideImported_CheckedChanged;
            // 
            // setAll
            // 
            setAll.Location = new Point(12, 7);
            setAll.Name = "setAll";
            setAll.Size = new Size(75, 23);
            setAll.TabIndex = 2;
            setAll.Text = "Set all";
            setAll.UseVisualStyleBackColor = true;
            setAll.Click += SetAll_Click;
            // 
            // dialogueOnly
            // 
            dialogueOnly.Location = new Point(93, 7);
            dialogueOnly.Name = "dialogueOnly";
            dialogueOnly.Size = new Size(75, 23);
            dialogueOnly.TabIndex = 3;
            dialogueOnly.Text = "Dialogue only";
            dialogueOnly.UseVisualStyleBackColor = true;
            dialogueOnly.Click += DialogueOnly_Click;
            // 
            // clearAll
            // 
            clearAll.Location = new Point(174, 7);
            clearAll.Name = "clearAll";
            clearAll.Size = new Size(75, 23);
            clearAll.TabIndex = 4;
            clearAll.Text = "Clear all";
            clearAll.UseVisualStyleBackColor = true;
            clearAll.Click += ClearAll_Click;
            // 
            // NodeGraphFilter
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(40, 40, 40);
            ClientSize = new Size(334, 311);
            Controls.Add(clearAll);
            Controls.Add(dialogueOnly);
            Controls.Add(setAll);
            Controls.Add(HideImported);
            Controls.Add(typelist);
            MinimumSize = new Size(350, 155);
            Name = "NodeGraphFilter";
            ShowIcon = false;
            Text = "Tick what node types to hide";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckedListBox typelist;
        private CheckBox HideImported;
        private Button setAll;
        private Button dialogueOnly;
        private Button clearAll;
    }
}