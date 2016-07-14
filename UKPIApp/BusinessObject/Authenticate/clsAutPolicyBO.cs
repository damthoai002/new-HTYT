using System;
using System.Data;
using System.Collections;
using UKPI.DataAccessObject;

namespace UKPI.BusinessObject
{
	/// <summary>
	/// Summary description for clsAutPolicyBO.
	/// </summary>
	/// <remarks>
	/// Author:			Nguyen Minh Duc. G3.
	/// Created date:	14/05/2006
	/// </remarks>
	public class clsAutPolicyBO:clsBaseBO
	{
		private clsAutPolicyDAO dao = new clsAutPolicyDAO();
		public clsAutPolicyBO()
		{
		}

		/// <summary>
		/// Get Policy of one user
		/// </summary>
		/// <param name="URoleID"></param>
		/// <returns></returns>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public DataTable GetPolicy(string URoleID)
		{
			return dao.GetPolicy(URoleID);
		}

		/// <summary>
		/// Update all feature of one role by RoleID
		/// </summary>
		/// <param name="URoleID"></param>
		/// <param name="added"></param>
		/// <param name="deleted"></param>
		/// <returns></returns>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public int UpdateAll(string URoleID, ArrayList added, ArrayList deleted)
		{
			return dao.UpdateAll(URoleID, added, deleted);
		}
	}
}
