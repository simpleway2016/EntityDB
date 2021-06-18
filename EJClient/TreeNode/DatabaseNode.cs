using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EJClient.TreeNode
{
    class DatabaseNode : TreeNodeBase
    {
        public override string Icon
        {
            get
            {
                return "imgs/db.png";
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
                    _ContextMenu = (ContextMenu)MainWindow.instance.Resources["treeMenu_Database"];
                }
                return _ContextMenu;
            }
            set
            {
                _ContextMenu = value;
            }
        }
        public DatabaseNode(ProjectNode parent)
            : base(parent)
        {

        }

        bool _binded = false;
        public override void ReBindItems()
        {
            this.Children.Add(new TreeNodeBase(this)
            {
                Name = "Loading..."
            });
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
                ProjectNode parentNode = this.Parent as ProjectNode;
                var mydatabases = await Helper.Client.InvokeAsync<EJ.Databases[]>("GetDatabaseList", parentNode.Project.id);
                this.Children.Clear();
                _binded = true;

                foreach (var dbitem in mydatabases)
                {
                    DatabaseItemNode dbnode = this.Children.Where(m => ((DatabaseItemNode)m).Database.id == dbitem.id).FirstOrDefault() as DatabaseItemNode;
                    if (dbnode == null)
                    {
                        dbnode = new DatabaseItemNode(this, dbitem);
                        this.Children.Add(dbnode);
                    }
                    else
                    {
                        dbnode.Database = dbitem;
                    }
                }

                for (int i = 0; i < Children.Count; i++)
                {
                    if (mydatabases.Where(m => m.id == ((DatabaseItemNode)Children[i]).Database.id).Count() == 0)
                    {
                        //删除
                        this.Children.RemoveAt(i);
                        i--;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


    }
}
