﻿namespace Botti
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
            this.startButton = new System.Windows.Forms.Button();
            this.DebugTextBox = new System.Windows.Forms.TextBox();
            this.stopButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.StatusBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ElapsedMsBox = new System.Windows.Forms.TextBox();
            this.CmdIndexBox = new System.Windows.Forms.TextBox();
            this.TrackLabel = new System.Windows.Forms.Label();
            this.trackComboBox = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(69, 75);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 0;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartButtonClick);
            // 
            // DebugTextBox
            // 
            this.DebugTextBox.Location = new System.Drawing.Point(140, 114);
            this.DebugTextBox.Name = "DebugTextBox";
            this.DebugTextBox.Size = new System.Drawing.Size(351, 20);
            this.DebugTextBox.TabIndex = 1;
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(182, 75);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 2;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.StopButtonClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(66, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Debug";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(303, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Initializer status:";
            // 
            // StatusBox
            // 
            this.StatusBox.Location = new System.Drawing.Point(410, 78);
            this.StatusBox.Name = "StatusBox";
            this.StatusBox.Size = new System.Drawing.Size(62, 20);
            this.StatusBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(303, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(139, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Elapsed milliseconds";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(66, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(108, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Command index";
            // 
            // ElapsedMsBox
            // 
            this.ElapsedMsBox.Location = new System.Drawing.Point(448, 146);
            this.ElapsedMsBox.Name = "ElapsedMsBox";
            this.ElapsedMsBox.Size = new System.Drawing.Size(100, 20);
            this.ElapsedMsBox.TabIndex = 8;
            // 
            // CmdIndexBox
            // 
            this.CmdIndexBox.Location = new System.Drawing.Point(182, 146);
            this.CmdIndexBox.Name = "CmdIndexBox";
            this.CmdIndexBox.Size = new System.Drawing.Size(100, 20);
            this.CmdIndexBox.TabIndex = 9;
            // 
            // TrackLabel
            // 
            this.TrackLabel.AutoSize = true;
            this.TrackLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TrackLabel.Location = new System.Drawing.Point(66, 37);
            this.TrackLabel.Name = "TrackLabel";
            this.TrackLabel.Size = new System.Drawing.Size(123, 17);
            this.TrackLabel.TabIndex = 11;
            this.TrackLabel.Text = "Select active track";
            // 
            // trackComboBox
            // 
            this.trackComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.trackComboBox.FormattingEnabled = true;
            this.trackComboBox.Location = new System.Drawing.Point(195, 33);
            this.trackComboBox.Name = "trackComboBox";
            this.trackComboBox.Size = new System.Drawing.Size(277, 21);
            this.trackComboBox.TabIndex = 10;
            this.trackComboBox.SelectedIndexChanged += new System.EventHandler(this.TrackComboboxSelectedItemChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(608, 232);
            this.Controls.Add(this.TrackLabel);
            this.Controls.Add(this.trackComboBox);
            this.Controls.Add(this.CmdIndexBox);
            this.Controls.Add(this.ElapsedMsBox);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.StatusBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.DebugTextBox);
            this.Controls.Add(this.startButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.TextBox DebugTextBox;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox StatusBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ElapsedMsBox;
        private System.Windows.Forms.TextBox CmdIndexBox;
        private System.Windows.Forms.Label TrackLabel;
        private System.Windows.Forms.ComboBox trackComboBox;
    }
}

