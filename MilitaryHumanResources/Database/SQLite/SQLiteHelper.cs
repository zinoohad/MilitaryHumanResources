using System.Collections.Generic;
using System.Data;
using MilitaryHumanResources.Common;
using MilitaryHumanResources.Database.Interface;
using MilitaryHumanResources.Model;

namespace MilitaryHumanResources.Database.SQLite
{
    public class SQLiteHelper : ISqlHelper
    {
        private SQLiteDB _db => SQLiteDB.Instance;

        #region General

        /// <inheritdoc />
        public long UpsertItem(IUpsertItem item)
        {
            if (item == null)
            {
                throw new System.ArgumentNullException(nameof(item));
            }

            var dt = _db.Select($"SELECT * FROM {item.GetTableName()} WHERE ID = {item.GetItemId()}");
            return dt?.Rows.Count > 0 ? _db.Update(item.UpdateItem()) : _db.Insert(item.InsertItem());
        }

        #endregion  // General

        #region Soldir

        public List<Soldier> GetListOfSoldiers()
        {
            var soldiers = new List<Soldier>();
            var dt = _db.Select("SELECT * FROM ViewSoldier");
            foreach (DataRow dr in dt?.Rows)
            {
                var soldier = DynamicClassLoader.LoadClass<Soldier>(dr);
                soldier.Role = new Role
                {
                    ID = int.Parse(dr["RoleID"].ToString()),
                    Title = dr["RoleTitle"].ToString()
                };
                soldier.Rank = new Rank
                {
                    ID = int.Parse(dr["RankID"].ToString()),
                    Title = dr["RankTitle"].ToString()
                };
                soldier.MainProfession = new Profession
                {
                    ID = int.Parse(dr["MainProfessionID"].ToString()),
                    Title = dr["MainProfessionTitle"].ToString()
                };
                soldier.SecondaryProfession = new Profession
                {
                    ID = int.Parse(dr["SecondaryProfessionID"].ToString()),
                    Title = dr["SecondaryProfessionTitle"].ToString()
                };
                soldier.CombatInlay = new ArmoredVessels
                {
                    ID = int.Parse(dr["ArmoredVesselsID"].ToString()),
                    Name = dr["ArmoredVesselsName"].ToString(),
                    Capacity = int.Parse(dr["ArmoredVesselsCapacity"].ToString()),
                };
                soldiers.Add(soldier);
            }
            return soldiers;
        }

        #endregion  //Soldir
    }
}
