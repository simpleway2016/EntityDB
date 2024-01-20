using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.Reflection;
namespace EJClient.UI
{
    /// <summary>
    /// Table.xaml 的交互逻辑
    /// </summary>
    public partial class Table : UserControl
    {
        class MyTextBox : TextBox
        {
            public MyTextBox()
            {
                this.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            }
            protected override void OnMouseUp(MouseButtonEventArgs e)
            {
                this.SelectAll();
                base.OnMouseUp(e);
            }
        }

        public event EventHandler MoveCompleted;
        public class _DataSource
        {
            public EJ.DBTable Table;
            public EJ.DBColumn[] Columns;
        }
        public _DataSource DataSource
        {
            get;
            set;
        }
        internal ModuleDocument OwnerDocument
        {
            get;
            private set;
        }
        public EJ.TableInModule TableInModule
        {
            get;
            set;
        }
        internal Table(ModuleDocument ownerDocument)
        {
            this.OwnerDocument = ownerDocument;
            InitializeComponent();
            this.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            this.VerticalAlignment = System.Windows.VerticalAlignment.Top;

        }
       
       
        /// <summary>
        /// 从服务器更新当前UI
        /// </summary>
        public void ReBindFromServer()
        {
        }

        public System.Drawing.Point GetColumnLocation(int columnid)
        {
            System.Windows.Point wpfPoint = new Point(0, 10);
            if (columnid == 0)
            {
                var pkcolumn = DataSource.Columns.FirstOrDefault(m => m.IsPKID == true);
                if (pkcolumn != null)
                {
                    columnid = pkcolumn.id.GetValueOrDefault();
                }
            }

            foreach (FrameworkElement ctrl in gridColumns.Children)
            {
                if (ctrl.Tag != null && ctrl.Tag.Equals(columnid))
                {
                    System.Windows.Point p2 = ctrl.TranslatePoint(new Point(0, ctrl.ActualHeight / 2), this);
                    wpfPoint = new Point( 0 , p2.Y );
                    break;
                }
            }
            return new System.Drawing.Point(Convert.ToInt32(wpfPoint.X), Convert.ToInt32(wpfPoint.Y));
        }

        bool _IsSelected = false;
        public bool IsSelected
        {
            get
            {
                return _IsSelected;
            }
            set
            {
                if (_IsSelected != value)
                {
                    _IsSelected = value;
                    if (value)
                    {
                        titleArea.Background = new SolidColorBrush(Color.FromArgb(255, 249, 202, 111));
                        txtTitle.Foreground = Brushes.Black;
                    }
                    else
                    {
                        txtTitle.Foreground = Brushes.White;
                        titleArea.Background = new SolidColorBrush(Color.FromArgb(255, 83, 139, 217));
                    }
                }
            }
        }

    
        void setTextBoxStyle(TextBox t,int column)
        {
            t.IsReadOnly = true;
            t.BorderThickness = new Thickness(0);
            if(column > 0)
                Grid.SetColumn(t, column);
            t.Background = Brushes.Transparent;
            t.Height = 21;
            if (column == 3)
            {
                t.Margin = new Thickness(5, 0, 5, 0);
            }
            else if(column >= -1)
            {
                t.Margin = new Thickness(5, 0, 0, 0);
            }
            t.Padding = new Thickness(0, 1, 0, 0);
            //IsReadOnly="True" BorderThickness="0" Grid.Column="0" Background="Transparent" Height="21" Margin="5,0,0,0" Padding="0,1,0,0" Text="Test caption"></TextBox>
        }

        protected override void OnMouseDoubleClick(MouseButtonEventArgs e)
        {
            e.Handled = true;
            TreeNode.DBTableNode.AllTableNodes.FirstOrDefault(m=>m.Table.id == this.DataSource.Table.id).OnDoubleClick(this,e);
        }

