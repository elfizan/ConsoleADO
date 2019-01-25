using Bootcamp.CRUD.Context;
using Bootcamp.CRUD.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.CRUD.Controller
{
    public class TransactionsController
    {
        public int? count;
        public void ManageTransaction()
        {

            Item item = new Item();
            Transaction transaction = new Transaction();
            MyContext _context = new MyContext();
            TransactionItem transactionItem = new TransactionItem();

            transaction.TransactionDate = DateTimeOffset.Now.LocalDateTime;
            _context.Transactions.Add(transaction);
            _context.SaveChanges();

            //get id dan date last
            var gettransactionIdData = _context.Transactions.Max(x=> x.Id);
            var getTransactionDetail = _context.Transactions.Find(gettransactionIdData);

            ///masukkan id ne pie
            Console.WriteLine("Transaksi id : " + gettransactionIdData);
            Console.WriteLine("Date Transaction:  " +getTransactionDetail.TransactionDate);

            Console.Write("How many Item : ");
            count = Convert.ToInt16(Console.ReadLine());

            if(count == null)
            {
                Console.WriteLine("Plese insert count of item that you want to buy");
            }
            else
            {
                for (int i = 1; i <= count; i++)
                {
                    Console.Write("Input Id Item: ");
                    int? inputId = Convert.ToInt16(Console.ReadLine());
                    if (inputId == null)
                    {
                        Console.WriteLine("Please insert id that you want buy");
                        Console.Read();
                    }
                    else
                    {
                        //Pencarian id item
                        var getItem = _context.Items.Find(inputId);
                        if (getItem == null)
                        {
                            Console.Write("We dont have item with id: "+inputId);
                            Console.Read();
                        }
                        else
                        {
                            Console.Write("Input amount of item: ");
                            int quantity = Convert.ToInt16(Console.ReadLine());
                            if(getItem.Stock < quantity)
                            {
                                Console.Write("Stock enough, we have " + getItem.Stock);
                                Console.ReadLine();
                            }
                            else
                            {
                                transactionItem.Transactions = getTransactionDetail;
                                transactionItem.Items = getItem;
                                transactionItem.Quantity = quantity;
                                _context.TransactionItems.Add(transactionItem);
                               _context.SaveChanges();
                            }
                        }
                    }
                }
                var getPrice = _context.TransactionItems.Where(x => x.Transactions.Id == getTransactionDetail.Id).ToList();
                int? total = 0;
                foreach(var proceed in getPrice)
                {
                    total += (proceed.Quantity * proceed.Items.Price);
                }
                Console.WriteLine("Total Price: " + total);
                Console.Write("Balance : ");
                int? balance = Convert.ToInt32(Console.ReadLine());
                Console.Write("Exchange : " + (balance - total));
                Console.ReadLine();

                Console.WriteLine("Transaction ID");
                Console.WriteLine(getTransactionDetail.TransactionDate.Date);
                Console.WriteLine(getTransactionDetail.TransactionDate.TimeOfDay);
                Console.WriteLine("\tName \tQuantity\t Price\t Total");
                
                foreach (var tampilin in getPrice) { 
                    Console.WriteLine("\t "+tampilin.Items.Name+ "\t"+tampilin.Quantity+ "\t" +tampilin.Items.Price+ "\t" +total);
                }
                Console.WriteLine("Total Price : " + total);
                Console.WriteLine("Balance : " + balance);
                Console.WriteLine("Exchange : " + (balance - total));
                Console.Read();
            }
            //call

           
            Console.Read();
        }
    }
}
