namespace PingChecker
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox intervaltb;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox tbmaxping;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBoxRouterConnection;
        private System.Windows.Forms.Button buttonRouterIPList;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.intervaltb = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.tbmaxping = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBoxRouterConnection = new System.Windows.Forms.CheckBox();
            this.buttonRouterIPList = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(333, 414);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(101, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Apply Values";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // intervaltb
            // 
            this.intervaltb.Location = new System.Drawing.Point(12, 414);
            this.intervaltb.Name = "intervaltb";
            this.intervaltb.Size = new System.Drawing.Size(100, 20);
            this.intervaltb.TabIndex = 1;
            this.intervaltb.Text = "1000";
            this.intervaltb.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.intervaltb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.intervaltb_KeyPress);
            // 
            // textBox1
            // 
            this.textBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox1.Size = new System.Drawing.Size(422, 314);
            this.textBox1.TabIndex = 2;
            // 
            // btnStartStop
            // 
            this.btnStartStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnStartStop.Location = new System.Drawing.Point(75, 332);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(308, 59);
            this.btnStartStop.TabIndex = 3;
            this.btnStartStop.Text = "Start";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.button2_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(9, 452);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(99, 17);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "High ping alarm";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 488);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(116, 23);
            this.button2.TabIndex = 6;
            this.button2.Text = "Add to Autostart";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(134, 488);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(124, 23);
            this.button3.TabIndex = 7;
            this.button3.Text = "Remove from Autostart";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // tbmaxping
            // 
            this.tbmaxping.Location = new System.Drawing.Point(151, 414);
            this.tbmaxping.Name = "tbmaxping";
            this.tbmaxping.Size = new System.Drawing.Size(96, 20);
            this.tbmaxping.TabIndex = 8;
            this.tbmaxping.Text = "500";
            this.tbmaxping.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbmaxping.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbmaxping_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 398);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Ping check interval";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(157, 398);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Max. ping value";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(328, 498);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Made by Rider, v 1.3";
            // 
            // checkBoxRouterConnection
            // 
            this.checkBoxRouterConnection.AutoSize = true;
            this.checkBoxRouterConnection.Location = new System.Drawing.Point(127, 452);
            this.checkBoxRouterConnection.Name = "checkBoxRouterConnection";
            this.checkBoxRouterConnection.Size = new System.Drawing.Size(133, 17);
            this.checkBoxRouterConnection.TabIndex = 12;
            this.checkBoxRouterConnection.Text = "Ping router connection";
            this.checkBoxRouterConnection.UseVisualStyleBackColor = true;
            // 
            // buttonRouterIPList
            // 
            this.buttonRouterIPList.Location = new System.Drawing.Point(326, 456);
            this.buttonRouterIPList.Name = "buttonRouterIPList";
            this.buttonRouterIPList.Size = new System.Drawing.Size(102, 23);
            this.buttonRouterIPList.TabIndex = 13;
            this.buttonRouterIPList.Text = "Router IP list";
            this.buttonRouterIPList.UseVisualStyleBackColor = true;
            this.buttonRouterIPList.Click += new System.EventHandler(this.buttonRouterIPList_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(108, 472);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(169, 12);
            this.label1.TabIndex = 14;
            this.label1.Text = "(Does not affect the average ping value)";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 526);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonRouterIPList);
            this.Controls.Add(this.checkBoxRouterConnection);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbmaxping);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.btnStartStop);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.intervaltb);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PingChecker";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label label1;
    }
}
