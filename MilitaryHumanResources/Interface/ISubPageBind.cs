using System.Windows.Controls;

namespace MilitaryHumanResources.Interface
{
    public interface ISubPageBind<T> : IPage
    {
        void BindPage(IPageBind page);

        Page LoadPage(T data);

    }
}
