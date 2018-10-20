namespace MilitaryHumanResources.Database.Interface
{
    public interface IUpsertItem
    {
        long GetItemId();
        string GetTableName();
        string InsertItem();
        string UpdateItem();
    }
}
