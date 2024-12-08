using Microsoft.AspNetCore.Mvc;

namespace Frontend.Models
{
    public class DashBoard
    {
        public int TotalUserBase { get; set; }
        public int TotalBookCount { get; set; }
        public int BranchCount { get; set; }
        public int TotalBorrowedBooks { get; set; }
        public int TotalReturnedBooks { get; set; }
        
    }

    public class OverdueBorrower
    {
        public string Name { get; set; }
        public string BorrowedId { get; set; }
    }
    public class MyComponentModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
