using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Search
{
    class clsSearchSQL
    {
        public string SelectAll()
        {
            string sSQL = "SELECT * FROM Invoices";
            return sSQL;
        }

        //All selects that start with ID (4)
        public string SelectInvoiceID(string sInvoiceID)
        {
            string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + sInvoiceID;
            return sSQL;
        }

        public string SelectIDDate(string sInvoiceID, DateTime dtDateTime)
        {
            string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + sInvoiceID + " AND InvoiceDate = #" + dtDateTime + "#";
            return sSQL;
        }

        public string SelectIDCost(string sInvoiceID, string sCost)
        {
            string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + sInvoiceID + " AND TotalCost = " + sCost;
            return sSQL;
        }

        public string SelectIDDateCost(string sInvoiceID, DateTime dtDateTime, string sCost)
        {
            string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + sInvoiceID + " AND InvoiceDate = #" + dtDateTime + "#" + " AND TotalCost = " + sCost;
            return sSQL;
        }

        //All selects that start with Date
        public string SelectInvoiceDate(DateTime dtDateTime)
        {
            string sSQL = "SELECT * FROM Invoices WHERE InvoiceDate = #" + dtDateTime + "#";
            return sSQL;
        }

        public string SelectDateCost(DateTime dtDateTime, string sCost)
        {
            string sSQL = "SELECT * FROM Invoices WHERE InvoiceDate = #" + dtDateTime + "# AND TotalCost = " + sCost;
            return sSQL;
        }

        //Last select that starts with Cost
        public string SelectInvoiceCost(string sCost)
        {
            string sSQL = "SELECT * FROM Invoices WHERE TotalCost = " + sCost;
            return sSQL;
        }

    }
}
