
using System.Data.Entity.ModelConfiguration.Configuration;

namespace MilitaryHumanResources.Database.SQLite.Objects
{
    public class SQLiteFieldType
    {
        public string Name { get; set; }

        public SQLiteTypes Type { get; set; }

        public string Length { get; set; } = null;

        public SQLiteFieldType()
        {
        }

        public SQLiteFieldType(string name, SQLiteTypes type, string length = null)
        {
            Name = name;
            Type = type;
            Length = length;
        }

        public override string ToString()
        {
            return $"{Name} {Type}{Length ?? ""}";
        }
    }
}
