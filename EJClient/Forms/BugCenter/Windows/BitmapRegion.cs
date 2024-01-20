using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Way.Windows
{
    /// <summary>
    /// 
    /// </summary>
    public class BitmapRegion
    {
        /// <summary>
        /// 
        /// </summary>
        public BitmapRegion()
        { }

        /// <summary> 
        /// ����λͼ�в�͸�����ֵı߽�
        /// </summary> 
        /// <param name="bitmap">The Bitmap object to calculate our graphics path from</param> 
        /// <returns>Calculated graphics path</returns> 
        public static GraphicsPath CalculateControlGraphicsPath(Bitmap bitmap)
        {
            // Create GraphicsPath for our bitmap calculation 
            //���� GraphicsPath
            GraphicsPath graphicsPath = new GraphicsPath();
            // Use the top left pixel as our transparent color 
            //ʹ�����Ͻǵ�һ�����ɫ��Ϊ����͸��ɫ
            Color colorTransparent = bitmap.GetPixel(0, 0);
            // This is to store the column value where an opaque pixel is first found. 
            // This value will determine where we start scanning for trailing opaque pixels.
            //��һ���ҵ����X
            int colOpaquePixel = 0;
            // Go through all rows (Y axis) 
            // ƫ�������У�Y����
            for (int row = 0; row < bitmap.Height; row++)
            {
                // Reset value 
                //����
                colOpaquePixel = 0;
                // Go through all columns (X axis) 
                //ƫ�������У�X����
                for (int col = 0; col < bitmap.Width; col++)
                {
                    // If this is an opaque pixel, mark it and search for anymore trailing behind 
                    //����ǲ���Ҫ͸������ĵ����ǣ�Ȼ�����ƫ��
                    if (bitmap.GetPixel(col, row) != colorTransparent)
                    {
                        // Opaque pixel found, mark current position
                        //��¼��ǰ
                        colOpaquePixel = col;
                        // Create another variable to set the current pixel position 
                        //�����±�������¼��ǰ��
                        int colNext = col;
                        // Starting from current found opaque pixel, search for anymore opaque pixels 
                        // trailing behind, until a transparent   pixel is found or minimum width is reached 
                        ///���ҵ��Ĳ�͸���㿪ʼ������Ѱ�Ҳ�͸����,һֱ���ҵ�����ﵽͼƬ��� 
                        for (colNext = colOpaquePixel; colNext < bitmap.Width; colNext++)
                            if (bitmap.GetPixel(colNext, row) == colorTransparent)
                                break;
                        // Form a rectangle for line of opaque   pixels found and add it to our graphics path 
                        //����͸����ӵ�graphics path
                        graphicsPath.AddRectangle(new Rectangle(colOpaquePixel, row, colNext - colOpaquePixel, 1));
                        // No need to scan the line of opaque pixels just found 
                        col = colNext;
                    }
                }
            }
            // Return calculated graphics path 
            return graphicsPath;
        }
    }
}
