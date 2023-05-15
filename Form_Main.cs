using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using NAudio.Wave;

namespace Presentia_Editor {

    public partial class Form_Main : Form {

        public Form_Main() {

            InitializeComponent();
        }

        bool bUsing_Slider = false;

        bool bDragging = false;

        bool bPaused = true;

        bool bResizing_Vertical = false;
        bool bResizing_Horizontal = false;

        int MouseX;
        int MouseY;

        private void Image_in_panel_Import_Mouse_Down( object sender, MouseEventArgs e ) {

            PictureBox pbTemp = (PictureBox)sender;

            if( e.Button == MouseButtons.Right ) {

                int Import_Index = (int)pbTemp.Tag;

                for( int i = Import_Index ; i < lpImport.Controls.Count ; i++ ) {
                    ((PictureBox)lpImport.Controls[i]).Tag = (int)((PictureBox)lpImport.Controls[i]).Tag - 1;
                }

                if( !Global.ptie.Imports[Import_Index].EndsWith(".mp3") ) {
                    for( int i = 0 ; i < Global.ptie.Images.Count ; i++ ) {
                        if( Global.ptie.Images[i].Import > Import_Index ) {
                            Global.ptie.Images[i].Import--;
                        }
                        while( i < Global.ptie.Images.Count && Global.ptie.Images[i].Import == Import_Index ) {
                            Global.ptie.Images.RemoveAt( i );
                        }
                    }
                    Update_Editor_Images();
                }
                if ( Global.ptie.Sound_Import == Import_Index )
                    Global.ptie.Sound_Import = null;
                if ( Global.ptie.Sound_Import > Import_Index )
                    Global.ptie.Sound_Import--;

                Global.ptie.Imports.RemoveAt( Import_Index );
                lpImport.Controls.RemoveAt( Import_Index );
            }
            else if ( e.Button == MouseButtons.Left ) {
                pb_Drag.Tag = pbTemp.Tag;
                pb_Drag.Image = pbTemp.Image;
                pb_Drag.Visible = true;
                bDragging = true;
                timer_Drag.Enabled = true;
                Move_Drag_Image();
            }
        }

        private Point Get_Mouse_Position_Relative_to_Form() {
            Point formPosition              = PointToScreen(new Point(0, 0));
            Rectangle primaryScreenBounds   = Screen.PrimaryScreen.Bounds;
            int formX                       = formPosition.X - primaryScreenBounds.X;
            int formY                       = formPosition.Y - primaryScreenBounds.Y;
            int x                           = Cursor.Position.X - formX;
            int y                           = Cursor.Position.Y - formY;
            return new Point( x, y );
        }

        private void Import_Mouse_Up( object sender, MouseEventArgs e ) {

            if (e.Button == MouseButtons.Left) {

                if( pb_Drag.Location.Y > pEditing_Panel.Location.Y ) {

                    if( Global.ptie.Imports[(int)pb_Drag.Tag].EndsWith( ".mp3" ) ) {

                        Global.ptie.Sound_Import = (int)pb_Drag.Tag;
                        Load_Sound();
                    } else {
                        int Time;
                        if( Global.ptie.Images.Count == 0 ) {
                            Time = 0;
                        } else
                            Time = Global.ptie.Images[Global.ptie.Images.Count - 1].Time + 100;
                        Global.ptie.Images.Add( new PTIE.Image( pb_Drag.Image, (int)pb_Drag.Tag, Time ) );
                    }

                    Update_Editor_Images();
                }

                pb_Drag.Visible = false;
                bDragging = false;
                pb_Drag.Tag = null;
            }
        }

        private void Load_Sound() {

            if (Global.ptie.Sound_Import != null) {
                Global.ptie.Sound_Reader = new Mp3FileReader( Global.ptie.Imports[(int)Global.ptie.Sound_Import] );
                Global.ptie.Sound_Output.Init( Global.ptie.Sound_Reader );
            }
        }

