namespace ProductDotnet.Models.Interface
{
    public interface IAuditedModel
    {
        public string CreateByUserId { get; set; }
        public DateTime CreateDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}