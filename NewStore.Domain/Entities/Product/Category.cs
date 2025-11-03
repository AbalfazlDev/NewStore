using NewStore.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewStore.Domain.Entities.Product
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public long? ParentCategoryId { get; set; }
        public virtual Category ParentCategory { get; set; }

        public ICollection<Category> ChildCategories { get; set; }
    }
}
