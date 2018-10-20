using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryHumanResources.Interface
{
    public interface IPageBind<T> : IPage
    {
        void SubmitPageResult(T data);
    }
}