        private void Move_Drag_Image() {

            pb_Drag.Location = Get_Mouse_Position_Relative_to_Form() + new Size( -pb_Drag.Width / 2, -pb_Drag.Height / 2 );

            if( bDragging)
                timer_Drag.Enabled = true;
        }

        private void Form_Main_Load( object sender, EventArgs e ) {

            BackColor = Color.FromArgb( 17, 17, 17 );
            lpImport.BackColor = Color.FromArgb( 23, 23, 23 );
            pEditing_Panel.BackColor = Color.FromArgb( 23, 23, 23 );
            imgDisplay.BackColor = Color.FromArgb( 0, 0, 0);

            lpImport.VerticalScroll.Visible = true;

            for ( int i = 0 ; i < Global.ptie.Imports.Count ; i++ )
                Add_Import_Image( Global.ptie.Imports[i] );

            Update_Editor_Images();

            Progress_Bar.BringToFront();

            Load_Sound();

            Focus();

            DoubleBuffered = true;

            SetStyle( ControlStyles.UserPaint, true );
            SetStyle( ControlStyles.OptimizedDoubleBuffer, true );
            SetStyle( ControlStyles.AllPaintingInWmPaint, true );
        }

        private void Update_Editor_Images() {

            Sort_Images();

            pEditing_Panel.Controls.Clear();

            for( int i = 0 ; i < Global.ptie.Images.Count ; i++ ) {

                PictureBox pbEditor_Image = new PictureBox();

                pbEditor_Image.BackColor = Color.FromArgb( 0, 0, 0 );
                pbEditor_Image.Image = Global.ptie.Images[i].Drawing;
                pbEditor_Image.SizeMode = PictureBoxSizeMode.Zoom;
                pbEditor_Image.Tag = i;

                pbEditor_Image.MouseDown += new MouseEventHandler( Editor_Image_Mouse_Down );

                Label lblTime_to_Appear = new Label();

                pbEditor_Image.Controls.Add( lblTime_to_Appear );

                Label lblIndex = new Label();

                pbEditor_Image.Controls.Add( lblIndex );

                pEditing_Panel.Controls.Add( pbEditor_Image );
            }

            Update_Editor_Images_Size();
        }

        private void Update_Editor_Images_Size() {

            for (int i = 0 ; i < pEditing_Panel.Controls.Count ; i++ ) {

                PictureBox pbEditor_Image = (PictureBox)pEditing_Panel.Controls[i];

                pbEditor_Image.Height = pEditing_Panel.Height - 35;
                pbEditor_Image.Width = pEditing_Panel.Height - 35;

                pbEditor_Image.Location = new Point( (int)(pEditing_Panel.Height * i + 5 * (-i*6 + 1)), 30 );

                pbEditor_Image.Controls[0].AutoSize = true;
                pbEditor_Image.Controls[0].Text = Convert.ToString( Global.ptie.Images[i].Time );
                pbEditor_Image.Controls[0].Location = new Point( 5, pbEditor_Image.Height - 20 );
                pbEditor_Image.Controls[0].BackColor = Color.FromArgb( 127, 0, 0, 0 );
                pbEditor_Image.Controls[0].ForeColor = Color.FromArgb( 255, 255, 255, 255 );

                pbEditor_Image.Controls[1].AutoSize = true;
                pbEditor_Image.Controls[1].Text = Convert.ToString( i + 1 );
                pbEditor_Image.Controls[1].Location = new Point( 5, 4 );
                pbEditor_Image.Controls[1].BackColor = Color.FromArgb( 127, 0, 0, 0 );
                pbEditor_Image.Controls[1].ForeColor = Color.FromArgb( 255, 255, 255, 255 );
            }
        }