        /// <summary>
        /// 绑定显示数据
        /// </summary>
        public void DataBind()
        {
            if (this.DataSource != null)
            {
                txtTitle.Text = this.DataSource.Table.Name + " " + DataSource.Table.caption;
                gridColumns.RowDefinitions.Clear();
                gridColumns.Children.Clear();
                for (int i = 0; i < DataSource.Columns.Length; i++)
                {
                    RowDefinition rowdef = new RowDefinition();
                    rowdef.Height = GridLength.Auto;
                    gridColumns.RowDefinitions.Add(rowdef);

                    EJ.DBColumn Column = DataSource.Columns[i];

                    TextBox t1 = new MyTextBox();
                    t1.Tag = Column.id.GetValueOrDefault();

                    var caption = Column.caption;
                    if (caption == null)
                        caption = "";

                    var text = caption.Split('\n')[0].Trim();
                    string tooltip = null;

                    string enumCaption = "";
                    if (!string.IsNullOrEmpty(Column.EnumDefine))
                    {
                        if (Column.EnumDefine.StartsWith("$") && Column.EnumDefine.Contains("."))
                        {
                            string[] arr = Column.EnumDefine.Substring(1).Split('.');
                            try
                            {
                                var refcolumn = this.OwnerDocument.GetAllTables().Where(m => m.DataSource.Table.Name == arr[0]).
                               Select(m => m.DataSource.Columns.FirstOrDefault(n => n.Name == arr[1])).FirstOrDefault();
                                if (refcolumn != null)
                                {
                                    enumCaption += "\r\n\r\n" + refcolumn.EnumDefine;
                                }
                                else
                                {
                                    enumCaption += "\r\n\r\n" + Column.EnumDefine;
                                }
                            }
                            catch (Exception)
                            {
                                enumCaption += "\r\n\r\n" + Column.EnumDefine;
                            }
                            
                        }
                        else
                        {
                            enumCaption += "\r\n\r\n" + Column.EnumDefine;
                        }
                    }

                    if (!string.IsNullOrEmpty(enumCaption))
                    {
                        try
                        {
                            enumCaption = compileEnum(enumCaption);
                        }
                        catch 
                        {
 
                        }
                        caption += enumCaption;
                    }
                    if (text != caption)
                    {
                        text += "...";
                        tooltip = caption;
                    }

                    t1.Text = text;
                    t1.ToolTip = tooltip;



                    setTextBoxStyle(t1 , 0);
                    Grid.SetRow(t1, i);
                    gridColumns.Children.Add(t1);

                    if( string.IsNullOrEmpty( Column.ClassName))
                    {
                        TextBox t2 = new MyTextBox();
                        t2.Text = Column.Name;
                        setTextBoxStyle(t2, 1);
                        Grid.SetRow(t2, i);
                        gridColumns.Children.Add(t2);
                    }
                    else
                    {
                        StackPanel panel = new StackPanel();
                        panel.Orientation = Orientation.Horizontal;
                        Grid.SetRow(panel, i);
                        Grid.SetColumn(panel, 1);
                        gridColumns.Children.Add(panel);

                        TextBox tClass = new MyTextBox();
                        tClass.TextAlignment = TextAlignment.Left;
                        tClass.Text = Column.ClassName + ".";
                        tClass.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString( "#2b91af"));
                        tClass.FontWeight = FontWeights.Bold;
                        setTextBoxStyle(tClass, -1);
                        panel.Children.Add(tClass);

                        TextBox t2 = new MyTextBox();
                        t2.TextAlignment = TextAlignment.Left;
                        t2.Text = Column.Name;
                        setTextBoxStyle(t2, -2);
                        panel.Children.Add(t2);
                    }
                   

                    TextBox t3 = new MyTextBox();
                    if (Column.CanNull == true)
                    {
                        t3.Text = $"{Column.dbType}?";
                    }
                    else
                    {
                        t3.Text = Column.dbType;
                    }
                    setTextBoxStyle(t3, 2);
                    Grid.SetRow(t3, i);
                    gridColumns.Children.Add(t3);

                    TextBox t4 = new MyTextBox();
                    t4.Text = Column.length;
                    setTextBoxStyle(t4, 3);
                    Grid.SetRow(t4, i);
                    gridColumns.Children.Add(t4);
                }
            }
        }

        static Dictionary<string, string> _enumCacheString = new Dictionary<string, string>();
        string compileEnum(string code)
        {
            if (_enumCacheString.ContainsKey(code))
                return _enumCacheString[code];

            string MyCodeString = @" 
public enum test
{
   "+ code + @"
}";
            //第三步：实现动态编译
            CompilerParameters compilerParams = new CompilerParameters();
            ///编译器选项设置
            compilerParams.CompilerOptions = "/target:library /optimize";
            ///编译时在内存输出
            compilerParams.GenerateInMemory = true;
            ///生成调试信息
            compilerParams.IncludeDebugInformation = false;
            ///添加相关的引用
            compilerParams.ReferencedAssemblies.Add("mscorlib.dll");
            compilerParams.ReferencedAssemblies.Add("System.dll");
            ICodeCompiler compiler = new CSharpCodeProvider().CreateCompiler();
            ///编译
            CompilerResults results = compiler.CompileAssemblyFromSource(compilerParams, MyCodeString);
            //第四步：输出编译结果
            ///创建程序集
            Assembly asm = results.CompiledAssembly;
            var type = asm.GetType("test");
            var names = Enum.GetNames(type);
            var values = Enum.GetValues(type);
            var arr = code.Split('\n');
            for(int i = 0; i < names.Length; i++)
            {
                for(int j = 0; j < arr.Length; j ++)
                {
                    var line = arr[j];
                    if(Regex.IsMatch(line , $@"{names[i]}[ ]?\="))
                    {
                        arr[j] = $"{names[i]} = {Convert.ToInt32( values.GetValue(i))}";
                        break;
                    }
                }
            }
            return _enumCacheString[code] = string.Join("\n" , arr);
        }

        #region 移动
        Point m_oldMousePoint;
        bool m_titleMoving = false;
        Thickness m_oldLocation;
        private void titleArea_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                e.MouseDevice.Capture(this.titleArea);
                m_oldMousePoint = e.GetPosition(this.Parent as IInputElement);
                m_titleMoving = true;
                m_oldLocation = this.Margin;
                e.Handled = true;
            }
        }

        private void titleArea_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_titleMoving)
            {
                e.Handled = true;
                Point p = e.GetPosition(this.Parent as IInputElement);
                this.Margin = new Thickness( m_oldLocation.Left + p.X - m_oldMousePoint.X , m_oldLocation.Top + p.Y - m_oldMousePoint.Y , 0,0 );
            }
        }

        private void titleArea_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (m_titleMoving)
            {
                e.Handled = true;
                e.MouseDevice.Capture(null);
                m_titleMoving = false;
                if (MoveCompleted != null)
                {
                    MoveCompleted(this, null);
                }
            }
        }
        #endregion

        private void MenuItem_ViewData_1(object sender, RoutedEventArgs e)
        {
            Forms.DataViewer frm = new Forms.DataViewer(this.DataSource.Table);
            frm.Show();
        }
        private async void MenuItem_移除_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("确定从当前模块移除吗？", "", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    await Helper.Client.InvokeAsync<string>("RemoveTableFromModule", this.TableInModule.id.Value, this.DataSource.Table.id.Value);
                    ((Panel)this.Parent).Children.Remove(this);
                }
            }
            catch (Exception ex)
            {
                Helper.ShowError(ex);
            }
        }
    }
}
