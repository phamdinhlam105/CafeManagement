namespace CafeManagement.Models.Report
{
    public class StockReportDetail
    {
        public Guid Id {  get; set; }
        public Guid IngredientId {  get; set; }
        public string IngredientName { get; set; }
        public float QuantityOnHand {  get; set; }
        public float QuantityImported {  get; set; }
        public float QuantityExported {  get; set; }
        public float AdjustmentQuantity {  get; set; }
        public decimal TotalValueRemain {  get; set; }
    }
}
