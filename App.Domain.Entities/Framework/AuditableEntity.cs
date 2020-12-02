using System;

namespace App.Domain.Entities.Framework
{
    public abstract class IAuditableEntity
    {
        // When the entity was Created 
        public string CreatedBy { get; private set; }
        public DateTime CreatedDateTime { get; private set; }

        // When the entity was Last Updated
        public string LastModifiedBy { get; private set; }
        public DateTime LastModifiedDateTime { get; private set; }

        /// <summary>
        /// Record when and who created the entity
        /// </summary>
        /// <param name="user"></param>
        public void Created(string user)
        {
            CreatedBy = user;
            CreatedDateTime = DateTime.UtcNow;

            LastModifiedBy = user;
            LastModifiedDateTime = CreatedDateTime;
        }

        /// <summary>
        /// Record when and who modified the entity
        /// </summary>
        /// <param name="user"></param>
        public void Modified(string user)
        {
            LastModifiedBy = user;
            LastModifiedDateTime = DateTime.UtcNow;
        }
    }
}