        private void Editor_Image_Mouse_Down( object sender, MouseEventArgs e ) {

            if (e.Button == MouseButtons.Left) {

                int? Value;

                PictureBox pbEditor_Image = (PictureBox)sender;

                Value = new IntegerInputDialog().ShowDialog( Global.ptie.Images[(int)pbEditor_Image.Tag].Time );

                if (Value != null) {

                    for (int i = 0 ; i < Global.ptie.Images.Count ; i++ )

                        if ( Global.ptie.Images[i].Time == (int)Value )

                            return;

                    Global.ptie.Images[(int)pbEditor_Image.Tag].Time = (int)Value;
                    Update_Editor_Images();
                }
            }
            if (e.Button == MouseButtons.Right) {

                PictureBox pbEditor_Image = (PictureBox)sender;

                for ( int i = 0 ; i < Global.ptie.Images.Count ; i++ )

                    if( Global.ptie.Images[i].Drawing == pbEditor_Image.Image )

                        Global.ptie.Images.RemoveAt( i );

                Update_Editor_Images();
            }
        }

        private void Sort_Images() {

            List<int> Times = new List<int>();

            for (int i = 0 ; i < Global.ptie.Images.Count ; i++ ) {

                Times.Add( Global.ptie.Images[i].Time );
            }

            Times.Sort();

            List<PTIE.Image> Sorted_Images = new List<PTIE.Image>();

            for( int i = 0 ; i < Global.ptie.Images.Count ; i++ ) {

                int Original_Index;

                for( Original_Index = 0 ; Original_Index < Global.ptie.Images.Count ; Original_Index++ )

                    if( Times[i] == Global.ptie.Images[Original_Index].Time )

                        break;

                Sorted_Images.Add( Global.ptie.Images[Original_Index] );
            }

            Global.ptie.Images = Sorted_Images;
        }

        private void Form_Main_MouseMove( object sender, MouseEventArgs e ) {

            MouseX = e.Location.X;
            MouseY = e.Location.Y;

            if (MouseX > lpImport.Location.X + lpImport.Width &&
                MouseX < imgDisplay.Location.X &&
                MouseY < pEditing_Panel.Location.Y) {

                Cursor.Current = Cursors.SizeWE;
            }

            if (MouseY > imgDisplay.Location.Y + imgDisplay.Height &&
                MouseY < pEditing_Panel.Location.Y) {

                if (Cursor.Current == Cursors.SizeWE)

                    Cursor.Current = Cursors.SizeAll;
                else
                    Cursor.Current = Cursors.SizeNS;
            }

            Resize_Windows();
        }

        private void Resize_Windows() {

            if( bResizing_Vertical ) {

                if( MouseX - lpImport.Location.X - 3 > 80 && Width - MouseX + 3 - 12 - 16 > 80 ) {

                    lpImport.Width          = MouseX - lpImport.Location.X - 3;
                    imgDisplay.Location     = new Point( MouseX + 3, imgDisplay.Location.Y );
                    imgDisplay.Width        = Width - imgDisplay.Location.X - 12 - 16;
                }
            }

            if( bResizing_Horizontal ) {

                if( MouseY - lpImport.Location.Y - 3 > 80 && Height - MouseY + 3 - 12 - 39 > 80 ) {

                    lpImport.Height         = MouseY - lpImport.Location.Y - 3;
                    imgDisplay.Height       = MouseY - imgDisplay.Location.Y - 3;
                    pEditing_Panel.Location = new Point( pEditing_Panel.Location.X, MouseY + 3 );
                    pEditing_Panel.Height   = Height - pEditing_Panel.Location.Y - 12 - 39;

                    Progress_Bar.Location = pEditing_Panel.Location + new Size( 60, 10 );
                    btnPause.Location = pEditing_Panel.Location + new Size( 5, 5 );

                    Update_Editor_Images_Size();
                }
            }
        }

        private void Form_Main_MouseDown( object sender, MouseEventArgs e ) {

            int MouseX = e.Location.X;
            int MouseY = e.Location.Y;

            if (MouseX > lpImport.Location.X + lpImport.Width &&
                MouseX < imgDisplay.Location.X &&
                MouseY < pEditing_Panel.Location.Y)

                    bResizing_Vertical = true;

            if( MouseY > imgDisplay.Location.Y + imgDisplay.Height &&
                MouseY < pEditing_Panel.Location.Y)
                
                    bResizing_Horizontal = true;
        }

        private void Form_Main_MouseUp( object sender, MouseEventArgs e ) {

            bResizing_Vertical = false;
            bResizing_Horizontal = false;
        }

