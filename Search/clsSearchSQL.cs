using System;

namespace GroupProject.Search
{
    class clsSearchSQL
    {
        /// <summary>
        /// This SQL gets all the invoices 
        /// </summary>
        /// <returns>string of SQL statement for all invoices</returns>
        public string SelectAll()
        {
            string sSQL = "SELECT * FROM Invoices";
            return sSQL;
        }

        //All selects that start with ID (4)
        /// <summary>
        /// This SQL gets all data on an invoice based on InvoiceID
        /// </summary>
        /// <param name="sInvoiceID">Data for invoice</param>
        /// <returns>returns the sql Statment string</returns>
        public string SelectInvoiceID(string sInvoiceID)
        {
            string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + sInvoiceID;
            return sSQL;
        }

        /// <summary>
        /// This SQL gets all data on an invoice based on Invoice ID and Date
        /// </summary>
        /// <param name="sInvoiceID">Data for ID</param>
        /// <param name="dtDateTime">Data for date</param>
        /// <returns>returns the sql statement string</returns>
        public string SelectIDDate(string sInvoiceID, DateTime dtDateTime)
        {
            string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + sInvoiceID + " AND InvoiceDate = #" + dtDateTime + "#";
            return sSQL;
        }

        /// <summary>
        /// this sql gets all data on an invoice based on Invoice ID and cost
        /// </summary>
        /// <param name="sInvoiceID">data for id</param>
        /// <param name="sCost">data for cost</param>
        /// <returns>returns the sql statement string</returns>
        public string SelectIDCost(string sInvoiceID, string sCost)
        {
            string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + sInvoiceID + " AND TotalCost = " + sCost;
            return sSQL;
        }

        /// <summary>
        /// this sql gets all data on an invoice based on invoice id, date, and cost
        /// </summary>
        /// <param name="sInvoiceID">data for ID</param>
        /// <param name="dtDateTime">data for Date</param>
        /// <param name="sCost">data for Cost</param>
        /// <returns>returns the sql statement string</returns>
        public string SelectIDDateCost(string sInvoiceID, DateTime dtDateTime, string sCost)
        {
            string sSQL = "SELECT * FROM Invoices WHERE InvoiceNum = " + sInvoiceID + " AND InvoiceDate = #" + dtDateTime + "#" + " AND TotalCost = " + sCost;
            return sSQL;
        }

        //All selects that start with Date
        /// <summary>
        /// this sql gets all data on an invoice based on date
        /// </summary>
        /// <param name="dtDateTime">data for Date</param>
        /// <returns>returns the sql statement string</returns>
        public string SelectInvoiceDate(DateTime dtDateTime)
        {
            string sSQL = "SELECT * FROM Invoices WHERE InvoiceDate = #" + dtDateTime + "#";
            return sSQL;
        }

        /// <summary>
        /// this sql gets all data on an invoice based on date and cost
        /// </summary>
        /// <param name="dtDateTime">data for date</param>
        /// <param name="sCost">data for cost</param>
        /// <returns>returns the sql statement</returns>
        public string SelectDateCost(DateTime dtDateTime, string sCost)
        {
            string sSQL = "SELECT * FROM Invoices WHERE InvoiceDate = #" + dtDateTime + "# AND TotalCost = " + sCost;
            return sSQL;
        }

        //Last select that starts with Cost
        /// <summary>
        /// this sql gets all data on an invoice based on date and cost
        /// </summary>
        /// <param name="sCost">data for cost</param>
        /// <returns>returns the sql statement</returns>
        public string SelectInvoiceCost(string sCost)
        {
            string sSQL = "SELECT * FROM Invoices WHERE TotalCost = " + sCost;
            return sSQL;
        }

    }
}
