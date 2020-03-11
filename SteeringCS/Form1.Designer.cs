namespace SteeringCS
{
    partial class Form1
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
            this.dbPanel1 = new SteeringCS.DBPanel();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.btn_Pause = new System.Windows.Forms.Button();
            this.btn_step = new System.Windows.Forms.Button();
            this.btn_play = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dbPanel1
            // 
            this.dbPanel1.BackColor = System.Drawing.Color.White;
            this.dbPanel1.Location = new System.Drawing.Point(0, 0);
            this.dbPanel1.Name = "dbPanel1";
            this.dbPanel1.Size = new System.Drawing.Size(600, 561);
            this.dbPanel1.TabIndex = 0;
            this.dbPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.dbPanel1_Paint);
            this.dbPanel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dbPanel1_MouseClick);
            // 
            // treeView1
            // 
            this.treeView1.Location = new System.Drawing.Point(607, 13);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(246, 160);
            this.treeView1.TabIndex = 1;
            // 
            // btn_Pause
            // 
            this.btn_Pause.Location = new System.Drawing.Point(667, 526);
            this.btn_Pause.Name = "btn_Pause";
            this.btn_Pause.Size = new System.Drawing.Size(37, 23);
            this.btn_Pause.TabIndex = 2;
            this.btn_Pause.Text = "| |";
            this.btn_Pause.UseVisualStyleBackColor = true;
            this.btn_Pause.Click += new System.EventHandler(this.btn_pause_click);
            // 
            // btn_step
            // 
            this.btn_step.Location = new System.Drawing.Point(710, 526);
            this.btn_step.Name = "btn_step";
            this.btn_step.Size = new System.Drawing.Size(37, 23);
            this.btn_step.TabIndex = 3;
            this.btn_step.Text = ">";
            this.btn_step.UseVisualStyleBackColor = true;
            this.btn_step.Click += new System.EventHandler(this.btn_step_click);
            // 
            // btn_play
            // 
            this.btn_play.Location = new System.Drawing.Point(753, 526);
            this.btn_play.Name = "btn_play";
            this.btn_play.Size = new System.Drawing.Size(37, 23);
            this.btn_play.TabIndex = 4;
            this.btn_play.Text = ">>>";
            this.btn_play.UseVisualStyleBackColor = true;
            this.btn_play.Click += new System.EventHandler(this.btn_play_click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 561);
            this.Controls.Add(this.btn_play);
            this.Controls.Add(this.btn_step);
            this.Controls.Add(this.btn_Pause);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.dbPanel1);
            this.Name = "Form1";
            this.Text = "Steering";
            this.ResumeLayout(false);

        }

        #endregion

        private DBPanel dbPanel1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.Button btn_Pause;
        private System.Windows.Forms.Button btn_step;
        private System.Windows.Forms.Button btn_play;
    }
}

