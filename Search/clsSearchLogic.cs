using GroupProject.Models;
using GroupProject.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows.Controls;

namespace GroupProject.Search
{
    class clsSearchLogic
    {
        public ObservableCollection<Invoice> Invoices { get; set; }
        private readonly InvoiceRepository _invoiceRepository;
        public clsSearchLogic()
        {
            _invoiceRepository = new InvoiceRepository();
            var invoices = _invoiceRepository.GetAllInvoices();
            Invoices = new ObservableCollection<Invoice>(invoices);
        }
        //This class will have a local variable for InvoiceID that will be sent over to the other windows with a function like a setter.
        //private int iInvoiceID;


        /// <summary>
        /// Function that runs whenever a ComboBox's selection is changed. Checks which ones are null, gets the proper sql string,
        /// executes data, and sends back a list of Invoices.
        /// </summary>
        /// <param name="cbTotalCharge">ComboBox of the Total Charge</param>
        /// <param name="cbInvoiceDate">ComboBox of the Invoice Date</param>
        /// <param name="cbInvoiceNum">ComboBox of the Invoice ID</param>
        /// <returns>returns a list of Invoices</returns>
        public List<Invoice> getUpdatedData(ComboBox cbTotalCharge, ComboBox cbInvoiceDate, ComboBox cbInvoiceNum)
        {
            clsDataAccess db = new clsDataAccess();
            clsSearchSQL sql = new clsSearchSQL();
            DataSet ds = new DataSet();

            bool isChargeNull = cbTotalCharge.SelectedItem == null;
            bool isDateNull = cbInvoiceDate.SelectedItem == null;
            bool isNumNull = cbInvoiceNum.SelectedItem == null;

            string sCharge = "";
            DateTime dDate = new DateTime();
            string sID = "";

            //used to set the variables to the selection if that selection isn't null.
            if (!isChargeNull)
            {
                sCharge = cbTotalCharge.SelectedItem.ToString();
            }
            if (!isDateNull)
            {
                dDate = (DateTime)cbInvoiceDate.SelectedItem;
            }
            if (!isNumNull)
            {
                sID = cbInvoiceNum.SelectedItem.ToString();
            }


            string sSQL;
            List<Invoice> Invoices;
            int iRet = 0;

            //Check all possible outcomes and execute the proper sql
            if (isChargeNull && isDateNull && isNumNull)
            {
                //All are null, display everything
                sSQL = sql.SelectAll();
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);
                Invoices = buildInvoiceList(ds, iRet);
                
            }
            else if (isChargeNull && isDateNull && !isNumNull)
            {
                //Filter using InvoiceNum
                sSQL = sql.SelectInvoiceID(sID);
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);
                Invoices = buildInvoiceList(ds, iRet);
            }
            else if (isChargeNull && !isDateNull && isNumNull)
            {
                //Filter using InvoiceDate
                sSQL = sql.SelectInvoiceDate(dDate);
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);
                Invoices = buildInvoiceList(ds, iRet);
            }
            else if (!isChargeNull && isDateNull && isNumNull)
            {
                //Filter using TotalCharge
                sSQL = sql.SelectInvoiceCost(sCharge);
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);
                Invoices = buildInvoiceList(ds, iRet);
            }
            else if (isChargeNull && !isDateNull && !isNumNull)
            {
                //Filter using InvoiceDate and InvoiceNum
                sSQL = sql.SelectIDDate(sID, dDate);
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);
                Invoices = buildInvoiceList(ds, iRet);
            }
            else if (!isChargeNull && isDateNull && !isNumNull)
            {
                //Filter using TotalCharge and Invoice Num
                sSQL = sql.SelectIDCost(sID, sCharge);
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);
                Invoices = buildInvoiceList(ds, iRet);

            }
            else if (!isChargeNull && !isDateNull && isNumNull)
            {
                //Filter using Total Charge and InvoiceDate
                sSQL = sql.SelectDateCost(dDate, sCharge);
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);
                Invoices = buildInvoiceList(ds, iRet);
            }
            else
            {
                //Filter using all
                sSQL = sql.SelectIDDateCost(sID, dDate, sCharge);
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);
                Invoices = buildInvoiceList(ds, iRet);
            }

            return Invoices;


        }//end of GetUpdatedData()


        /// <summary>
        /// Function that builds a list of Invoices from the dataSet that comes from a sql query
        /// </summary>
        /// <param name="ds">The dataset from the updated data</param>
        /// <param name="iRet">an int that shows how many rows there are</param>
        /// <returns>returns a list of invoices</returns>
        private List<Invoice> buildInvoiceList(DataSet ds, int iRet)
        {
            List<Invoice> invoiceList = new List<Invoice>();

            Invoice inv;

            for (int i = 0; i < iRet; i++)
            {
                inv = new Invoice();
                inv.InvoiceNum = (int)ds.Tables[0].Rows[i]["InvoiceNum"];
                inv.InvoiceDate = (DateTime)ds.Tables[0].Rows[i]["InvoiceDate"];
                inv.TotalCost = (int)ds.Tables[0].Rows[i]["TotalCost"];

                invoiceList.Add(inv);
            }

            return invoiceList;
        }//End of buildInvoiceList


        /// <summary>
        /// Function that returns a list of invoices, used at the beginning of the program to fill the comboboxes
        /// </summary>
        /// <returns>returns list of Invoices</returns>
        public List<Invoice> getInvoices()
        {
            clsDataAccess db = new clsDataAccess();
            clsSearchSQL sql = new clsSearchSQL();
            DataSet ds = new DataSet();

            int iRet = 0;
            string sSQL = sql.SelectAll();

            List<Invoice> InvoiceList;

            ds = db.ExecuteSQLStatement(sSQL, ref iRet);
            InvoiceList = buildInvoiceList(ds, iRet);

            return InvoiceList;
        }

    }
}
