using System;

namespace Framework.DataAccess
{
    public abstract class AuditableEntity<T> : BaseEntity<T>
    {
        public string CreatedBy { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime LastModifiedDateTime { get; set; }
    }
}
