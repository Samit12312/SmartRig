using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Case
    {
        int caseId;
        string caseName;
        int casePrice;
        int caseCompanyId;

        public int CaseId
        {
            get { return this.caseId; }
            set { this.caseId = value; }
        }

        public string CaseName
        {
            get { return this.caseName; }
            set { this.caseName = value; }
        }

        public int CasePrice
        {
            get { return this.casePrice; }
            set { this.casePrice = value; }
        }

        public int CaseCompanyId
        {
            get { return this.caseCompanyId; }
            set { this.caseCompanyId = value; }
        }



    }
}
