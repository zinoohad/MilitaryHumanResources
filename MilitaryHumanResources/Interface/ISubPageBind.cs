using System.Windows.Controls;

namespace MilitaryHumanResources.Interface
{
    public interface ISubPageBind<T> : IPage
    {
        void BindPage(IPageBind<T> page);

        Page LoadPage(T data);

    }
}
