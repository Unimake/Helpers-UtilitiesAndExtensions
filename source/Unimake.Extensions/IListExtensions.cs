using System.Linq;

#pragma warning disable IDE0130 // Namespace de extensões devem corresponder ao tipo extendido

namespace System.Collections.Generic
{
    public static class IListExtensions
    {
        #region Public Methods

        public static IList<TObject> GetInstanceOrEmpty<TObject>(this IList<TObject> list)
            => list ?? Enumerable.Empty<TObject>().ToList();

        #endregion Public Methods
    }
}
