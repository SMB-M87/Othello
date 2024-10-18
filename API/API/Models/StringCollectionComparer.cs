using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace API.Models
{
    public class StringCollectionComparer : ValueComparer<ICollection<string>>
    {
        public StringCollectionComparer() : base(
            (c1, c2) => (c1 == null && c2 == null) || (c1 != null && c2 != null && c1.SequenceEqual(c2)),
            c => c != null ? c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())) : 0,
            c => c != null ? c.ToList() : new List<string>()
        )
        { }
    }
}