        private void lpImport_DoubleClick( object sender, EventArgs e ) {

            OpenFileDialog File_Picker = new OpenFileDialog();

            File_Picker.Filter = "Images/Audio (*.JPG,*.PNG,*.MP3)|*.JPG;*.PNG;*.MP3|"
                               + "Images (*.JPG,*.PNG)|*.JPG;*.PNG|"
                               + "Audio (*.MP3)|*.MP3";

            File_Picker.Multiselect = true;
            File_Picker.Title = "Select Images or Audio";

            DialogResult Results = File_Picker.ShowDialog();

            foreach (string File in File_Picker.FileNames) {

                if ( !Global.ptie.Imports.Contains ( File ) ) {

                    Global.ptie.Imports.Add( File );

                    Add_Import_Image( File );
                }
            }
        }

        private void Add_Import_Image( string The_File_Path ) {

            PictureBox pbImport_Image = new PictureBox();

            if( The_File_Path.EndsWith( ".mp3" ) )

                pbImport_Image.Image = Image.FromFile( Global.appPath + "Icons\\Audio_File.png" );
            else
                pbImport_Image.Image = Image.FromFile( The_File_Path );

            pbImport_Image.Tag = Global.ptie.Imports.IndexOf(The_File_Path);

            pbImport_Image.BackColor = Color.FromArgb( 0, 0, 0 );

            pbImport_Image.SizeMode = PictureBoxSizeMode.Zoom;
            pbImport_Image.Width = 75;
            pbImport_Image.Height = 75;

            pbImport_Image.MouseDown += new MouseEventHandler( Image_in_panel_Import_Mouse_Down );
            pbImport_Image.MouseUp += new MouseEventHandler( Import_Mouse_Up );

            lpImport.Controls.Add( pbImport_Image );
        }

        private void Form_Main_FormClosing( object sender, FormClosingEventArgs e ) {

            Global.PTIE_File_from_PTIE( Global.ptie, Global.Path );
        }

        private void timer_Drag_Tick( object sender, EventArgs e ) {
            timer_Drag.Enabled = false;
            if( bDragging ) {
                Move_Drag_Image();
            }
        }

        private void btnExport_Click( object sender, EventArgs e ) {

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Export your Presentia project!";
            saveFileDialog.DefaultExt = "ptia";
            saveFileDialog.Filter = "Presentia files (*.ptia)|*.ptia";
            if( saveFileDialog.ShowDialog() == DialogResult.OK )
                Global.PTIA_File_from_PTIE( Global.ptie, saveFileDialog.FileName );
        }

        private void btnPause_Click( object sender, EventArgs e ) {

            bPaused = !bPaused;

            if( !bPaused ) {

                if( Global.ptie.Sound_Import != null ) {
                    btnPause.Text = "Pause";
                    Global.ptie.Sound_Output.Play();
                    timer_Progress_Bar.Enabled = true;
                }
            } else {

                btnPause.Text = "Play";
                if (Global.ptie.Sound_Import != null) {
                    Global.ptie.Sound_Output.Pause();
                    timer_Progress_Bar.Enabled = true;
                }
            }
        }

