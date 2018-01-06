namespace KeyHelperForms
{
    partial class MainForm
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
            this.button_StartStop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_StartStop
            // 
            this.button_StartStop.Location = new System.Drawing.Point(158, 239);
            this.button_StartStop.Name = "button_StartStop";
            this.button_StartStop.Size = new System.Drawing.Size(75, 23);
            this.button_StartStop.TabIndex = 0;
            this.button_StartStop.Text = "Start";
            this.button_StartStop.UseVisualStyleBackColor = true;
            this.button_StartStop.Click += new System.EventHandler(this.button_StartStop_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 334);
            this.Controls.Add(this.button_StartStop);
            this.Name = "MainForm";
            this.Text = "KeyHelper";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_StartStop;
    }
}

