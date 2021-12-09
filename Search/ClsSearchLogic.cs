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

        public List<Invoice> getUpdatedData(ComboBox cbTotalCharge, ComboBox cbInvoiceDate, ComboBox cbInvoiceNum)
        {
            clsDataAccess db = new clsDataAccess();
            clsSearchSQL sql = new clsSearchSQL();
            DataSet ds = new DataSet();

            bool isChargeNull = cbTotalCharge.SelectedItem == null;
            bool isDateNull = cbInvoiceDate.SelectedItem == null;
            bool isNumNull = cbInvoiceNum.SelectedItem == null;

            string sCharge = cbTotalCharge.Text;
            DateTime dDate = (DateTime)cbInvoiceDate.SelectedItem;
            string sID = cbInvoiceNum.Text;


            string sSQL;
            List<Invoice> Invoices;
            int iRet = 0;

            if (isChargeNull && isDateNull && isNumNull)
            {
                //All are null, display everything
                sSQL = sql.SelectAll();
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);
                Invoices = buildInvoiceList(ds, iRet);
                
            }
            else if (isChargeNull && isDateNull)
            {
                //Filter using InvoiceNum
                sSQL = sql.SelectInvoiceID(sID);
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);
                Invoices = buildInvoiceList(ds, iRet);
            }
            else if (isChargeNull && isNumNull)
            {
                //Filter using InvoiceDate
                sSQL = sql.SelectInvoiceDate(dDate);
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);
                Invoices = buildInvoiceList(ds, iRet);
            }
            else if (isDateNull && isNumNull)
            {
                //Filter using TotalCharge
                sSQL = sql.SelectInvoiceCost(sCharge);
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);
                Invoices = buildInvoiceList(ds, iRet);
            }
            else if (isChargeNull)
            {
                //Filter using InvoiceDate and InvoiceNum
                sSQL = sql.SelectIDDate(sID, dDate);
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);
                Invoices = buildInvoiceList(ds, iRet);
            }
            else if (isDateNull)
            {
                //Filter using TotalCharge and Invoice Num
                sSQL = sql.SelectIDCost(sID, sCharge);
                ds = db.ExecuteSQLStatement(sSQL, ref iRet);
                Invoices = buildInvoiceList(ds, iRet);

            }
            else if (isNumNull)
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

    }
}
