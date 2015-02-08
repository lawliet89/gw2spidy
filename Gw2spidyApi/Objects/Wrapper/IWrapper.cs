using System.Collections.Generic;

namespace Gw2spidyApi.Objects.Wrapper
{
    public interface IWrapper<out TObject>
        where TObject : Gw2Object
    {
        IEnumerable<TObject> Unwrap();
    }
}
