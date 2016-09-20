using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kisumu.Reports.Models
{
    public class TransactionObject
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<TransactionsModel> Transactions { get; set; }
    }
    public class TransactionsModel
    {
        public string ReceiptNumber { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Account { get; set; }
        public string RevenueStream { get; set; }
        public string PaymentType { get; set; }
        public string Category { get; set; }
        public string SubCategory { get; set; }
        public string Zone { get; set; }
    }
}