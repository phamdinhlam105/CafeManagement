using CafeManagement.Interfaces;

namespace CafeManagement.Models.Stock
{
    public class StockEntry:ISoftDeletable
    {
        public Guid Id { get; set; }
        public DateTime EntryDate { get; set; }
        public decimal TotalValue { get; set; }
        public ICollection<StockEntryDetail> StockEntryDetails { get; set; }
        public bool IsDeleted {  get; set; }

        public StockEntry()
        {
            StockEntryDetails = new List<StockEntryDetail>();
            IsDeleted = false;
        }
    }
}
