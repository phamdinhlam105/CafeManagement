namespace CafeManagement.Models
{
    public class StockEntry
    {
        public Guid Id { get; set; } 
        public DateTime EntryDate { get; set; } 
        public decimal TotalValue { get; set; } 
        public ICollection<StockEntryDetail> StockEntryDetails { get; set; } 

        public StockEntry()
        {
            StockEntryDetails = new List<StockEntryDetail>();
        }
    }
}
