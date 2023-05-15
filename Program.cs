using System;
using System.IO;
using System.Windows.Forms;

namespace Presentia_Editor {

    internal static class Program {

        [STAThread]
        static void Main(string[] args) {

            if (args.Length == 0 ) {

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "Where do you want to store your project?";
                saveFileDialog.DefaultExt = "ptie";
                saveFileDialog.Filter = "Presentia project files (*.ptie)|*.ptie";

                if( saveFileDialog.ShowDialog() == DialogResult.OK ) {

                    Global.Path = saveFileDialog.FileName;

                    if( !File.Exists(Global.Path) ) {

                        File.WriteAllText( Global.Path, "" );
                    }
                }
                else
                    return;
            } else {
                Global.Path = args[0];
            }
            if( File.ReadAllText( Global.Path ) == "" ) 

                Global.ptie = Global.PTIE_from_Nothing();
            else
                Global.ptie = Global.PTIE_from_PTIE_File( Global.Path );

            Global.appPath = Path.GetDirectoryName( Application.ExecutablePath ) + "\\";

            Application.Run( new Form_Main() );
        }
    }
}