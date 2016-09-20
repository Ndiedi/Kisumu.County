using Kisumu.Reports.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Kisumu.Reports.Controllers
{
    public class TablesController : Controller
    {
        string msg = "";
        DbConnector access = new DbConnector();

        public ActionResult StaticTables()
        {
            return View();
        }

        public ActionResult DataTables()
        {
            TransactionObject _object = new TransactionObject();
            List<TransactionsModel> data = new List<TransactionsModel>();
            DateTime startDate = DateTime.Now;
            _object.StartDate = startDate;
            
            DateTime endDate = DateTime.Now;
            _object.EndDate = endDate;
            DbDataReader reader = access.GetDBResults(ref msg, "Proc_GetTransactions", "@CountyCode", 42, "@StartDate", startDate, "@EndDate", endDate);

            if (msg!="")
            {
                _object.Transactions = data;
                return View(_object);
            }
            data = Functions.DataReaderMapToList<TransactionsModel>(reader);
            _object.Transactions = data;
            return View(_object);
        }
        [HttpPost]
        public ActionResult DataTables(TransactionObject _object)
        {
            //TransactionObject _object = new TransactionObject();
            List<TransactionsModel> data = new List<TransactionsModel>();
            
            DbDataReader reader = access.GetDBResults(ref msg, "Proc_GetTransactions", "@CountyCode", 42, "@StartDate", _object.StartDate, "@EndDate", _object.EndDate);

            if (msg != "")
            {
                _object.Transactions = data;
                return View(_object);
            }
            data = Functions.DataReaderMapToList<TransactionsModel>(reader);
            _object.Transactions = data;
            return View(_object);
        }

        public ActionResult FooTables()
        {
            return View();
        }

        public ActionResult jqGrid()
        {
            return View();
        }
	}
}