using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Drawing.Imaging;

using AForge.Video;
using AForge.Vision.Motion;
using AForge.Video.DirectShow;
using AForge.Imaging.Filters;

namespace pNeuron.Imagining
{
    public class Capture
    {
        #region Fields

        private IVideoSource videoSource = null;
        private FiltersSequence filters = null;
        private Size size = new Size(0, 0);

        private int framesSec = 0;

        #endregion

        #region Properties

        public int FramesSec
        {
            get { return framesSec; }
        }

        public bool IsRunning
        {
            get { return videoSource == null ? false : videoSource.IsRunning; }
        }

        public FiltersSequence Filters
        {
            get { return filters; }
            set { filters = value; }
        }

        public IVideoSource VideoSource
        {
            get { return videoSource; }
        }

        #endregion

        #region Events

        public delegate void ProcessFrameEventHandler(Bitmap image);
        public event ProcessFrameEventHandler OnProcessFrame;

        #endregion

        #region Constructor

        public Capture()
            : this(new Size(0, 0))
        {
        }

        public Capture(Size CaptureSize)
        {
            size = CaptureSize;

            FilterInfoCollection videoDevices;
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (videoDevices.Count == 0)
                throw new ApplicationException();

            //TODO:ChooseDevice
            videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);

            filters = new FiltersSequence();
        }

        #endregion

        #region Open/Close

        public void OpenVideoSource()
        {
            videoSource.NewFrame += new NewFrameEventHandler(videoSource_NewFrame);
            videoSource.Start();
        }

        public void CloseVideoSource()
        {
            if (videoSource == null)
                return;

            videoSource.SignalToStop();

            for (int i = 0; (i < 20) && (videoSource.IsRunning); i++)
            {
                Thread.Sleep(100);
            }
            if (videoSource.IsRunning)
                videoSource.Stop();

            videoSource = null;
        }

        #endregion

        #region Video Processing

        private Bitmap bmp;
        private TimeSpan diff;
        private DateTime? time10TakesBefore = null;
        private bool busy = false;
        private int takeCount = 0;

        void videoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            if (busy)
                return;

