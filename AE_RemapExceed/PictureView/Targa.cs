using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace AE_RemapExceed
{
	public enum TagraImageType
	{
		none = 0,
		IndexCOlor = 0x01,
		FullColor = 0x02,
		Gray = 0x03,
		IndexColorRLE = 0x09,
		FullColorRLE = 0x0A,
		GrayRLE = 0x0B
	}
    public class Targa
    {
		public const int HeaderSzie = 0x12;
		public const int FooterSzie = 0x1A;
		public const int DataOffset = 0x12;
        private bool m_IsTarga = false;

        private int m_IDFieldLength = 0;
        private bool m_ColorMapType = false;
		private TagraImageType m_ImageType = TagraImageType.none;
        private int m_ColorMapIndex = 0;
        private int m_ColorMapLength = 0;
		private int m_ColorMapSize = 0;
        private int m_ImageOriginX = 0;
        private int m_ImageOriginY = 0;
        private int m_ImageWidth = 0;
        private int m_ImageHeight = 0;
        private int m_BitPerPixel = 0;
        private int m_Discripter = 0;

		private bool m_HorBottomTo = true;
		private bool m_VurLeftTo = true;

        //--------------------------------------------------------------------------
        public Targa()
        {
        }
       //--------------------------------------------------------------------------
        public void init()
        {
            m_IsTarga = false;
            m_IDFieldLength = 0;
            m_ColorMapType = false;
            m_ImageType = TagraImageType.none;
            m_ColorMapIndex = 0;
            m_ColorMapLength = 0;
			m_ColorMapSize = 0;
			m_ImageOriginX = 0;
            m_ImageOriginY = 0;
            m_ImageWidth = 0;
            m_ImageHeight = 0;
            m_BitPerPixel = 0;
            m_Discripter = 0;
        }
        //--------------------------------------------------------------------------
		private TagraImageType ImageType(byte v)
		{
			if (v == (int)TagraImageType.FullColor) { return TagraImageType.FullColor; }
			else if (v == (int)TagraImageType.FullColorRLE) { return TagraImageType.FullColorRLE; }
			else { return TagraImageType.none; }
		}
		//--------------------------------------------------------------------------
		public bool getHeader(byte[] hed)
        {

            init();
            if (hed.Length < HeaderSzie) return false;
            m_IDFieldLength = (int)hed[0x00];
            m_ColorMapType = (hed[0x01] == 1);
            m_ImageType = ImageType(hed[0x02]);
			m_ColorMapIndex = (int)hed[0x03] + ((int)hed[0x04] << 8);
			m_ColorMapLength = (int)hed[0x05] + ((int)hed[0x06] << 8);
			m_ColorMapSize = (int)hed[0x07];
			m_ImageOriginX = (int)hed[0x08] + ((int)hed[0x09] << 8);
			m_ImageOriginY = (int)hed[0x0A] + ((int)hed[0x0B] << 8);
			m_ImageWidth = (int)hed[0x0C] + ((int)hed[0x0D] << 8);
            m_ImageHeight= (int)hed[0x0E] + ((int)hed[0x0F] << 8);
            m_BitPerPixel = (int)hed[0x10];
            m_Discripter = (int)hed[0x11];

			m_VurLeftTo = (((m_Discripter >> 4) & 1) == 0);
			m_HorBottomTo = (((m_Discripter >> 5) & 1) == 0);
			m_IsTarga = true;
            if (m_IDFieldLength != 0) m_IsTarga = false;
            if (m_ColorMapType == true) m_IsTarga = false;
            if (m_ImageType == TagraImageType.none) m_IsTarga = false;
            if (m_BitPerPixel < 24) m_IsTarga = false;
			return m_IsTarga;
         
        }
		//--------------------------------------------------------------------------
		public bool chlFooter(byte[] buf)
		{
			int e  = buf.Length;
			if ( e < FooterSzie) return false;

			string s = "";
			for (int i = 18; i > 6; i--)
			{
				s += (char)buf[e - i];
			}
			return ( s == "TRUEVISION");
		}
		//--------------------------------------------------------------------------
		public bool LoadHeader(string path)
		{
			if (File.Exists(path) == false) return false;
			//ファイルを開く
			FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
			try
			{
				byte[] bs = new byte[HeaderSzie];
				int readSize = fs.Read(bs, 0, bs.Length);

				return getHeader(bs);
			}
			catch
			{
				return false;
			}
			finally
			{
				fs.Close();
			}
		}
		//--------------------------------------------------------------------------
		public Bitmap loadTGA(string path)
        {
            if (File.Exists(path) == false) return null;
            //fileNameの内容をByte配列にすべて読み込む
            try
            {
                byte [] buf = System.IO.File.ReadAllBytes(path);
                if (getHeader(buf) == true)
                {
                    if (m_BitPerPixel == 24)
                    {
                        if (m_ImageType == TagraImageType.FullColor) { return decodeRaw24(buf); }
                        else if (m_ImageType == TagraImageType.FullColorRLE) { return  decodeRLE24(buf); }
                    }
                    else if (m_BitPerPixel == 32)
                    {
						if (m_ImageType == TagraImageType.FullColor) { return decodeRaw32(buf); }
						else if (m_ImageType == TagraImageType.FullColorRLE) { return decodeRLE32(buf); }
                    }
                }
            }
            catch
            {
                return null;
            }
            return null;
        }
        //--------------------------------------------------------------------------
        private Bitmap decodeRaw24(byte[] buf)
        {

			if (buf.Length < (m_ImageWidth * 3 * m_ImageHeight + HeaderSzie)) return null;

            Bitmap bmp = new Bitmap(m_ImageWidth, m_ImageHeight, PixelFormat.Format24bppRgb);

			BitmapData bitmapData = bmp.LockBits( 
				new Rectangle(0, 0, bmp.Width, bmp.Height),
				ImageLockMode.ReadWrite,
				PixelFormat.Format24bppRgb);

			IntPtr bmpPtr = bitmapData.Scan0;
			int stride = Math.Abs(bitmapData.Stride);
			int length = stride * bmp.Height;

			int cnt = DataOffset;

            int posHor;
            int posOffset;
            if (m_HorBottomTo == true)
            {
                posHor = (m_ImageHeight - 1) * stride;
                posOffset = -stride;
            }
            else
            {
                posHor = 0;
                posOffset = stride;
            }
			for (int j = 0; j < m_ImageHeight; j++)
			{
				for (int i = 0; i < m_ImageWidth; i++)
				{
					byte b = buf[cnt]; cnt++; 
					byte g = buf[cnt]; cnt++; 
					byte r = buf[cnt]; cnt++; 
					int pos = i * 3 + posHor;

					Marshal.WriteByte(bmpPtr, pos + 0, b);
					Marshal.WriteByte(bmpPtr, pos + 1, g);
					Marshal.WriteByte(bmpPtr, pos + 2, r);
	
				}
                posHor += posOffset;
				
			}


			bmp.UnlockBits(bitmapData);
            return bmp;
        }
     //--------------------------------------------------------------------------
        private Bitmap decodeRaw32(byte[] buf)
        {

			if (buf.Length < (m_ImageWidth * 4 * m_ImageHeight + HeaderSzie)) return null;

			Bitmap bmp = new Bitmap(m_ImageWidth, m_ImageHeight, PixelFormat.Format32bppArgb);

			BitmapData bitmapData = bmp.LockBits(
				new Rectangle(0, 0, bmp.Width, bmp.Height),
				ImageLockMode.ReadWrite,
				PixelFormat.Format32bppArgb);

			IntPtr bmpPtr = bitmapData.Scan0;
			int stride = Math.Abs(bitmapData.Stride);
			int length = stride * bmp.Height;

			int cnt = DataOffset;

            int posHor;
            int posOffset;
            if (m_HorBottomTo == true)
            {
                posHor = (m_ImageHeight - 1) * stride;
                posOffset = -stride;
            }
            else
            {
                posHor = 0;
                posOffset = stride;
            }
            for (int j = 0; j < m_ImageHeight; j++)
			{
				for (int i = 0; i < m_ImageWidth; i++)
				{
					byte b = buf[cnt]; cnt++;
					byte g = buf[cnt]; cnt++;
					byte r = buf[cnt]; cnt++;
					byte a = buf[cnt]; cnt++;
					int pos = i * 4 + posHor;

					Marshal.WriteByte(bmpPtr, pos + 0, b);
					Marshal.WriteByte(bmpPtr, pos + 1, g);
					Marshal.WriteByte(bmpPtr, pos + 2, r);
					Marshal.WriteByte(bmpPtr, pos + 3, a);

				}
                posHor += posOffset;

			}


			bmp.UnlockBits(bitmapData);
			return bmp;

        }
 	    //--------------------------------------------------------------------------
        private Bitmap decodeRLE24(byte[] buf)
        {
			Bitmap bmp = new Bitmap(m_ImageWidth, m_ImageHeight, PixelFormat.Format24bppRgb);

			BitmapData bitmapData = bmp.LockBits(
				new Rectangle(0, 0, bmp.Width, bmp.Height),
				ImageLockMode.ReadWrite,
				PixelFormat.Format24bppRgb);

			IntPtr bmpPtr = bitmapData.Scan0;
			int stride = Math.Abs(bitmapData.Stride);
			int length = stride * bmp.Height;

			int len = buf.Length;


			int i = DataOffset;
			byte[] line = new byte[m_ImageWidth * 3];
			int lineT = 0;
			int aLine = m_ImageHeight - 1;
            int aLineAdd = 1;

            if (m_HorBottomTo == true)
            {
                aLine = m_ImageHeight - 1;
                aLineAdd = -1;
            }
            else
            {
                aLine = 0;
                aLineAdd = 1;
            }

            
            while (i < len)
			{
				int d = (int)(buf[i] >> 7);
				int l = (int)(buf[i] & 0x7F);
				i++;
				byte b, g, r;
				if (d == 1)
				{
					b = buf[i]; i++;
					g = buf[i]; i++;
					r = buf[i]; i++;
					for (int k = 0; k <= l; k++)
					{
						if (lineT < m_ImageWidth)
						{
							int pos = lineT * 3;
							line[pos + 0] = b;
							line[pos + 1] = g;
							line[pos + 2] = r;
							lineT++;
						}
					}
				}
				else
				{
					for (int k = 0; k <= l; k++)
					{
						b = buf[i]; i++;
						g = buf[i]; i++;
						r = buf[i]; i++;
						if (lineT < m_ImageWidth)
						{
							int pos = lineT * 3;
							line[pos + 0] = b;
							line[pos + 1] = g;
							line[pos + 2] = r;
							lineT++;
						}
					}

				}
				if (lineT >= m_ImageWidth)
				{
					int posHor = aLine * stride;
					for (int p = 0; p < m_ImageWidth; p++)
					{

						int pos = p * 3 + posHor;
						int posL = p * 3;

						Marshal.WriteByte(bmpPtr, pos + 0, line[posL + 0]);
						Marshal.WriteByte(bmpPtr, pos + 1, line[posL + 1]);
						Marshal.WriteByte(bmpPtr, pos + 2, line[posL + 2]);

					}
					lineT = 0;
                    aLine += aLineAdd;
				}
                if ((aLine < 0) || (aLine >= m_ImageHeight)) break;


			}
			bmp.UnlockBits(bitmapData);
			return bmp;
		}
		//--------------------------------------------
        private Bitmap decodeRLE32(byte[] buf)
        {
			Bitmap bmp = new Bitmap(m_ImageWidth, m_ImageHeight, PixelFormat.Format32bppRgb);

			BitmapData bitmapData = bmp.LockBits(
				new Rectangle(0, 0, bmp.Width, bmp.Height),
				ImageLockMode.ReadWrite,
				PixelFormat.Format32bppRgb);

			IntPtr bmpPtr = bitmapData.Scan0;
			int stride = Math.Abs(bitmapData.Stride);
			int length = stride * bmp.Height;

			int len = buf.Length;


			int i = DataOffset;
			byte[] line = new byte[m_ImageWidth * 4];
			int lineT = 0;
			int aLine = m_ImageHeight - 1;
            int aLineAdd = 1;

            if (m_HorBottomTo == true)
            {
                aLine = m_ImageHeight - 1;
                aLineAdd = -1;
            }
            else
            {
                aLine = 0;
                aLineAdd = 1;
            }
			while (i < len)
			{
				int d = (int)(buf[i] >> 7);
				int l = (int)(buf[i] & 0x7F);
				i++;
				byte b, g, r,a;
				if (d == 1)
				{
					b = buf[i]; i++;
					g = buf[i]; i++;
					r = buf[i]; i++;
					a = buf[i]; i++;
					for (int k = 0; k <= l; k++)
					{
						if (lineT < m_ImageWidth)
						{
							int pos = lineT * 4;
							line[pos + 0] = b;
							line[pos + 1] = g;
							line[pos + 2] = r;
							line[pos + 3] = a;
							lineT++;
						}
					}
				}
				else
				{
					for (int k = 0; k <= l; k++)
					{
						b = buf[i]; i++;
						g = buf[i]; i++;
						r = buf[i]; i++;
						a = buf[i]; i++;
						if (lineT < m_ImageWidth)
						{
							int pos = lineT * 4;
							line[pos + 0] = b;
							line[pos + 1] = g;
							line[pos + 2] = r;
							line[pos + 3] = a;
							lineT++;
						}
					}

				}
				if (lineT >= m_ImageWidth)
				{
					int posHor = aLine * stride;
					for (int p = 0; p < m_ImageWidth; p++)
					{

						int pos = p * 4 + posHor;
						int posL = p * 4;

						Marshal.WriteByte(bmpPtr, pos + 0, line[posL + 0]);
						Marshal.WriteByte(bmpPtr, pos + 1, line[posL + 1]);
						Marshal.WriteByte(bmpPtr, pos + 2, line[posL + 2]);
						Marshal.WriteByte(bmpPtr, pos + 3, line[posL + 3]);

					}
					lineT = 0;
                    aLine += aLineAdd;
				}
                if ((aLine < 0) || (aLine >= m_ImageHeight)) break;

			};
			bmp.UnlockBits(bitmapData);
			return bmp;
        }
    }
    //--------------------------------------------------------------------------
}
