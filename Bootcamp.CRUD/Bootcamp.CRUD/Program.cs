using Bootcamp.CRUD.Context;
using Bootcamp.CRUD.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.CRUD
{
    class Program
    {
        static void Main(string[] args)
        {
            var result=0;

            Supplier supplier = new Supplier();
            MyContext _context = new MyContext();
            Console.WriteLine("================ Supplier Data ===================");
            Console.WriteLine("1. Insert \n2. Update \n3. Delete \n4. Rerieve");
            Console.WriteLine("==================================================");
            Console.Write("Going to : ");
            char chance = Convert.ToChar(Console.ReadLine());
            Console.WriteLine("==================================================");
            switch (chance)
            {
                case '1':
                    // memasukkan nilai name, join date dan create date dalam table supplier
                    Console.Write("Insert Name of Supplier: ");
                    supplier.Name = Console.ReadLine();
                    supplier.JoinDate = DateTimeOffset.Now.LocalDateTime;
                    supplier.CreateDate = DateTimeOffset.Now.LocalDateTime;

                    _context.Suppliers.Add(supplier);
                    result = _context.SaveChanges();
                    if (result > 0)
                    {
                        Console.Write("Insert Success");
                        Console.Read();
                    }
                    else
                    {
                        Console.Write("Insert failed");
                        Console.Read();
                    }
                    break;
                case '2':
                    // Input id untuk dicari
                    Console.Write("Insert Id to Update Date : ");
                    int id = Convert.ToInt16(Console.ReadLine());
                    //mencari data sesuai dengan id di database 
                    var get = _context.Suppliers.Find(id);

                    //pengecekan data didatabase
                    if (get == null)
                    {
                        Console.Write("Sorry your data is not found");
                        Console.Read();
                    }
                    else {
                        //jika ada, maka akan meminta inputan nama dan akan di simpan di database
                        Console.Write("Insert Name of Supplier : ");
                        get.Name = Console.ReadLine();
                        get.UpdateDate = DateTimeOffset.Now.LocalDateTime;

                        result = _context.SaveChanges();
                        if (result > 0)
                        {
                            Console.Write("Update Success");
                            Console.Read();
                        }
                        else
                        {
                            Console.Write("Insert failed");
                            Console.Read();
                        }
                    }
                    break;
                case '3':
                    // Input id untuk dicari
                    Console.Write("Insert Id to delete Date : ");
                    //mencari data sesuai dengan id di database 
                    var getData = _context.Suppliers.Find(Convert.ToInt16(Console.ReadLine()));

                    //pengecekan data didatabase
                    if (getData == null)
                    {
                        Console.Write("Sorry your data is not found");
                        Console.Read();
                    }
                    else
                    {
                        //jika ada, maka akan mengubah status is delete dan akan disimpan di database
                        getData.IsDelete = true;
                        getData.DeleteDate = DateTimeOffset.Now.LocalDateTime;

                        result = _context.SaveChanges();
                        if (result > 0)
                        {
                            Console.Write("Delete Success");
                            Console.Read();
                        }
                        else
                        {
                            Console.Write("Insert failed");
                            Console.Read();
                        }
                    }
                    break;
                case '4':
                    //menampilkan data yang di supplier dimana data isdelete bernilai false, 
                    var getDatatoDisplay = _context.Suppliers.Where(x => x.IsDelete == false).ToList();
                    if(getDatatoDisplay.Count == 0)
                    {
                        Console.Write("No data in your database");
                        Console.Read();
                    }
                    else
                    {
                        foreach(var tampilkan in getDatatoDisplay)
                        {
                            Console.WriteLine("==================================");
                            Console.WriteLine("Name : " +tampilkan.Name);
                            Console.WriteLine("Join Date : " + tampilkan.JoinDate);
                            Console.WriteLine("==================================");
                        }
                        Console.Write("Total Supplier" + getDatatoDisplay.Count);
                        Console.Read();
                    }
                    
                    break;
                default:

                    break;
            }
         }
    }
}
