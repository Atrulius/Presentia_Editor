using System.Collections.Generic;

namespace Presentia_Editor {

    internal class PTIA_File {

        public byte[] Sound;

        public List<Image> Images = new List<Image>();

        public class Image {

            public Image( System.Drawing.Image The_Image, int The_Time ) {

                System.Drawing.ImageConverter converter = new System.Drawing.ImageConverter();
                byte[] arr = (byte[])converter.ConvertTo(The_Image, typeof(byte[]));

                Drawing = arr;

                Time = The_Time;
            }
            public Image( byte[] The_Image, int The_Time ) {

                Drawing = The_Image;

                Time = The_Time;
            }

            public byte[] Drawing;

            public int Time;

            public Transition Transition_Type;

            public int Transition_Time;
        }

        public void Add_Image( System.Drawing.Image Image, int Time ) {

            System.Drawing.ImageConverter converter = new System.Drawing.ImageConverter();
            byte[] arr = (byte[])converter.ConvertTo(Image, typeof(byte[]));

            Images.Add( new Image(arr, Time) );
        }

        public void Set_Sound( byte[] The_Sound ) {

            Sound = The_Sound;
        }
    }
}