        private void timer_Progress_Bar_Tick( object sender, EventArgs e ) {

            double milliseconds = Global.ptie.Sound_Reader.CurrentTime.TotalMilliseconds;

            if( !bUsing_Slider ) {

                Progress_Bar.Value = (int)(1000 * (double)Global.ptie.Sound_Reader.CurrentTime.TotalMilliseconds / (double)Global.ptie.Sound_Reader.TotalTime.TotalMilliseconds);
            } else {

                int value = (int)((float)(MousePosition.X - this.Location.X - Progress_Bar.Location.X * 1.1) / Progress_Bar.Size.Width * 1000);

                if( value >= Progress_Bar.Minimum && value <= Progress_Bar.Maximum ) {

                    Progress_Bar.Value = value;
                } else if( value < Progress_Bar.Minimum ) {

                    Progress_Bar.Value = Progress_Bar.Minimum;
                } else if( value > Progress_Bar.Maximum ) {

                    Progress_Bar.Value = Progress_Bar.Maximum;
                }

                milliseconds = (float)value / 1000 * Global.ptie.Sound_Reader.TotalTime.TotalMilliseconds;
            }

            if( milliseconds < 0.0 ) {

                milliseconds = 0;
            } else if( milliseconds > Global.ptie.Sound_Reader.TotalTime.TotalMilliseconds ) {

                milliseconds = Global.ptie.Sound_Reader.TotalTime.TotalMilliseconds;
            }

            //lbCurrent_Time.Text = Global.Time_Format( (int)Math.Floor( (float)milliseconds / 1000 ) );

            for( int i = 0 ; i < Global.ptie.Images.Count - 1 ; i++ ) {

                if( milliseconds >= Global.ptie.Images[i].Time &&
                    milliseconds < Global.ptie.Images[i + 1].Time ) {

                    Display_Image(i);

                    return;
                }
            }

            if (Global.ptie.Images.Count > 0)
                Display_Image( Global.ptie.Images.Count - 1 );
        }

        private void Display_Image( int Index ) {

            SuspendLayout();

            float opacity = 1;

            double milliseconds = Global.ptie.Sound_Reader.CurrentTime.TotalMilliseconds;

            if( Global.ptie.Images[Index].Transition_Type == Transition.Fade_Black
             && Global.ptie.Images[Index].Transition_Time > milliseconds - Global.ptie.Images[Index].Time ) {

                float Factor = (float)(milliseconds - Global.ptie.Images[Index].Time) / Global.ptie.Images[Index].Transition_Time;

                opacity = Factor;
            } else
            if( Global.ptie.Images.Count > Index + 1 ) {
                if( Global.ptie.Images[Index + 1].Transition_Type == Transition.Fade_Black
                && -Global.ptie.Images[Index + 1].Transition_Time < milliseconds - Global.ptie.Images[Index + 1].Time ) {

                    float Factor = (float)(Global.ptie.Images[Index + 1].Time - milliseconds) / Global.ptie.Images[Index].Transition_Time;
                    opacity = Factor;
                }
            }

            Image image = Global.ptie.Images[Index].Drawing;
            Bitmap bmp = new Bitmap(image.Width, image.Height);
            Graphics graphics = Graphics.FromImage(bmp);
            ColorMatrix matrix = new ColorMatrix(new float[][] {
                                                 new float[] {1, 0, 0, 0, 0},
                                                 new float[] {0, 1, 0, 0, 0},
                                                 new float[] {0, 0, 1, 0, 0},
                                                 new float[] {0, 0, 0, opacity, 0},
                                                 new float[] {0, 0, 0, 0, 1}});

            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix( matrix );
            graphics.DrawImage( image, new Rectangle( 0, 0, image.Width, image.Height ), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, attributes );
            imgDisplay.Image?.Dispose();
            imgDisplay.Image = bmp;
            graphics.Dispose();

            ResumeLayout(true);
        }

        private void Progress_Bar_MouseDown( object sender, MouseEventArgs e ) {

            bUsing_Slider = true;
            Global.ptie.Sound_Output.Stop();
        }

        private void Progress_Bar_MouseUp( object sender, MouseEventArgs e ) {

            bUsing_Slider = false;

            TimeSpan time = TimeSpan.FromMilliseconds((float)Progress_Bar.Value / 1000 * Global.ptie.Sound_Reader.TotalTime.TotalMilliseconds);

            Global.ptie.Sound_Reader.CurrentTime = time;

            Global.ptie.Sound_Output.Stop();
            Global.ptie.Sound_Output.Init( Global.ptie.Sound_Reader );

            if( !bPaused ) {

                Global.ptie.Sound_Output.Play();
            }
        }

        private void Form_Main_Resize( object sender, EventArgs e ) {

            imgDisplay.Width = pEditing_Panel.Width - (imgDisplay.Location.X - pEditing_Panel.Location.X);
            imgDisplay.Height = lpImport.Height;
        }
    }
}