using EJ;
using EJClient.Designer;
using EJClient.Forms.InterfaceCenter.Nodes;
using EJClient.TreeNode;
using System;
using System.Activities.Presentation.Model;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Imaging;
using System.Globalization;
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
using System.Windows.Shapes;
using Way.Lib;
namespace EJClient.Forms
{
    /// <summary>
    /// DBTableEditor.xaml 的交互逻辑
    /// </summary>
    public partial class DBTableEditor : Window
    {


        static string[] Tables;
        #region design class

        class MyClassProperty : EJ.classproperty
        {
            DBTableEditor _editor;
            public MyClassProperty(DBTableEditor editor)
            {
                _editor = editor;
                this.ForeignKeys = new System.Collections.ObjectModel.ObservableCollection<ColumnViewModel>();
            }
            public override int? foreignkey_tableid
            {
                get => base.foreignkey_tableid;
                set
                {
                    if (base.foreignkey_tableid != value)
                    {
                        base.foreignkey_tableid = value;

                        this.ForeignKeys.Clear();

                        var columns = Helper.Client.InvokeSync<EJ.DBColumn[]>("GetColumnList", (this.iscollection == false) ? _editor.m_modifyingTable.id.GetValueOrDefault() : value.GetValueOrDefault());
                        foreach (var c in columns)
                        {
                            this.ForeignKeys.Add(new ColumnViewModel(c, _editor));
                        }
                    }
                }
            }

            public override bool? iscollection
            {
                get => base.iscollection;
                set
                {
                    if (base.iscollection != value)
                    {
                        base.iscollection = value;

                        this.ForeignKeys.Clear();

                        var columns = Helper.Client.InvokeSync<EJ.DBColumn[]>("GetColumnList", (this.iscollection == false) ? _editor.m_modifyingTable.id.GetValueOrDefault() : foreignkey_tableid.GetValueOrDefault());
                        foreach (var c in columns)
                        {
                            this.ForeignKeys.Add(new ColumnViewModel(c, _editor));
                        }
                    }
                }
            }


            public System.Collections.ObjectModel.ObservableCollection<ColumnViewModel> ForeignKeys { get; set; }
        }

        /// <summary>
        /// 索引
        /// </summary>
        internal class IndexModel : INotifyPropertyChanged
        {
            public System.Collections.ObjectModel.ObservableCollection<ColumnViewModel> Columns;
            public bool IsUnique;
            public bool IsClustered;

            public IndexModel(System.Collections.ObjectModel.ObservableCollection<ColumnViewModel> columns, bool isUnique, bool isClustered)
            {
                IsUnique = isUnique;
                IsClustered = isClustered;
                this.Columns = columns;
            }
            public string[] ColumnNames
            {
                get;
                set;
            }

            public void OnPropertyChanged()
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("ColumnNames"));
            }
            public event PropertyChangedEventHandler PropertyChanged;

