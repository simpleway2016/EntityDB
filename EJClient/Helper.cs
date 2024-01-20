using EJClient.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EJClient
{
    class Helper
    {

        #region 计算文本显示
        /// <summary>
        /// 计算文本显示宽、高
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="fontFamily">The font family.</param>
        /// <param name="fontStyle">The font style.</param>
        /// <param name="fontWeight">The font weight.</param>
        /// <param name="fontStretch">The font stretch.</param>
        /// <param name="fontSize">Size of the font.</param>
        /// <returns></returns>
        public static MeasureSize MeasureTextSize(
            string text,
            System.Windows.Media.FontFamily fontFamily,
            System.Windows.FontStyle fontStyle,
            System.Windows.FontWeight fontWeight,
            System.Windows.FontStretch fontStretch,
            double fontSize)
        {
            System.Windows.Media.FormattedText ft = new System.Windows.Media.FormattedText(text,
                                                    System.Globalization.CultureInfo.CurrentCulture,
                                                    System.Windows.FlowDirection.LeftToRight,
                                                    new System.Windows.Media.Typeface(fontFamily, fontStyle, fontWeight, fontStretch),
                                                    fontSize,
                                                    System.Windows.Media.Brushes.Black);
            return new MeasureSize(ft.Width, ft.Height);
        }
        /// <summary>
        /// 计算文本显示宽、高
        /// </summary>
        public static MeasureSize MeasureText(string text,
            System.Windows.Media.FontFamily fontFamily,
            System.Windows.FontStyle fontStyle,
            System.Windows.FontWeight fontWeight,
            System.Windows.FontStretch fontStretch,
            double fontSize)
        {
            System.Windows.Media.Typeface typeface = new System.Windows.Media.Typeface(fontFamily, fontStyle, fontWeight, fontStretch);
            System.Windows.Media.GlyphTypeface glyphTypeface;
            if (!typeface.TryGetGlyphTypeface(out glyphTypeface))
            {
                return MeasureTextSize(text, fontFamily, fontStyle, fontWeight, fontStretch, fontSize);
            }
            double totalWidth = 0;
            double height = 0;
            for (int n = 0; n < text.Length; n++)
            {
                ushort glyphIndex = glyphTypeface.CharacterToGlyphMap[text[n]];
                double width = glyphTypeface.AdvanceWidths[glyphIndex] * fontSize;
                double glyphHeight = glyphTypeface.AdvanceHeights[glyphIndex] * fontSize;
                if (glyphHeight > height)
                {
                    height = glyphHeight;
                }
                totalWidth += width;
            }
            return new MeasureSize(totalWidth, height);
        }
        /// <summary>
        /// 显示文本尺寸
        /// </summary>
        public class MeasureSize
        {
            /// <summary>
            /// 宽度
            /// </summary>
            public double Width { get; set; }
            /// <summary>
            /// 高度
            /// </summary>
            public double Height { get; set; }
            /// <summary>
            /// 构造
            /// </summary>
            public MeasureSize() { }
            /// <summary>
            /// 构造
            /// </summary>
            /// <param name="width"></param>
            /// <param name="height"></param>
            public MeasureSize(double width, double height)
            {
                this.Width = width;
                this.Height = height;
            }
        }
        #endregion

        public static string WebSite;
        public static RemotingClient Client;
        public static EJ.User_RoleEnum CurrentUserRole;
        public static int CurrentUserID;

        public static void ShowMessage( Window win , string msg)
        {
            MessageBox.Show(win, msg);
        }
      
        public static void ShowMessage(string msg)
        {
            if(MainWindow.instance != null)
            MessageBox.Show(MainWindow.instance, msg);
            else
                MessageBox.Show( msg);
        }
        public static void ShowError( Window win , Exception err)
        {
            MessageBox.Show(win, err.Message);
        }
        public static void ShowError(Window win, string err)
        {
            MessageBox.Show(win, err);
        }
        public static void ShowError(Exception err)
        {
            if (MainWindow.instance != null)
            MessageBox.Show(MainWindow.instance, err.Message);
            else
                MessageBox.Show(err.Message);
        }
        public static void ShowError(string err)
        {
            if (MainWindow.instance != null)
                MessageBox.Show(MainWindow.instance, err);
            else
                MessageBox.Show(err);
        }
        public static object Clone(object src)
        {
            Type type = src.GetType();
            object clone = Activator.CreateInstance(type);
            System.Reflection.PropertyInfo[] pinfos = type.GetProperties();
            for (int i = 0; i < pinfos.Length; i++)
            {
                try
                {
                    object pvalue = pinfos[i].GetValue(src);
                    pinfos[i].SetValue( clone , pvalue );
                }
                catch
                {
                }
            }
            return clone;
        }
    }

    static class MyExtensions
    {
        public static string FileMD5(this string filePath)
        {
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(filePath))
                {
                    byte[] hash = md5.ComputeHash(stream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLower();
                }
            }
        }
        public static string ToJsonString(this object obj)
        {
            if (obj == null)
                return null;
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
        public static T ToJsonObject<T>(this string str)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(str);
        }
    }
}
