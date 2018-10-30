using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryHumanResources.Common
{
    public static class Extentions
    {
        public static ObservableCollection<T> CastToObservableCollection<T>(this List<T> list)
        {
            var oc = new ObservableCollection<T>();
            foreach (var item in list)
                oc.Add(item);

            return oc;
        }
    }
}