            public event PropertyChangingEventHandler PropertyChanging;
        }
        /// <summary>
        /// 级联删除
        /// </summary>
        class CascadingDeletionViewModel
        {
            int m_databaseid;
            public CascadingDeletionViewModel(int databaseid, string tableName)
            {
                m_databaseid = databaseid;
                this.TableName = tableName;
                Columns = new System.Collections.ObjectModel.ObservableCollection<string>();
                reBindColumns();
            }
            public void reBindColumns()
            {
                Columns.Clear();
                try
                {
                    if (!string.IsNullOrEmpty(this.TableName))
                    {
                        string[] columns = Helper.Client.InvokeSync<string[]>("GetColumnNames", m_databaseid, this.TableName);

                        foreach (string c in columns)
                        {
                            Columns.Add(c);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Helper.ShowError(null, ex.Message);
                }
            }
            public string TableName
            {
                get;
                set;
            }
            public string ColumnName
            {
                get;
                set;
            }
            public string[] Tables
            {
                get
                {
                    return DBTableEditor.Tables;
                }
            }
            public System.Collections.ObjectModel.ObservableCollection<string> Columns
            {
                get;
                set;
            }


        }
        class TableViewModel
        {
            [DisplayName("2:注释")]
            public string Comment
            {
                get
                {
                    return table.caption;
                }
                set
                {
                    table.caption = value.Trim();
                }
            }

            [DisplayName("1:表名")]
            public string TableName
            {
                get
                {
                    return table.Name;
                }
                set
                {
                    table.Name = value.Trim();
                }
            }
            EJ.DBTable table;
            public TableViewModel(EJ.DBTable table)
            {
                this.table = table;
            }
        }

        internal class ColumnViewModel : INotifyPropertyChanged
        {
            internal EJ.DBColumn m_column;
            System.Windows.Forms.PropertyGrid m_pgGrid;
            DBTableEditor m_parentEditor;
            public ColumnViewModel(EJ.DBColumn column, DBTableEditor parentEditor)
            {
                m_parentEditor = parentEditor;
                m_pgGrid = parentEditor.pgridForColumn;
                m_column = column;

                if (!string.IsNullOrEmpty(column.EnumDefine) && column.IsDiscriminator == true)
                {
                    try
                    {
                        ClassNameTypeConvert.EnumNames = ParseNames(column.EnumDefine);
                    }
                    catch
                    {

                    }
                }
            }

            [Browsable(false)]
            public int? id
            {
                get
                {
                    return m_column.id;
                }
            }

            [Browsable(false)]
            public string showCaption
            {
                get
                {
                    if (m_column.caption != null)
                    {
                        return m_column.caption.Split('\n')[0].Trim();
                    }
                    return m_column.caption;
                }
            }

            [Browsable(false)]
            public string ShowInLeft
            {
                get
                {
                    if (!string.IsNullOrEmpty(this.ClassName))
                    {
                        return string.Format("{0}.", this.ClassName);
                    }
                    else
                        return "";
                }
            }


            private double _ShowWidth;
            [Browsable(false)]
            public double ShowWidth
            {
                get => _ShowWidth;
                set
                {
                    if (_ShowWidth != value)
                    {
                        _ShowWidth = value;
                        this.OnPropertyChanged("ShowWidth");
                    }
                }
            }

            [Category("A字段属性"), DisplayName("1:Name")]
            public virtual string Name
            {
                get
                {
                    return m_column.Name;
                }
                set
                {
                    value = value.Trim();
                    if (m_column.Name != value)
                    {
                        if (m_parentEditor.m_columns.Where(m => m != this && string.Equals(m.Name, value, StringComparison.CurrentCultureIgnoreCase)).Count() > 0)
                        {
                            MessageBox.Show(m_parentEditor, "列名与其它列冲突！");
                            return;
                        }

                        foreach (var idxConfigItem in m_parentEditor.m_IDXConfigs)
                        {
                            bool changed = false;
                            for (int i = 0; i < idxConfigItem.ColumnNames.Length; i++)
                            {
                                if (idxConfigItem.ColumnNames[i] == m_column.Name)
                                {
                                    changed = true;
                                    idxConfigItem.ColumnNames[i] = value;
                                }
                            }
                            if (changed)
                            {
                                //要重新创建一个string[]，否则就算触发OnPropertyChanged，也不会引起Selector的OnDataSourceChanged
                                idxConfigItem.ColumnNames = idxConfigItem.ColumnNames.ToJsonString().ToJsonObject<string[]>();
                                idxConfigItem.OnPropertyChanged();
                            }
                        }

                        m_column.Name = value;
                        OnPropertyChanged("Name");
                        OnPropertyChanged("ShowInLeft");
                        m_parentEditor.computeColumnAreaWidth();

                    }
                }

            }

            bool _IsChecked = false;
            [Browsable(false)]
            public virtual bool IsChecked
            {
                get
                {
                    return _IsChecked;
                }
                set
                {
                    if (_IsChecked != value)
                    {
                        _IsChecked = value;
                        if (PropertyChanged != null)
                            PropertyChanged(this, new PropertyChangedEventArgs("IsChecked"));
                    }
                }
            }

            bool _IsSelected = false;
            [Browsable(false)]
            public virtual bool IsSelected
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
                        if (PropertyChanged != null)
                            PropertyChanged(this, new PropertyChangedEventArgs("IsSelected"));
                    }
                }
            }

            [Category("A字段属性"), DisplayName("3:CanNull")
                 , System.ComponentModel.TypeConverter(typeof(BooleanTypeConvert))]
            public bool? CanNull
            {
                get
                {
                    return m_column.CanNull;
                }
                set
                {
                    if (m_column.CanNull != value)
                    {
                        if (value == true && IsPKID == true)
                        {
                            MessageBox.Show(m_parentEditor, "主键不允许可以为空！");
                            return;
                        }

                        m_column.CanNull = value;
                        OnPropertyChanged("CanNull");
                    }
                }
            }



            //
            // 摘要:
            //     caption
            [Category("A字段属性"), DisplayName("2:中文注释"), Description("中文注释，换行后可以备注更多内容"),
                  System.ComponentModel.Editor(typeof(Editor.EnumEditor), typeof(System.Drawing.Design.UITypeEditor))]
            public string caption
            {
                get
                {
                    return m_column.caption;
                }
                set
                {
                    value = value.Trim();
                    if (m_column.caption != value)
                    {
                        m_column.caption = value;
                        if (PropertyChanged != null)
                        {
                            PropertyChanged(this, new PropertyChangedEventArgs("showCaption"));
                            PropertyChanged(this, new PropertyChangedEventArgs("caption"));
                            m_parentEditor.computeColumnAreaWidth();

                        }
                    }
                }
            }
            //
            // 摘要:
            //     数据库字段类型
            [Category("A字段属性"), DisplayName("4:数据库中的类型"),
             System.ComponentModel.TypeConverter(typeof(DbTypeConvert))]
            public string dbType
            {
                get
                {
                    return m_column.dbType;
                }
                set
                {
                    if (m_column.dbType != value)
                    {
                        if (value.Contains("char"))
                        {
                            length = "50";
                        }
                        else if (value.Contains("decimal"))
                        {
                            length = "18,4";
                        }
                        else
                        {
                            length = "";
                        }
                        m_column.dbType = value;
                        OnPropertyChanged("dbType");
                    }
                }
            }

            [Category("A字段属性"), DisplayName("4:jsonb类型"), Description("jsonb对应的类型FullName")]
            public string ClassFullName
            {
                get
                {
                    return m_column.ClassFullName;
                }
                set
                {
                    m_column.ClassFullName = value;
                    OnPropertyChanged("ClassFullName");
                }
            }

            //
            // 摘要:
            //     默认值
            [Category("A字段属性"), DisplayName("5:默认值")]
            public string defaultValue
            {
                get
                {
                    return m_column.defaultValue;
                }
                set
                {
                    if (m_column.defaultValue != value)
                    {
                        m_column.defaultValue = value;
                        OnPropertyChanged("defaultValue");
                    }
                }
            }

            [Category("B继承设置"), DisplayName("1:IsDiscriminator"), Description("是否用来表示数据对应的类，如果设置为True，应该同时设置Enum内容"),
            System.ComponentModel.TypeConverter(typeof(BooleanTypeConvert))]
            public bool? IsDiscriminator
            {
                get
                {
                    return m_column.IsDiscriminator;
                }
                set
                {
                    if (m_column.IsDiscriminator != value)
                    {
                        if (value == true && string.IsNullOrEmpty(EnumDefine))
                        {
                            throw new Exception("请先定义当前列的Enum内容");
                        }
                        if (value == true)
                        {
                            this.CanNull = false;

                            var other = this.m_parentEditor.m_columns.FirstOrDefault(m => m != this && m.IsDiscriminator == true);
                            if (other != null)
                            {
                                throw new Exception("列“" + other.Name + "”已经被设置为IsDiscriminator，不能设置多列");
                            }
                        }

                        m_column.IsDiscriminator = value;

                        if (!string.IsNullOrEmpty(EnumDefine) && value == true)
                        {
                            try
                            {
                                ClassNameTypeConvert.EnumNames = ParseNames(EnumDefine);
                            }
                            catch
                            {
                                ClassNameTypeConvert.EnumNames = null;
                            }
                        }

                        if (PropertyChanged != null)
                            PropertyChanged(this, new PropertyChangedEventArgs("IsDiscriminator"));
                    }
                }
            }


            List<ClassName> ParseNames(string value)
            {
                var ms = Regex.Matches(value, @"(?<name>\w+)[ ]?=(?<value>[ 0-9\w\<\>\(\)\|]+)");
                List<ClassName> names = new List<ClassName>();
                foreach (Match m in ms)
                {
                    names.Add(new ClassName() { Name = m.Groups["name"].Value });
                }
                foreach (Match m in ms)
                {
                    var content = m.Groups["value"].Value;
                    var othernameMatch = Regex.Match(content, @"\w+");
                    if (othernameMatch != null && othernameMatch.Length > 0 && Regex.Match(othernameMatch.Value, @"[0-9]+").Length != othernameMatch.Length)
                    {
                        var othername = othernameMatch.Value;
                        var item = names.FirstOrDefault(c => c.Name == othername);
                        if (item == null)
                        {
                            throw new Exception("无法识别" + othername);
                        }
                        else
                        {
                            var myitem = names.FirstOrDefault(c => c.Name == m.Groups["name"].Value);
                            myitem.BaseName = othername;
                        }
                    }
                }
                return names;
            }

            [Category("A字段属性"), DisplayName("6:Enum"), Description("如果这是个枚举字段，那么在这里设置枚举的内容"),
             System.ComponentModel.Editor(typeof(Editor.EnumEditor), typeof(System.Drawing.Design.UITypeEditor))]
            public string EnumDefine
            {
                get
                {
                    return m_column.EnumDefine;
                }
                set
                {
                    if (m_column.EnumDefine != value)
                    {
                        //解析代码
                        try
                        {
                            var names = ParseNames(value);
                            if (!string.IsNullOrEmpty(EnumDefine) && this.IsDiscriminator == true)
                            {
                                ClassNameTypeConvert.EnumNames = names;
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("枚举内容解析错误，" + ex.Message);
                        }

                        m_column.EnumDefine = value;

                        this.dbType = "int";
                        if (PropertyChanged != null)
                            PropertyChanged(this, new PropertyChangedEventArgs("EnumDefine"));
                    }
                }
            }

            //
            // 摘要:
            //     自增长
            [Category("A字段属性"), DisplayName("3:自增长"), System.ComponentModel.TypeConverter(typeof(BooleanTypeConvert))]
            public bool? IsAutoIncrement
            {
                get
                {
                    return m_column.IsAutoIncrement;
                }
                set
                {
                    if (m_column.IsAutoIncrement != value)
                    {
                        m_column.IsAutoIncrement = value;
                        OnPropertyChanged("IsAutoIncrement");
                    }
                }
            }
            //
            // 摘要:
            //     是否是主键
            [Category("A字段属性"), DisplayName("3:主键"), System.ComponentModel.TypeConverter(typeof(BooleanTypeConvert))]
            public bool? IsPKID
            {
                get
                {
                    return m_column.IsPKID;
                }
                set
                {
                    if (m_column.IsPKID != value)
                    {
                        if (value == true && m_parentEditor.m_columns.Count(m => m != this && m.IsPKID == true) > 0)
                        {
                            MessageBox.Show("一个表只能有一个主键，您可以另外设置唯一值索引");
                            return;
                        }
                        m_column.IsPKID = value;
                        if (value == true)
                        {
                            CanNull = false;
                        }
                        OnPropertyChanged("IsPKID");
                    }
                }
            }

            [Category("B继承设置"), DisplayName("2:所属派生类"),
                System.ComponentModel.TypeConverter(typeof(ClassNameTypeConvert))]
            public string ClassName
            {
                get
                {
                    return m_column.ClassName;
                }
                set
                {
                    if (m_column.ClassName != value)
                    {
                        m_column.ClassName = value;
                        OnPropertyChanged("ClassName");
                        OnPropertyChanged("ShowInLeft");
                        m_parentEditor.computeColumnAreaWidth();

                    }
                }
            }
            //
            // 摘要:
            //     长度
            [Category("A字段属性"), DisplayName("7:Length")]
            public string length
            {
                get
                {
                    return m_column.length;
                }
                set
                {
                    if (m_column.length != value)
                    {
                        m_column.length = value;
                        OnPropertyChanged("length");
                    }
                }
            }


            public event PropertyChangedEventHandler PropertyChanged;
            public void OnPropertyChanged(string name)
            {
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(name));


            }
        }

        #endregion

        System.Windows.Forms.PropertyGrid pgridForTable;
        System.Windows.Forms.PropertyGrid pgridForColumn;
        DatabaseItemNode m_DBNode;
        public EJ.DBTable m_table;
        EJ.DBTable m_modifyingTable;

        bool m_relationChanged = false;
        bool m_IsModify = false;
        System.Collections.ObjectModel.ObservableCollection<CascadingDeletionViewModel> m_deleteConfigs = new System.Collections.ObjectModel.ObservableCollection<CascadingDeletionViewModel>();

        System.Collections.ObjectModel.ObservableCollection<IndexModel> m_IDXConfigs = new System.Collections.ObjectModel.ObservableCollection<IndexModel>();
        System.Collections.ObjectModel.ObservableCollection<ColumnViewModel> m_columns = new System.Collections.ObjectModel.ObservableCollection<ColumnViewModel>();
        System.Collections.ObjectModel.ObservableCollection<MyClassProperty> m_properties = new System.Collections.ObjectModel.ObservableCollection<MyClassProperty>();
        internal DBTableEditor(DatabaseItemNode dbnode, EJ.DBTable currentTable)
        {
            m_DBNode = dbnode;

            InitializeComponent();

            //把目前所有表信息，放入Resources["AllDBTables"]，便于页面绑定
            var 数据表Node = dbnode.Children.FirstOrDefault(m => m is DBTableContainerNode);
            var alldbtables = new System.Collections.ObjectModel.ObservableCollection<EJ.DBTable>();
            foreach (DBTableNode tablenode in 数据表Node.Children)
            {
                alldbtables.Add(tablenode.Table);
            }
            this.Resources["AllDBTables"] = alldbtables;
            this.Resources["DBColumns"] = m_columns;

            pgridForTable = new System.Windows.Forms.PropertyGrid();
            pgridForTable.LineColor = System.Drawing.ColorTranslator.FromHtml("#cccccc");
            hostTablePG.Child = pgridForTable;

            pgridForColumn = new System.Windows.Forms.PropertyGrid();
            pgridForColumn.LineColor = System.Drawing.ColorTranslator.FromHtml("#cccccc");
            hostColumnPG.Child = pgridForColumn;

            listUniqueIndex.ItemsSource = m_IDXConfigs;
            if (currentTable == null)
            {
                m_table = new EJ.DBTable();
                m_table.DatabaseID = dbnode.Database.id;

                ColumnViewModel item = new ColumnViewModel(new EJ.DBColumn()
                {
                    Name = "id",
                    CanNull = false,
                    IsPKID = true,
                    IsAutoIncrement = true,
                    dbType = "bigint",

                }, this);
                m_columns.Add(item);
            }
            else
            {
                this.IsEnabled = false;
                this.Title = "Loading...";
                m_modifyingTable = currentTable;
                loadTableInfos();
            }


            init();

            tabControl.SelectionChanged += TabControl_SelectionChanged;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(tabControl.SelectedItem == tabDtoCode)
            {
                try
                {
                   txtDtoCode.Text = Helper.Client.InvokeSync<string>("GetDtoCode", m_table);
                }
                catch (Exception ex)
                {
                    Helper.ShowError(ex);
                }
            }
        }


        void init()
        {
            var tableNodes = m_DBNode.Children.FirstOrDefault(n => n is DBTableContainerNode).Children;
            Tables =(from m in tableNodes
                     select ((DBTableNode)m).Table.Name
                     ).ToArray();

            pgridForTable.SelectedObject = new TableViewModel(m_table);


            m_deleteConfigs.CollectionChanged += m_deleteConfigs_CollectionChanged;
            listDelConfig.ItemsSource = m_deleteConfigs;
            treeColumns.ItemsSource = m_columns;
           
            listProperties.ItemsSource = m_properties;
        }

        async void loadTableInfos()
        {
            try
            {
                tabControl.SelectedItem = columnTab;

                m_IsModify = true;
                m_table = (EJ.DBTable)Helper.Clone(m_modifyingTable);

                var ret = await Helper.Client.InvokeAsync<TableFullInfo>("GetTableInfo", m_modifyingTable.id.Value);
                foreach (var c in ret.Columns)
                {
                    m_columns.Add(new ColumnViewModel(c, this));
                }

                foreach (var c in ret.ClassProperties)
                {
                    var p = new MyClassProperty(this)
                    {
                        foreignkey_columnid = c.foreignkey_columnid,
                        foreignkey_tableid = c.foreignkey_tableid,
                        id = c.id,
                        iscollection = c.iscollection,
                        name = c.name,
                        desc = c.desc,
                        tableid = c.tableid,
                    };
                    p.ChangedProperties.Clear();
                    m_properties.Add(p);
                }


                foreach (var delitem in ret.DBDeleteConfigs)
                {
                    m_deleteConfigs.Add(new CascadingDeletionViewModel(m_modifyingTable.DatabaseID.Value, delitem.RelaTable_Desc)
                    {
                        ColumnName = delitem.RelaColumn_Desc
                    });
                }

                foreach (var config in ret.IdxIndexes)
                {
                    m_IDXConfigs.Add(new IndexModel(m_columns, config.IsUnique.Value, config.IsClustered.Value)
                    {
                        ColumnNames = config.Keys.Split(',')
                    });
                }

               
                computeColumnAreaWidth();
                this.Title = m_table.Name + " " + m_table.caption;
            }
            catch (Exception ex)
            {
                Helper.ShowError(ex);
            }
            finally
            {
                this.IsEnabled = true;
            }
        }

        void computeColumnAreaWidth()
        {
            FontFamily family = new FontFamily("微软雅黑");
            FontStyle style = new FontStyle();
            FontWeight weight = new FontWeight();
            FontStretch stretch = FontStretches.Normal;
            double fontSize = 12;

            double width = 0;
            foreach (var column in m_columns)
            {
                double itemWidth = Helper.MeasureText(column.ShowInLeft + column.Name + "   " + column.showCaption, family, style, weight, stretch, fontSize).Width + 50;
                width = Math.Max(width, itemWidth);
            }

            foreach (var column in m_columns)
            {
                column.ShowWidth = width;
            }
        }

        void m_deleteConfigs_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            m_relationChanged = true;
        }

        private void btnAddColumn_Click_1(object sender, RoutedEventArgs e)
        {
            int index = 1;
            string columnName = "column" + index;
            while (m_columns.Where(m => m.Name == columnName).Count() > 0)
            {
                index++;
                columnName = "column" + index;
            }
            ColumnViewModel item = new ColumnViewModel(new EJ.DBColumn()
            {
                Name = columnName,
                CanNull = true,
                IsPKID = false,
                IsAutoIncrement = false,
                dbType = "varchar",
                length = "50"
            }, this);

            var currentSelectedItem = m_columns.FirstOrDefault(m => m.IsSelected);
            if (currentSelectedItem != null)
            {
                m_columns.Insert(m_columns.IndexOf(currentSelectedItem) + 1, item);
            }
            else
            {
                m_columns.Add(item);
            }
            item.IsSelected = true;
            this.computeColumnAreaWidth();
        }

        private void treeColumns_SelectedItemChanged_1(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            pgridForColumn.SelectedObject = treeColumns.SelectedItem;
        }

        private void btnOK_Click_1(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            e.Handled = true;
            if (string.IsNullOrEmpty(m_table.Name))
            {
                this.Cursor = null;
                MessageBox.Show(this, "表名不能为空");
                return;
            }
            for (int i = 0; i < m_columns.Count; i++)
            {

                if (m_columns[i].EnumDefine.IsNullOrEmpty() == false && m_columns[i].dbType != "int")
                {
                    this.Cursor = null;
                    MessageBox.Show(this, string.Format("{0}({1})定义为枚举类型，所以必须是int类型", m_columns[i].Name, m_columns[i].caption));
                    return;
                }

                if (m_columns[i].dbType == "jsonb" && string.IsNullOrWhiteSpace(m_columns[i].ClassFullName))
                {
                    MessageBox.Show(this, "jsonb类型不能为空");
                    return;
                }
            }

            List<object> idsConfigs = new List<object>();
            foreach (IndexModel c in m_IDXConfigs)
            {
                //如果索引包含已经删除的字段，则忽略
                if (c.ColumnNames.Any(m => m_columns.Any(o => o.Name == m) == false))
                {
                    this.Cursor = null;
                    MessageBox.Show(this, $"索引包含不存在的字段{c.ColumnNames.FirstOrDefault(m => m_columns.Any(o => o.Name == m) == false)}");
                    return;
                }

                if (c.ColumnNames.Length > 0)
                {
                    idsConfigs.Add(new
                    {
                        c.IsUnique,
                        c.IsClustered,
                        c.ColumnNames,
                    });
                }
            }

            List<EJ.DBDeleteConfig> delconfigs = new List<EJ.DBDeleteConfig>();
            foreach (var delitem in m_deleteConfigs)
            {
                if (string.IsNullOrEmpty(delitem.TableName) || string.IsNullOrEmpty(delitem.ColumnName))
                    continue;
                if (delconfigs.Count(m => m.RelaTable_Desc == delitem.TableName && m.RelaColumn_Desc == delitem.ColumnName) > 0)
                    continue;
                delconfigs.Add(new EJ.DBDeleteConfig()
                {
                    RelaTable_Desc = delitem.TableName,
                    RelaColumn_Desc = delitem.ColumnName,
                });
            }

            try
            {
                EJ.DBColumn[] columns = new EJ.DBColumn[m_columns.Count];
                for (int i = 0; i < m_columns.Count; i++)
                {
                    columns[i] = m_columns[i].m_column;
                    m_columns[i].m_column.orderid = i;
                }

                if (m_IsModify)
                {
                    var propertyItems = m_properties.Where(m => m.name.IsNullOrEmpty() == false).ToArray().ToJsonString().ToJsonObject<EJ.classproperty[]>();
                    Helper.Client.InvokeSync<string>("ModifyTable", m_table, columns, delconfigs, idsConfigs, propertyItems);
                    m_modifyingTable.Name = m_table.Name;
                    m_modifyingTable.caption = m_table.caption;

                    foreach (UI.Document doc in MainWindow.instance.documentContainer.Items)
                    {
                        if (doc is UI.ModuleDocument)
                        {
                            var ui = ((UI.ModuleDocument)doc).getTableById(this.m_table.id.GetValueOrDefault());
                            if (ui != null)
                            {
                                ui.DataSource = new UI.Table._DataSource()
                                {
                                    Table = m_table,
                                    Columns = Helper.Client.InvokeSync<EJ.DBColumn[]>("GetColumns", m_table.id.GetValueOrDefault()),
                                };
                                ui.DataBind();
                            }
                        }
                    }
                }
                else
                {
                    m_table = Helper.Client.InvokeSync<EJ.DBTable>("CreateTable", m_table, columns, delconfigs, idsConfigs, m_properties.Where(m => m.name.IsNullOrEmpty() == false).ToArray());
                    //ChangedProperties本来是0，但ToJsonObject转换时，每个属性都进行set操作，又产生了ChangedProperties
                    m_table.ChangedProperties.Clear();

                    DBTableContainerNode parent = (DBTableContainerNode)m_DBNode.Children.FirstOrDefault(m => m is DBTableContainerNode);
                    parent.Children.Insert(0, new DBTableNode(m_table, parent));
                    this.DialogResult = true;
                }

                this.Close();
                if (m_relationChanged)
                {
                    foreach (UI.Document doc in MainWindow.instance.documentContainer.Items)
                    {
                        if (doc is UI.ModuleDocument)
                        {
                            ((UI.ModuleDocument)doc).TableChangeRelation(this.m_table.id.GetValueOrDefault());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.GetBaseException().Message);
                return;
            }
            finally
            {
                this.Cursor = null;
            }
        }

        private void btnDeleteColumn_Click_1(object sender, RoutedEventArgs e)
        {
            ColumnViewModel column = treeColumns.SelectedItem as ColumnViewModel;
            if (column == null)
                return;
            int index = m_columns.IndexOf(column);
            m_columns.Remove(column);
            var idx = m_IDXConfigs.FirstOrDefault(m => m.ColumnNames.Contains(column.Name));
            if (idx != null)
            {
                m_IDXConfigs.Remove(idx);
            }
            try
            {
                m_columns[index].IsSelected = true;
            }
            catch
            {
            }
        }
        private void RelaColumnNameChanged(object sender, SelectionChangedEventArgs e)
        {
            m_relationChanged = true;
        }
        private void RelaTableNameChanged(object sender, SelectionChangedEventArgs e)
        {
            m_relationChanged = true;
            ComboBox selTableName = (ComboBox)sender;
            ((CascadingDeletionViewModel)selTableName.Tag).reBindColumns();
        }

        private void addDeleteRela_Click_1(object sender, RoutedEventArgs e)
        {
            m_deleteConfigs.Add(new CascadingDeletionViewModel(m_table.DatabaseID.Value, null));
        }

        private void delDeleteRela_Click_1(object sender, RoutedEventArgs e)
        {
            if (listDelConfig.SelectedItem != null)
            {
                m_deleteConfigs.Remove((CascadingDeletionViewModel)listDelConfig.SelectedItem);
            }
        }
        private void addProperty_Click(object sender, RoutedEventArgs e)
        {
            m_properties.Add(new MyClassProperty(this));
        }

        private void delProperty_Click(object sender, RoutedEventArgs e)
        {
            if (listProperties.SelectedItem != null)
            {
                m_properties.Remove((MyClassProperty)listProperties.SelectedItem);
            }
        }

        private void btnMoveUp_Click_1(object sender, RoutedEventArgs e)
        {
            ColumnViewModel column = treeColumns.SelectedItem as ColumnViewModel;
            if (column == null || column == m_columns[0])
                return;

            m_relationChanged = true;//位置改变，也需要重画线
            int index = m_columns.IndexOf(column);
            m_columns.RemoveAt(index);
            m_columns.Insert(index - 1, column);
            column.IsSelected = true;
        }

        private void btnMoveDown_Click_1(object sender, RoutedEventArgs e)
        {
            ColumnViewModel column = treeColumns.SelectedItem as ColumnViewModel;
            if (column == null || column == m_columns.Last())
                return;

            m_relationChanged = true;//位置改变，也需要重画线
            int index = m_columns.IndexOf(column);
            m_columns.RemoveAt(index);
            m_columns.Insert(index + 1, column);
            column.IsSelected = true;
        }

        private void addUniqueIndex_Click_1(object sender, RoutedEventArgs e)
        {
            m_IDXConfigs.Add(new IndexModel(this.m_columns, false, false)
            {
                ColumnNames = new string[0],
            });
        }

        private void delUniqueIndex_Click_1(object sender, RoutedEventArgs e)
        {
            IndexModel item = listUniqueIndex.SelectedItem as IndexModel;
            if (item != null)
            {
                m_IDXConfigs.Remove(item);
            }
        }

        static string CopyedColumns = null;
        private void btnCopy_Click_1(object sender, RoutedEventArgs e)
        {
            List<EJ.DBColumn> copyColumns = new List<EJ.DBColumn>();
            foreach (var column in m_columns)
            {
                if (column.IsChecked)
                {
                    copyColumns.Add(column.m_column);
                }
            }
            CopyedColumns = copyColumns.ToJsonString();
        }

        private void btnPaste_Click_1(object sender, RoutedEventArgs e)
        {
            if (CopyedColumns != null)
            {
                EJ.DBColumn[] sourceColumns = CopyedColumns.ToJsonObject<EJ.DBColumn[]>();
                for (int i = 0; i < sourceColumns.Length; i++)
                {
                    if (m_columns.Where(m => m.Name == sourceColumns[i].Name).Count() > 0)
                        continue;
                    sourceColumns[i].id = null;

                    ColumnViewModel item = new ColumnViewModel(sourceColumns[i], this);
                    m_columns.Add(item);
                }
                this.computeColumnAreaWidth();
            }
        }

    }

    internal class UniqueIndexSelector : StackPanel
    {
        public static readonly DependencyProperty DataSourceProperty = DependencyProperty.Register("DataSource", typeof(object), typeof(UniqueIndexSelector),
            new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnDataSourceChanged)));

        object _DataSource;
        public object DataSource
        {
            get
            {
                return _DataSource;
            }
            set
            {
                _DataSource = value;
                this.Children.Clear();
                if (value != null)
                {
                    EJClient.Forms.DBTableEditor.IndexModel dataContext = (EJClient.Forms.DBTableEditor.IndexModel)this.DataContext;
                    if (true)
                    {
                        ComboBox comboBox = new ComboBox();
                        comboBox.Items.Add("唯一值");
                        comboBox.Items.Add("非唯一值");
                        comboBox.SelectedIndex = dataContext.IsUnique ? 0 : 1;
                        comboBox.Margin = new Thickness(3);
                        comboBox.SelectionChanged += Unique_SelectionChanged;
                        this.Children.Add(comboBox);
                    }
                    if (true)
                    {
                        ComboBox comboBox = new ComboBox();
                        comboBox.Items.Add("非聚焦");
                        comboBox.Items.Add("聚焦");
                        comboBox.SelectedIndex = dataContext.IsClustered ? 1 : 0;
                        comboBox.SelectionChanged += focus_SelectionChanged;
                        comboBox.Margin = new Thickness(3);
                        this.Children.Add(comboBox);
                    }
                    foreach (var column in (string[])value)
                    {
                        ComboBox comboBox = new ComboBox();
                        comboBox.Items.Add(column.ToString());
                        comboBox.SelectedItem = column;
                        this.Children.Add(comboBox);
                        comboBox.DropDownOpened += comboBox_DropDownOpened;
                        comboBox.SelectionChanged += comboBox_SelectionChanged;


                        comboBox.Margin = new Thickness(3);

                    }
                    if (true)
                    {
                        ComboBox comboBox = new ComboBox();
                        comboBox.DropDownOpened += comboBox_DropDownOpened;
                        comboBox.SelectionChanged += comboBox_SelectionChanged;
                        comboBox.Margin = new Thickness(3);
                        this.Children.Add(comboBox);
                    }
                }
            }
        }
        void Unique_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EJClient.Forms.DBTableEditor.IndexModel dataContext = (EJClient.Forms.DBTableEditor.IndexModel)this.DataContext;
            ComboBox comboBox = sender as ComboBox;
            dataContext.IsUnique = comboBox.SelectedIndex == 0;
        }
        void focus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EJClient.Forms.DBTableEditor.IndexModel dataContext = (EJClient.Forms.DBTableEditor.IndexModel)this.DataContext;
            ComboBox comboBox = sender as ComboBox;
            dataContext.IsClustered = comboBox.SelectedIndex == 1;
        }
        void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox.SelectedItem == null)
                return;
            if (comboBox.SelectedItem.Equals(""))
            {
                if (comboBox != this.Children[this.Children.Count - 1])
                    this.Children.Remove(comboBox);
            }
            else
            {
                int index = this.Children.IndexOf(comboBox);
                if (this.Children.Count - 1 == index)
                {
                    //添加一个combobox
                    comboBox = new ComboBox();
                    comboBox.DropDownOpened += comboBox_DropDownOpened;
                    comboBox.SelectionChanged += comboBox_SelectionChanged;
                    comboBox.Margin = new Thickness(3);
                    this.Children.Add(comboBox);
                }
            }

            EJClient.Forms.DBTableEditor.IndexModel dataContext = (EJClient.Forms.DBTableEditor.IndexModel)this.DataContext;
            List<string> columns = new List<string>();
            for (int i = 2; i < this.Children.Count; i++)
            {
                ComboBox selector = (ComboBox)this.Children[i];
                string name = Convert.ToString(selector.SelectedItem);
                if (!string.IsNullOrEmpty(name) && columns.Contains(name) == false)
                    columns.Add(name);
            }
            dataContext.ColumnNames = columns.ToArray();
        }

        void comboBox_DropDownOpened(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            EJClient.Forms.DBTableEditor.IndexModel dataContext = (EJClient.Forms.DBTableEditor.IndexModel)this.DataContext;
            var oldselected = comboBox.SelectedItem;
            comboBox.Items.Clear();
            comboBox.Items.Add("");
            foreach (EJClient.Forms.DBTableEditor.ColumnViewModel column in dataContext.Columns)
            {
                comboBox.Items.Add(column.m_column.Name);
            }

            comboBox.SelectedItem = oldselected;
        }

        private static void OnDataSourceChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ((UniqueIndexSelector)sender).DataSource = e.NewValue;
        }


    }

    class TableFullInfo
    {
        public DBColumn[] Columns;
        public classproperty[] ClassProperties;
        public DBDeleteConfig[] DBDeleteConfigs;
        public IDXIndex[] IdxIndexes;
    }
}
