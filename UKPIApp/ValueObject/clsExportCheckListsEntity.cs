using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace UKPI.ValueObject

{   
    public class clsExportCheckListsEntity
    {
        private DataTable m_dtStores;
        private DataTable m_dtOSA;
        private DataTable m_dtNPD;
        private DataTable m_dtShelfStandard;
        private DataTable m_dtShelfStandard_MT;
        private DataTable m_dtPromotionCompliance;
        private DataTable m_dtSOS;
        private DataTable m_dtStandardPrice;
        private DataTable m_dtSOF;

        public DataTable DtSOF
        {
            get { return m_dtSOF; }
            set { m_dtSOF = value; }
        }

        public DataTable DtStandardPrice
        {
            get { return m_dtStandardPrice; }
            set { m_dtStandardPrice = value; }
        }

        public DataTable DtSOS
        {
            get { return m_dtSOS; }
            set { m_dtSOS = value; }
        }

        public DataTable DtPromotionCompliance
        {
            get { return m_dtPromotionCompliance; }
            set { m_dtPromotionCompliance = value; }
        }

        public DataTable DtShelfStandard
        {
            get { return m_dtShelfStandard; }
            set { m_dtShelfStandard = value; }
        }


        public DataTable DtShelfStandard_MT
        {
            get { return m_dtShelfStandard_MT; }
            set { m_dtShelfStandard_MT = value; }
        }

        public DataTable DtNPD
        {
            get { return m_dtNPD; }
            set { m_dtNPD = value; }
        }

        public DataTable DtOSA
        {
            get { return m_dtOSA; }
            set { m_dtOSA = value; }
        }

        public DataTable DtStores
        {
            get { return m_dtStores; }
            set { m_dtStores = value; }
        }
    }
}
