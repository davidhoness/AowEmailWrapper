﻿namespace AowEmailWrapper.Controls
{
    partial class ActivityListView
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
            this.listView = new System.Windows.Forms.ListView();
            this.colFileName = new System.Windows.Forms.ColumnHeader();
            this.colInwards = new System.Windows.Forms.ColumnHeader();
            this.colOutwards = new System.Windows.Forms.ColumnHeader();
            this.colTicks = new System.Windows.Forms.ColumnHeader();
            this.SuspendLayout();
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colFileName,
            this.colInwards,
            this.colOutwards,
            this.colTicks});
            this.listView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView.FullRowSelect = true;
            this.listView.Location = new System.Drawing.Point(0, 0);
            this.listView.Name = "listView";
            this.listView.Size = new System.Drawing.Size(412, 433);
            this.listView.TabIndex = 1;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.DoubleClick += new System.EventHandler(this.listView_DoubleClick);
            // 
            // colFileName
            // 
            this.colFileName.Text = "File Name";
            this.colFileName.Width = 155;
            // 
            // colInwards
            // 
            this.colInwards.Tag = "Inwards";
            this.colInwards.Text = "In";
            // 
            // colOutwards
            // 
            this.colOutwards.Tag = "Outwards";
            this.colOutwards.Text = "Out";
            // 
            // colTicks
            // 
            this.colTicks.Tag = "Ticks";
            this.colTicks.Text = "Ticks";
            // 
            // ActivityListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listView);
            this.Name = "ActivityListView";
            this.Size = new System.Drawing.Size(412, 433);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader colFileName;
        private System.Windows.Forms.ColumnHeader colInwards;
        private System.Windows.Forms.ColumnHeader colOutwards;
        private System.Windows.Forms.ColumnHeader colTicks;
    }
}
