using Bootcamp.CRUD.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bootcamp.CRUD
{
    public class Transaction:BaseModel
    {
        public DateTimeOffset TransactionDate { get; set; }
    }
}
