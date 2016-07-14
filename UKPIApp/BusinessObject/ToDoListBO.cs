using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using UKPI.DataAccessObject;

namespace UKPI.BusinessObject
{
    class ToDoListBO
    {
        clsParameterDAO DAO = new clsParameterDAO();

        public DataTable LoadTodoList()
        {
            return DAO.SelectTodoList(); 
        }

        public int GetTimer()
        {
            return DAO.SelectTimer();
        }
    }
}
