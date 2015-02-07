using System.Collections.Generic;

namespace Gw2spidyApi.Objects.Wrapper
{
    public interface IWrapper<out TObject>
    {
        IEnumerable<TObject> Unwrap();
    }
}
