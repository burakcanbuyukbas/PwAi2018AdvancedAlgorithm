namespace HousesAndWells
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxWells = new System.Windows.Forms.TextBox();
            this.textBoxConstant = new System.Windows.Forms.TextBox();
            this.textBoxHouses = new System.Windows.Forms.TextBox();
            this.buttonRandomLocations = new System.Windows.Forms.Button();
            this.fileInputButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Well Number:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Constant:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "House Number:";
            // 
            // textBoxWells
            // 
            this.textBoxWells.Location = new System.Drawing.Point(119, 20);
            this.textBoxWells.MaxLength = 2;
            this.textBoxWells.Name = "textBoxWells";
            this.textBoxWells.Size = new System.Drawing.Size(135, 20);
            this.textBoxWells.TabIndex = 3;
            this.textBoxWells.TextChanged += new System.EventHandler(this.textBoxWells_TextChanged);
            this.textBoxWells.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxWells_KeyPress);
            // 
            // textBoxConstant
            // 
            this.textBoxConstant.Location = new System.Drawing.Point(119, 46);
            this.textBoxConstant.MaxLength = 2;
            this.textBoxConstant.Name = "textBoxConstant";
            this.textBoxConstant.Size = new System.Drawing.Size(135, 20);
            this.textBoxConstant.TabIndex = 4;
            this.textBoxConstant.TextChanged += new System.EventHandler(this.textBoxConstant_TextChanged);
            this.textBoxConstant.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxConstant_KeyPress);
            // 
            // textBoxHouses
            // 
            this.textBoxHouses.Location = new System.Drawing.Point(119, 74);
            this.textBoxHouses.Name = "textBoxHouses";
            this.textBoxHouses.ReadOnly = true;
            this.textBoxHouses.Size = new System.Drawing.Size(135, 20);
            this.textBoxHouses.TabIndex = 5;
            // 
            // buttonRandomLocations
            // 
            this.buttonRandomLocations.Location = new System.Drawing.Point(34, 116);
            this.buttonRandomLocations.Name = "buttonRandomLocations";
            this.buttonRandomLocations.Size = new System.Drawing.Size(220, 23);
            this.buttonRandomLocations.TabIndex = 6;
            this.buttonRandomLocations.Text = "Randomize Locations";
            this.buttonRandomLocations.UseVisualStyleBackColor = true;
            this.buttonRandomLocations.Click += new System.EventHandler(this.buttonRandomLocations_Click);
            // 
            // fileInputButton
            // 
            this.fileInputButton.Location = new System.Drawing.Point(34, 161);
            this.fileInputButton.Name = "fileInputButton";
            this.fileInputButton.Size = new System.Drawing.Size(220, 24);
            this.fileInputButton.TabIndex = 7;
            this.fileInputButton.Text = "Input By File";
            this.fileInputButton.UseVisualStyleBackColor = true;
            this.fileInputButton.Click += new System.EventHandler(this.fileInputButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(289, 225);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.fileInputButton);
            this.Controls.Add(this.buttonRandomLocations);
            this.Controls.Add(this.textBoxHouses);
            this.Controls.Add(this.textBoxConstant);
            this.Controls.Add(this.textBoxWells);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxWells;
        private System.Windows.Forms.TextBox textBoxConstant;
        private System.Windows.Forms.TextBox textBoxHouses;
        private System.Windows.Forms.Button buttonRandomLocations;
        private System.Windows.Forms.Button fileInputButton;
        private System.Windows.Forms.Button button1;
    }
}

