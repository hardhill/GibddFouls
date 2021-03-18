
namespace GibddFouls
{
    partial class FormHelp
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
            this.webbro = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // webbro
            // 
            this.webbro.AllowNavigation = false;
            this.webbro.AllowWebBrowserDrop = false;
            this.webbro.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webbro.IsWebBrowserContextMenuEnabled = false;
            this.webbro.Location = new System.Drawing.Point(0, 0);
            this.webbro.MinimumSize = new System.Drawing.Size(20, 20);
            this.webbro.Name = "webbro";
            this.webbro.Size = new System.Drawing.Size(456, 478);
            this.webbro.TabIndex = 0;
            this.webbro.WebBrowserShortcutsEnabled = false;
            // 
            // FormHelp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(456, 478);
            this.Controls.Add(this.webbro);
            this.Name = "FormHelp";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Документация";
            this.Load += new System.EventHandler(this.FormHelp_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.WebBrowser webbro;
    }
}