using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MilitaryHumanResources.Database.Interface;

namespace MilitaryHumanResources.Model
{
    public class Soldier : IUpsertItem
    {

        public Image SoldirAvatar { get; set; } = Properties.Resources.user_avatar;

        public int ID { get; set; }

        public string Name { get; set; }

        public Rank Rank { get; set; }

        public Role Role { get; set; }

        public Profession MainProfession { get; set; }

        public Profession SecondaryProfession { get; set; }

        public ArmoredVessels CombatInlay { get; set; }

        public DateTime DateOfBirth { get; set; }

        public double Age => Math.Round((DateTime.Now - DateOfBirth).TotalDays / 365, 1);

        public string Address { get; set; }

        public string MobileNumber { get; set; }

        public string HomeNumber { get; set; }

        public string Email { get; set; }

        public string Notes { get; set; }

        #region Imlemented

        public long GetItemId() => ID;

        public static string GetTableName() => "Soldier";

        public string InsertItem()
        {
            return $"INSERT INTO {GetTableName()} (ID,Name,Rank, Role, Main_Profession, Secondary_Profession,Combat_Inlay,Date_Of_Birth,Address,Mobile_Number,Home_Number,Email,Notes) " +
                $"VALUES ({ID}, '{Name}', {Rank.ID}, {Role.ID}, {MainProfession.ID}, {SecondaryProfession.ID} ,{CombatInlay.ID}, '{DateOfBirth}', '{Address}', '{MobileNumber}','{HomeNumber}', '{Email}', '{Notes}')";
        }

        public string UpdateItem()
        {
            return $"UPDATE {GetTableName()} SET " +
                $"Name = '{Name}', Rank = {Rank.ID}, Role = {Role.ID}, Main_Profession = {MainProfession.ID}, Secondary_Profession = {SecondaryProfession.ID}, " +
                $"Combat_Inlay = {CombatInlay.ID} ,Date_Of_Birth = '{DateOfBirth:yyyy-MM-dd}' ,Address = '{Address}', Mobile_Number = '{MobileNumber}', " +
                $"Home_Number = '{HomeNumber}', Email = '{Email}', Notes = '{Notes}'" +
                $"WHERE ID = {ID}";

        }

        #endregion  // Implemented
    }
}
