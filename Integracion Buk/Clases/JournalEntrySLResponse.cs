using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integracion_Buk.Clases
{
    public class JournalEntrySLResponse
    {
        public string Reference2 { get; set; }
        public string TransactionCode { get; set; }
        public string ProjectCode { get; set; }
        public string TaxDate { get; set; }
        public int JdtNum { get; set; }
        public string Indicator { get; set; }
    }
}
