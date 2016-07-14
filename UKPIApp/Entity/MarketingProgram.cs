using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UKPI.Entity
{
    public class MarketingProgram : BaseEntity
    {

        private List<DisplaySet> displaySetCollection;
        private List<DisplaySetShopFormatRelation> displaySetShopFormatRelations;
        private List<BasicExtraSetRelation> basicExtraSetRelations;

        public string ProgramCode { get; set; }
        public ProgramType ProgramType { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public int MaxRegPerBasicSet { get; set; }
        public int MaxRegPerExtraSet { get; set; }
        public int MaxBasicSet { get; set; }
        public int MaxExtraSet { get; set; }
        public ModifiedStatus Modified { get; set; }
        public int IsSent { get; set; }

        public List<DisplaySet> DisplaySetCollection
        {
            get
            {
                return displaySetCollection;
            }
        }

        public List<DisplaySetShopFormatRelation> DisplaySetShopFormatRelations
        {
            get
            {
                return displaySetShopFormatRelations;
            }
        }

        public List<BasicExtraSetRelation> BasicExtraSetRelations
        {
            get
            {
                return basicExtraSetRelations;
            }
        }

        public MarketingProgram()
            : base()
        {
            Year = 0;
            Month = 0;
            ProgramCode = string.Empty;
            ProgramType = new ProgramType();
            MaxBasicSet = 0;
            MaxExtraSet = 0;
            MaxRegPerBasicSet = 0;
            MaxRegPerExtraSet = 0;
            Modified = ModifiedStatus.ADD;
            IsSent = 0;
            displaySetCollection = new List<DisplaySet>();
            displaySetShopFormatRelations = new List<DisplaySetShopFormatRelation>();
            basicExtraSetRelations = new List<BasicExtraSetRelation>();
        }

        public MarketingProgram(IEnumerable<DisplaySet> displaySets, IEnumerable<DisplaySetShopFormatRelation> shopFormatRelations, IEnumerable<BasicExtraSetRelation> extraSetRelations)
            : base()
        {
            Year = 0;
            Month = 0;
            ProgramCode = string.Empty;
            ProgramType = new ProgramType();
            MaxBasicSet = 0;
            MaxExtraSet = 0;
            MaxRegPerBasicSet = 0;
            MaxRegPerExtraSet = 0;
            Modified = ModifiedStatus.ADD;
            IsSent = 0;
            displaySetCollection = new List<DisplaySet>(displaySets);
            displaySetShopFormatRelations = new List<DisplaySetShopFormatRelation>(shopFormatRelations);
            basicExtraSetRelations = new List<BasicExtraSetRelation>(extraSetRelations);
        }
    }
}
