using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RateMyManagement.Data;

namespace RateMyManagement.Core
{
    public class EnumValueComparer<T> : ValueComparer<T> where T : Enum
    {
        public EnumValueComparer() : base((x, y) => x.Equals(y), x => (int)(object)x, x => x)
        { }
    }
    public class ManagerAttributesValueComparer : ValueComparer<List<ManagerAttribute>>
    {
        public override bool Equals(List<ManagerAttribute> x, List<ManagerAttribute> y)
        {
            if (x.Count != y.Count)
            {
                return false;
            }

            for (int i = 0; i < x.Count; i++)
            {
                if (!x[i].Equals(y[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode(List<ManagerAttribute> obj)
        {
            int hashCode = 0;
            foreach (var element in obj)
            {
                hashCode = hashCode * 31 + element.GetHashCode();
            }
            return hashCode;
        }

        public ManagerAttributesValueComparer(bool favorStructuralComparisons) : base(favorStructuralComparisons)
        {
        }
    }
}
