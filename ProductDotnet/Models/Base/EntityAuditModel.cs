using ProductDotnet.Models.Interface;
using System.ComponentModel.DataAnnotations;

namespace ProductDotnet.Models.Base
{
    public abstract class EntityAuditModel : IIdentityModel, IActivateableModel, IAuditedModel, ISoftDeleteable
    {
        [Key]
        public virtual int Id { get; set; }
        public bool IsActive { get; set; }

        [StringLength(EntityConstantModel.MAX_NAME_LENGTH)]
        public string? CreateByUserId { get; set; }
        public DateTime CreateDate { get; set; }

        [StringLength(EntityConstantModel.MAX_NAME_LENGTH)]
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}