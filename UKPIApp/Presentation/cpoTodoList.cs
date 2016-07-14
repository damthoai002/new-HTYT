using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using log4net;
using UKPI.BusinessObject;
using UKPI.Utils;

namespace UKPI.Presentation
{
    public partial class cpoTodoList : UserControl
    {
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(cpoTodoList));

        ToDoListBO BO;
        string[] strDescription = {clsResources.GetMessage("TodoList.Description.SendDisplayStructure"),
                                   clsResources.GetMessage("TodoList.Description.SendRegisterlistToTPC"),
                                   clsResources.GetMessage("TodoList.Description.SendQualifiedToDT"),
                                   clsResources.GetMessage("TodoList.Description.SendQualifiedToNuti")};

        public cpoTodoList()
        {
            InitializeComponent();
            BO = new ToDoListBO();
        }

        /// <summary>
        /// Reload Form
        /// </summary>
        /// <param name="isReload">
        /// True - Timer will not be reloaded
        /// False - Timer will be reloaded
        /// </param>
        private void LoadToDo()
        {
            DataTable dtEvents = null;

            TreeNode TodoNode;

            try
            {
                dtEvents = BO.LoadTodoList();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
            }

            if (trvTodoList.Nodes.Count != 0)
            {
                trvTodoList.Nodes.Clear();
            }

            if (dtEvents != null && dtEvents.Rows.Count != 0)
            {
                foreach (DataRow row in dtEvents.Rows)
                {
                    TodoNode = new TreeNode(strDescription[Convert.ToInt32(row[0].ToString().Trim())]);
                    AddInfo(row,ref TodoNode);
                    trvTodoList.Nodes.Add(TodoNode);
                    TodoNode = null;// Disapose Node
                }
				trvTodoList.Nodes[0].Expand();
            }
        }

        /// <summary>
        /// Add Todo's infor
        /// </summary>
        /// <param name="row">Information row</param>
        /// <param name="root">Add to node</param
        private void AddInfo(DataRow row,ref TreeNode root)
        {
            if(row.ItemArray.Length != 6){
                return;
            }

            DateTime time = Convert.ToDateTime(row[1].ToString().Trim());
            root.Nodes.Add(string.Format("DeadLine: {0}",time.ToString("dd/MM/yyyy").Trim()));
            root.Nodes.Add(string.Format("Display Month: {0}/{1}",row[3],row[2]));
            root.Nodes.Add(string.Format("Program Code: {0}", row[4]));
            root.Nodes.Add(string.Format("Program Type: {0}", row[5]));
        }

        private void cpoTodoList_Load(object sender, EventArgs e)
        {
            trvTodoList.Nodes.Clear();
            LoadToDo();
        }

        private void cpoTodoList_Leave(object sender, EventArgs e)
        {
           
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (!e.Node.IsExpanded)
            {
                e.Node.Expand();
            }
        }

        public void Reload()
        {
            trvTodoList.Nodes.Clear();
            LoadToDo();
        }
    }
}
