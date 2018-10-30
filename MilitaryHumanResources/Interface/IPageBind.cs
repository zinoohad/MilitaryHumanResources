using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MilitaryHumanResources.Interface
{
    public interface IPageBind : IPage
    {
        void SubmitPageResult(object data);

        Page GetPage();
    }
}
