using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using UKPI.DataAccessObject;
using UKPI.Utils;
using UKPI.ValueObject;

namespace UKPI.BusinessObject
{
    public class TeamBo
    {
        private TeamDao _teamDao = new TeamDao();
        private clsCommon _common = new clsCommon();

        public DataTable GetAllTeams()
        {
            return _teamDao.GetAllTeams();
        }

        public DataTable SearchTeam(string ten, string ho, string nhom)
        {
            return _teamDao.SearchTeam(ten, ho, nhom);
        }


        public void UnActiveTeam(List<ClsTeam> teams, string strTeamId, string userid)
        {
            _teamDao.DoUnActiveTeam(teams, strTeamId, userid);

        }

        public void CreateTeam(ClsTeam team, string userName)
        {
            _teamDao.CreateTeam(team, userName);
        }
        public DataTable GetTruongNhomForTeam()
        {
            return _teamDao.GetTruongNhomForTeam();
        }

      
    }
}
