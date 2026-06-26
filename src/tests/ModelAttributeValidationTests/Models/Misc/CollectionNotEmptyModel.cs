#region U S I N G

using RzR.Validation.Attributes.Attributes.Collection;
using System.Collections.Generic;

#endregion

namespace ModelAttributeValidationTests.Models.Misc
{
    public class CollectionNotEmptyListModel
    {
        [ValCollectionNotEmpty] 
        public List<int> Items { get; set; }
    }

    public class CollectionNotEmptyArrayModel
    {
        [ValCollectionNotEmpty] 
        public int[] Items { get; set; }
    }
}
