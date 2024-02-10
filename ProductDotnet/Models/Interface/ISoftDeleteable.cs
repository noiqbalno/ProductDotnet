namespace ProductDotnet.Models.Interface

{
    public interface ISoftDeleteable
    {
        public bool IsDeleted { get; set; }
    }
}
