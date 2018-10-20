using System.Collections.Generic;
using MilitaryHumanResources.Model;

namespace MilitaryHumanResources.Database.Interface
{
    public interface ISqlHelper
    {

        #region General

        /// <summary>
        /// Insert or Upsert item.
        /// </summary>
        /// <param name="item">The relevant item</param>
        /// <returns>Long value represent the new row in the table</returns>
        long UpsertItem(IUpsertItem item);

        #endregion


        #region Soldier

        List<Soldier> GetListOfSoldiers();

        #endregion

    }
}
