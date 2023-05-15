using NAudio.Wave;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Presentia_Editor {

    internal class Global {

        public static string Path;
        public static string appPath;

        public static PTIE ptie = new PTIE();

        public static PTIE      PTIE_from_PTIE_File         ( string    The_PTIE_File_Path                              ) {

            string Json = File.ReadAllText( The_PTIE_File_Path );

            if (Json == "")
                return PTIE_from_Nothing();

            PTIE_File The_PTIE_File = new PTIE_File();
            The_PTIE_File = JsonConvert.DeserializeObject<PTIE_File>(Json);

            return PTIE_from_PTIE_File( The_PTIE_File );
        }
        public static PTIE      PTIE_from_PTIE_File         ( PTIE_File The_PTIE_File                                   ) {

            PTIE The_PTIE = new PTIE();

            The_PTIE.Imports = The_PTIE_File.Imports;
            The_PTIE.Sound_Import = The_PTIE_File.Sound_Import;

            The_PTIE.Imports = The_PTIE.Imports.Distinct().ToList();

            for( int i = 0 ; i < The_PTIE.Imports.Count ; i++ ) {

                while( i < The_PTIE.Imports.Count && !File.Exists( The_PTIE.Imports[i] ) ) {

                    The_PTIE.Imports.RemoveAt( i );
                }
            }

            for ( int i = 0 ; i < The_PTIE_File.Images.Count ; i++ )
                The_PTIE.Images.Add( new PTIE.Image( System.Drawing.Image.FromFile( The_PTIE_File.Imports[The_PTIE_File.Images[i].Import] ), The_PTIE_File.Images[i].Import, The_PTIE_File.Images[i].Time, The_PTIE_File.Images[i].Transition_Type, The_PTIE_File.Images[i].Transition_Time ) );

            The_PTIE.Sound_Reader = new Mp3FileReader( The_PTIE.Imports[(int)The_PTIE.Sound_Import] );
            The_PTIE.Sound_Output = new WaveOutEvent();

            return The_PTIE;
        }
        public static PTIE      PTIE_from_Nothing           (                                                           ) {

            PTIE The_PTIE = new PTIE();

            The_PTIE.Imports = new List<string>();
            The_PTIE.Images = new List<PTIE.Image>();
            The_PTIE.Sound_Import = null;

            return The_PTIE;
        }

        public static void      PTIE_File_from_PTIE         ( PTIE      The_PTIE            , string The_PTIE_File_Path ) {

            string Json = JsonConvert.SerializeObject( PTIE_File_from_PTIE( The_PTIE ) );
            File.WriteAllText( The_PTIE_File_Path, Json );
        }
        public static PTIE_File PTIE_File_from_PTIE         ( PTIE      The_PTIE                                        ) {

            PTIE_File The_PTIE_File = new PTIE_File();

            The_PTIE_File.Imports = The_PTIE.Imports;
            The_PTIE_File.Sound_Import = (int)The_PTIE.Sound_Import;

            for ( int i = 0 ; i < The_PTIE.Images.Count ; i++ )
                The_PTIE_File.Images.Add( new PTIE_File.Image( The_PTIE.Images[i].Import, The_PTIE.Images[i].Time, The_PTIE.Images[i].Transition_Type, The_PTIE.Images[i].Transition_Time ) );

            return The_PTIE_File;
        }

        public static void      PTIA_File_from_PTIE_File    ( string    The_PTIE_File_Path  , string The_PTIA_File_Path ) {
            PTIA_File_from_PTIE( PTIE_from_PTIE_File( The_PTIE_File_Path ), The_PTIA_File_Path );
        }
        public static void      PTIA_File_from_PTIE_File    ( PTIE_File The_PTIE_File       , string The_PTIA_File_Path ) {
            PTIA_File_from_PTIE( PTIE_from_PTIE_File( The_PTIE_File ), The_PTIA_File_Path );
        }
        public static void      PTIA_File_from_PTIE         ( PTIE      The_PTIE            , string The_PTIA_File_Path ) {

            PTIA_File The_PTIA_File = new PTIA_File();

            for (int i = 0 ; i < The_PTIE.Images.Count ; i++ )
                The_PTIA_File.Add_Image( The_PTIE.Images[i].Drawing, The_PTIE.Images[i].Time );

            The_PTIA_File.Set_Sound( File.ReadAllBytes( The_PTIE.Imports[(int)The_PTIE.Sound_Import] ) );

            string json = JsonConvert.SerializeObject(The_PTIA_File);

            The_PTIA_File_Path.Substring(0, The_PTIA_File_Path.LastIndexOf("."));

            File.WriteAllText( The_PTIA_File_Path, json );
        }
    }

    public class PTIE {

        public PTIE() {

            Imports = new List<string>();
            Images = new List<Image>();

            Sound_Output = new WaveOutEvent();
        }

        public List<string> Imports;

        public List<Image> Images;

        public class Image {

            public Image( System.Drawing.Image The_Image, int The_Import_Index, int The_Time ) {

                Drawing = The_Image;

                Import = The_Import_Index;

                Time = The_Time;

                Transition_Type = Transition.None;
                Transition_Time = 0;
            }

            public Image( System.Drawing.Image The_Image, int The_Import_Index, int The_Time, Transition The_Transition_Type, int The_Transition_Time ) {

                Drawing = The_Image;

                Import = The_Import_Index;

                Time = The_Time;

                Transition_Type = The_Transition_Type;
                Transition_Time = The_Transition_Time;
            }

            public System.Drawing.Image Drawing;

            public int Import;

            public int Time;

            public Transition Transition_Type;

            public int Transition_Time;
        }

        public Mp3FileReader Sound_Reader;
        public WaveOutEvent Sound_Output;

        public int? Sound_Import;
    }

    public class PTIE_File {

        public PTIE_File() {

            Imports = new List<string>();
            Images = new List<Image>();
        }

        public List<string> Imports;

        public int Sound_Import;

        public List<Image> Images;

        public class Image {

            public Image() {

                Import = 0;

                Time = 0;

                Transition_Type = Transition.None;
                Transition_Time = 0;

                Loaded = false;
            }

            public Image( int The_Import_Index, int The_Time ) {

                Import = The_Import_Index;

                Time = The_Time;

                Transition_Type = Transition.None;
                Transition_Time = 0;

                Loaded = true;
            }

            public Image( int The_Import_Index, int The_Time, Transition The_Transition_Type, int The_Transition_Time ) {

                Import = The_Import_Index;

                Time = The_Time;

                Transition_Type = The_Transition_Type;
                Transition_Time = The_Transition_Time;

                Loaded = true;
            }

            public bool Loaded;

            public int Import;

            public int Time;

            public Transition Transition_Type;

            public int Transition_Time;
        }
    }

    public enum Transition {

        None            = 0,

        Fade_Black      = 10,
        Fade_Overlap    = 11,

        Move_Up         = 20,
        Move_Down       = 21,
        Move_Left       = 22,
        Move_Right      = 23,

        Scale_Up        = 30,
        Scale_Down      = 31
    }
}