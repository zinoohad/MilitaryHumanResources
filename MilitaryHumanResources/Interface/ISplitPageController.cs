using System.Windows.Controls;

namespace MilitaryHumanResources.Interface
{
    public interface ISplitPageController
    {
        void LoadMainPage(object page);

        void OpenSubMenu<T>(ISubPageBind<T> bindPage, T data);

        void CloseSubMenu();

    }
}
