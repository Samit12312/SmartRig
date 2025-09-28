using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class MotherBoard
    {
        int motherBoardId;
        string motherBoardName;
        int motherBoardPrice;
        int motherBoardCompanyId;

        public int MotherBoardId
        {
            get { return this.motherBoardId; }
            set { this.motherBoardId = value; }
        }

        public string MotherBoardName
        {
            get { return this.motherBoardName; }
            set { this.motherBoardName = value; }
        }

        public int MotherBoardPrice
        {
            get { return this.motherBoardPrice; }
            set { this.motherBoardPrice = value; }
        }

        public int MotherBoardCompanyId
        {
            get { return this.motherBoardCompanyId; }
            set { this.motherBoardCompanyId = value; }
        }



    }
}
