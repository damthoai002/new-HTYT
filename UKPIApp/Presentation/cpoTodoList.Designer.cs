namespace UKPI.Presentation
{
    partial class cpoTodoList
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.trvTodoList = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // trvTodoList
            // 
            this.trvTodoList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.trvTodoList.Location = new System.Drawing.Point(0, 3);
            this.trvTodoList.Name = "trvTodoList";
            this.trvTodoList.Size = new System.Drawing.Size(300, 750);
            this.trvTodoList.TabIndex = 0;
            this.trvTodoList.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            // 
            // cpoTodoList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.trvTodoList);
            this.Name = "cpoTodoList";
            this.Size = new System.Drawing.Size(303, 756);
            this.Load += new System.EventHandler(this.cpoTodoList_Load);
            this.Leave += new System.EventHandler(this.cpoTodoList_Leave);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView trvTodoList;

    }
}
