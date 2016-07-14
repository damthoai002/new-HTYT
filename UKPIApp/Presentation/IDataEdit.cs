using System;
using System.Data;

namespace UKPI.Presentation
{
	/// <summary>
	/// Summary description for IDataEdit.
	/// </summary>
	public interface IDataEdit
	{
		void Insert(DataRow row);
		void Update(DataRow row);
		void Delete(DataRow row);
		void UpdateAll(DataTable dt);
		bool ValidateData();
	}
}
