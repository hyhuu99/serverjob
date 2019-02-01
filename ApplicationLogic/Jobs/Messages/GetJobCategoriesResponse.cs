using System;
using System.Collections.Generic;
using System.Text;

namespace ApplicationLogic.Jobs.Messages
{
    public class GetJobCategoriesResponse
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int Quantity { get; set; }
    }
}