            try
            {

                busy = true;
                takeCount++;

                if (takeCount == 50)
                {
                    diff = DateTime.Now.Subtract(time10TakesBefore.Value);
                    framesSec = takeCount / diff.Seconds;

                    takeCount = 1;
                    time10TakesBefore = null;
                }
                if (!time10TakesBefore.HasValue)
                    time10TakesBefore = DateTime.Now;

                bmp = eventArgs.Frame;

                if (size.IsEmpty)
                    size = bmp.Size;
                else
                {
                    bmp = new Bitmap(bmp, size);
                }

                if (filters.Count > 0)
                {
                    bmp = filters.Apply(bmp);
                }

                bmp = CopyToBpp(bmp, 8);

                if (OnProcessFrame != null)
                    OnProcessFrame(bmp);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                busy = false;
            }
        }

        #endregion

        #region http://www.wischik.com/lu/programmer/1bpp.html

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern int DeleteDC(IntPtr hdc);

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern int BitBlt(IntPtr hdcDst, int xDst, int yDst, int w, int h, IntPtr hdcSrc, int xSrc, int ySrc, int rop);
        static int SRCCOPY = 0x00CC0020;

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        static extern IntPtr CreateDIBSection(IntPtr hdc, ref BITMAPINFO bmi, uint Usage, out IntPtr bits, IntPtr hSection, uint dwOffset);
        static uint BI_RGB = 0;
        static uint DIB_RGB_COLORS = 0;
        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct BITMAPINFO
        {
            public uint biSize;
            public int biWidth, biHeight;
            public short biPlanes, biBitCount;
            public uint biCompression, biSizeImage;
            public int biXPelsPerMeter, biYPelsPerMeter;
            public uint biClrUsed, biClrImportant;
            [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 256)]
            public uint[] cols;
        }

        static uint MAKERGB(int r, int g, int b)
        {
            return ((uint)(b & 255)) | ((uint)((r & 255) << 8)) | ((uint)((g & 255) << 16));
        }

        static System.Drawing.Bitmap CopyToBpp(System.Drawing.Bitmap b, int bpp)
        {
            if (bpp != 1 && bpp != 8) throw new System.ArgumentException("1 or 8", "bpp");

            // Plan: built into Windows GDI is the ability to convert
            // bitmaps from one format to another. Most of the time, this
            // job is actually done by the graphics hardware accelerator card
            // and so is extremely fast. The rest of the time, the job is done by
            // very fast native code.
            // We will call into this GDI functionality from C#. Our plan:
            // (1) Convert our Bitmap into a GDI hbitmap (ie. copy unmanaged->managed)
            // (2) Create a GDI monochrome hbitmap
            // (3) Use GDI "BitBlt" function to copy from hbitmap into monochrome (as above)
            // (4) Convert the monochrone hbitmap into a Bitmap (ie. copy unmanaged->managed)

            int w = b.Width, h = b.Height;
            IntPtr hbm = b.GetHbitmap(); // this is step (1)
            //
            // Step (2): create the monochrome bitmap.
            // "BITMAPINFO" is an interop-struct which we define below.
            // In GDI terms, it's a BITMAPHEADERINFO followed by an array of two RGBQUADs
            BITMAPINFO bmi = new BITMAPINFO();
            bmi.biSize = 40;  // the size of the BITMAPHEADERINFO struct
            bmi.biWidth = w;
            bmi.biHeight = h;
            bmi.biPlanes = 1; // "planes" are confusing. We always use just 1. Read MSDN for more info.
            bmi.biBitCount = (short)bpp; // ie. 1bpp or 8bpp
            bmi.biCompression = BI_RGB; // ie. the pixels in our RGBQUAD table are stored as RGBs, not palette indexes
            bmi.biSizeImage = (uint)(((w + 7) & 0xFFFFFFF8) * h / 8);
            bmi.biXPelsPerMeter = 1000000; // not really important
            bmi.biYPelsPerMeter = 1000000; // not really important
            // Now for the colour table.
            uint ncols = (uint)1 << bpp; // 2 colours for 1bpp; 256 colours for 8bpp
            bmi.biClrUsed = ncols;
            bmi.biClrImportant = ncols;
            bmi.cols = new uint[256]; // The structure always has fixed size 256, even if we end up using fewer colours
            if (bpp == 1) { bmi.cols[0] = MAKERGB(0, 0, 0); bmi.cols[1] = MAKERGB(255, 255, 255); }
            else { for (int i = 0; i < ncols; i++) bmi.cols[i] = MAKERGB(i, i, i); }
            // For 8bpp we've created an palette with just greyscale colours.
            // You can set up any palette you want here. Here are some possibilities:
            // greyscale: for (int i=0; i<256; i++) bmi.cols[i]=MAKERGB(i,i,i);
            // rainbow: bmi.biClrUsed=216; bmi.biClrImportant=216; int[] colv=new int[6]{0,51,102,153,204,255};
            //          for (int i=0; i<216; i++) bmi.cols[i]=MAKERGB(colv[i/36],colv[(i/6)%6],colv[i%6]);
            // optimal: a difficult topic: http://en.wikipedia.org/wiki/Color_quantization
            // 
            // Now create the indexed bitmap "hbm0"
            IntPtr bits0; // not used for our purposes. It returns a pointer to the raw bits that make up the bitmap.
            IntPtr hbm0 = CreateDIBSection(IntPtr.Zero, ref bmi, DIB_RGB_COLORS, out bits0, IntPtr.Zero, 0);
            //
            // Step (3): use GDI's BitBlt function to copy from original hbitmap into monocrhome bitmap
            // GDI programming is kind of confusing... nb. The GDI equivalent of "Graphics" is called a "DC".
            IntPtr sdc = GetDC(IntPtr.Zero);       // First we obtain the DC for the screen
            // Next, create a DC for the original hbitmap
            IntPtr hdc = CreateCompatibleDC(sdc); SelectObject(hdc, hbm);
            // and create a DC for the monochrome hbitmap
            IntPtr hdc0 = CreateCompatibleDC(sdc); SelectObject(hdc0, hbm0);
            // Now we can do the BitBlt:
            BitBlt(hdc0, 0, 0, w, h, hdc, 0, 0, SRCCOPY);
            // Step (4): convert this monochrome hbitmap back into a Bitmap:
            System.Drawing.Bitmap b0 = System.Drawing.Bitmap.FromHbitmap(hbm0);
            //
            // Finally some cleanup.
            DeleteDC(hdc);
            DeleteDC(hdc0);
            ReleaseDC(IntPtr.Zero, sdc);
            DeleteObject(hbm);
            DeleteObject(hbm0);
            //
            return b0;
        }

        #endregion
    }
}
