using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EJClient.TreeNode
{
    class DBTableContainerNode : TreeNodeBase
    {
        public override string Icon
        {
            get
            {
                return "imgs/table.png";
            }
            set
            {
            }
        }
        ContextMenu _ContextMenu;
        public override ContextMenu ContextMenu
        {
            get
            {
                if (_ContextMenu == null)
                {
                    _ContextMenu = (ContextMenu)MainWindow.instance.Resources["treeMenu_Tables"];
                }
                return _ContextMenu;
            }
            set
            {
                _ContextMenu = value;
            }
        }
        DatabaseItemNode DatabaseItemNode;
        public DBTableContainerNode(DatabaseItemNode parent)
            : base(parent)
        {
            DatabaseItemNode = parent;
        }

        bool _binded = false;
        public override void ReBindItems()
        {
            if (this.Children.Count == 0)
            {
                this.Children.Add(new TreeNodeBase(this)
                {
                    Name = "Loading..."
                });
                _binded = false;
                this.OnExpandChanged(false);
            }
            else
            {
                _binded = false;
                this.Children.Clear();
                this.Children.Add(new TreeNodeBase(this)
                {
                    Name = "Loading..."
                });
                this.OnExpandChanged(true);
            }

           
        }

        public override void OnSelectChanged(bool select)
        {
            OnExpandChanged(true);
        }

        public override async void OnExpandChanged(bool expanded)
        {
            if (_binded)
                return;
            try
            {
                _binded = true;
                EJ.DBTable[] tables = await Helper.Client.InvokeAsync<EJ.DBTable[]>("GetTableList", this.DatabaseItemNode.Database.id.Value);
                this.Children.Clear();
              

                //检查已经删除的
                for (int i = 0; i < this.Children.Count; i++)
                {
                    DBTableNode node = this.Children[i] as DBTableNode;
                    if (node != null)
                    {
                        if (tables.Count(m => m.id == node.Table.id) == 0)
                        {
                            //已经删除
                            this.Children.RemoveAt(i);
                            i--;
                        }
                    }
                }
                foreach (EJ.DBTable t in tables)
                {
                    if (this.Children.Count(m => ((DBTableNode)m).Table.id == t.id) == 0)
                    {
                        //有新增的表
                        DBTableNode node = new DBTableNode(t, this);
                        this.Children.Add(node);
                    }
                    else
                    {
                        DBTableNode node = (DBTableNode)this.Children.FirstOrDefault(m => ((DBTableNode)m).Table.id == t.id);
                        node.Table.Name = t.Name;
                        node.Table.caption = t.caption;
                        node.Name = string.Format("{0} {1}", t.Name, t.caption);
                    }
                }
            }
            catch (Exception ex)
            {
                _binded = false;
                MessageBox.Show(ex.Message);
            }
           
        }
    }
}
