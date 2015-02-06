namespace Gw2spidyApi.Objects.Wrapper
{
    public interface IWrapper<out TObject>
    {
        TObject Unwrap();
    }
}
