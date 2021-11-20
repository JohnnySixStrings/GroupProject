using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProject.Item
{
    public class clsItemsSQL
    {
        /// <summary>
        /// Constructor for clsItemsSQL class
        /// </summary>
        public clsItemsSQL()
        {
            // Insert constructor code here.
        }

        /// <summary>
        /// This SQL selects items from ItemDesc
        /// defaults to all items if parameters are empty
        /// </summary>
        /// <param name="iCode">Item Code</param>
        /// <param name="iDesc">Item Description</param>
        /// <param name="iCost">Item Cost</param>
        /// <returns>Returns SQL Statement in form of a string</returns>
        public string SelectItemDesc(char iCode = '', string iDesc = "",  string iCost = "")
        {
            string query = "SELECT ItemCode, ItemDesc, Cost FROM ItemDesc";
            if (iCode != '' && iDesc != "" && iCost != "")
            {
                query += $"WHERE ItemCode = {iCode} AND ItemDesc = {iDesc} AND Cost = {iCost}";
            }
            return query;
        }

        /// <summary>
        /// This SQL selects a distinct InvoiceNum from LineItems
        /// </summary>
        /// <param name="iCode"></param>
        /// <returns>Returns SQL Statement in form of a string</returns>
        public string SelectInvoiceID (char iCode)
        {
            string query = $"SELECT DISTINCT InvoiceNum FROM LineItems WHERE ItemCode = {iCode}";
            return query;
        }

        /// <summary>
        /// This SQL updates an ItemDesc entry's description and cost via an ItemCode
        /// </summary>
        /// <param name="iCode">Item Code</param>
        /// <param name="iDesc">Item Description</param>
        /// <param name="iCost">Item Cost</param>
        /// <returns>Returns SQL Statement in form of a string</returns>
        public string UpdateItemDesc(char iCode, string iDesc, string iCost)
        {
            string sSQL = $"UPDATE ItemDesc SET ItemDesc = {iDesc}, Cost = {iCost} WHERE ItemCode = {iCode}";
            return sSQL;
        }

        /// <summary>
        /// This SQL inserts a new item into ItemDesc
        /// </summary>
        /// <param name="iCode">Item Code</param>
        /// <param name="iDesc">Item Description</param>
        /// <param name="iCost">Item Cost</param>
        /// <returns>Returns SQL Statement in form of a string</returns>
        public string InsertItemDesc (char iCode, string iDesc, string iCost)
        {
            string sSQL = $"INSERT INTO ItemDesc (ItemCode, ItemDesc, Cost) VALUES({iCode}, {iDesc}, {iCost})";
            return sSQL;
        }

        /// <summary>
        /// This SQL deletes an existing item from an
        /// </summary>
        /// <param name="iCode"></param>
        /// <returns>Returns SQL Statement in form of a string</returns>
        public string DeleteItemDesc (char iCode)
        {
            string sSQL = $"DELETE FROM ItemDesc WHERE ItemCode = {iCode}";
        }
    }
}
