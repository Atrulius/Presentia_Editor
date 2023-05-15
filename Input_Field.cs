using Presentia_Editor;
using System;
using System.Drawing;
using System.Windows.Forms;

public class IntegerInputDialog : Form {
    private Label lblUnit;
    private Label lblDescription;
    private TextBox txtMilliseconds;
    private Button btnNow;
    private Button btnOk;
    private Button btnCancel;

    public IntegerInputDialog() {

    }

    public int? ShowDialog( int initialValue ) {

        InitializeComponent();

        txtMilliseconds.Text = initialValue.ToString();

        if( ShowDialog() == DialogResult.OK ) {
            int value;
            if( int.TryParse( txtMilliseconds.Text, out value ) ) {
                return value;
            }
        }

        return null;
    }

    private void nowButton_Click( object sender, EventArgs e ) {

        txtMilliseconds.Text = Convert.ToString( Global.ptie.Sound_Reader.CurrentTime.TotalMilliseconds );
    }

    private void InitializeComponent() {
        lblUnit = new System.Windows.Forms.Label();
        lblDescription = new System.Windows.Forms.Label();
        txtMilliseconds = new System.Windows.Forms.TextBox();
        btnNow = new System.Windows.Forms.Button();
        btnOk = new System.Windows.Forms.Button();
        btnCancel = new System.Windows.Forms.Button();
        SuspendLayout();

        lblUnit.BackColor = System.Drawing.Color.White;
        lblUnit.Font = new System.Drawing.Font("Bahnschrift", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        lblUnit.ForeColor = System.Drawing.Color.Black;
        lblUnit.Location = new System.Drawing.Point(76, 42);
        lblUnit.Name = "lblUnit";
        lblUnit.Size = new System.Drawing.Size(69, 14);
        lblUnit.TabIndex = 0;
        lblUnit.Text = "milliseconds";
        lblUnit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;

        lblDescription.Font = new System.Drawing.Font("Bahnschrift", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        lblDescription.ForeColor = System.Drawing.Color.White;
        lblDescription.Location = new System.Drawing.Point(10, 10);
        lblDescription.Name = "lblDescription";
        lblDescription.Size = new System.Drawing.Size(135, 27);
        lblDescription.TabIndex = 1;
        lblDescription.Text = "At what time do you want\r\nthis image to appear?";

        txtMilliseconds.BorderStyle = System.Windows.Forms.BorderStyle.None;
        txtMilliseconds.Font = new System.Drawing.Font("Bahnschrift", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        txtMilliseconds.Location = new System.Drawing.Point(10, 42);
        txtMilliseconds.Margin = new System.Windows.Forms.Padding(5);
        txtMilliseconds.MaxLength = 10;
        txtMilliseconds.Name = "txtMilliseconds";
        txtMilliseconds.Size = new System.Drawing.Size(135, 14);
        txtMilliseconds.TabIndex = 2;

        btnNow.BackColor = System.Drawing.Color.White;
        btnNow.FlatAppearance.BorderSize = 0;
        btnNow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btnNow.Font = new System.Drawing.Font("Bahnschrift", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        btnNow.Location = new System.Drawing.Point(10, 61);
        btnNow.Name = "btnNow";
        btnNow.Size = new System.Drawing.Size(40, 25);
        btnNow.TabIndex = 3;
        btnNow.Text = "Now";
        btnNow.UseVisualStyleBackColor = false;
        btnNow.Click += new System.EventHandler(nowButton_Click);

        btnOk.BackColor = System.Drawing.Color.White;
        btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
        btnOk.FlatAppearance.BorderSize = 0;
        btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btnOk.Font = new System.Drawing.Font("Bahnschrift", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        btnOk.Location = new System.Drawing.Point(55, 61);
        btnOk.Name = "btnOk";
        btnOk.Size = new System.Drawing.Size(30, 25);
        btnOk.TabIndex = 4;
        btnOk.Text = "Ok";
        btnOk.UseVisualStyleBackColor = false;

        btnCancel.BackColor = System.Drawing.Color.White;
        btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        btnCancel.FlatAppearance.BorderSize = 0;
        btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
        btnCancel.Font = new System.Drawing.Font("Bahnschrift", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        btnCancel.Location = new System.Drawing.Point(90, 61);
        btnCancel.Name = "btnCancel";
        btnCancel.Size = new System.Drawing.Size(55, 25);
        btnCancel.TabIndex = 5;
        btnCancel.Text = "Cancel";
        btnCancel.UseVisualStyleBackColor = false;

        BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
        ClientSize = new System.Drawing.Size(155, 96);
        Controls.Add(btnCancel);
        Controls.Add(btnOk);
        Controls.Add(btnNow);
        Controls.Add(lblDescription);
        Controls.Add(lblUnit);
        Controls.Add(txtMilliseconds);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "IntegerInputDialog";
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        Text = "Image time";
        ResumeLayout(false);
        PerformLayout();

    }

    protected override bool ProcessCmdKey( ref Message msg, Keys keyData ) {
        if( keyData == Keys.Escape ) {

            Close();
            return true;
        }
        return base.ProcessCmdKey( ref msg, keyData );
    }
}