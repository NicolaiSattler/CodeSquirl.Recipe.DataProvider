namespace CodeSquirl.RecipeApp.DataProvider
{
    public class QueryBuilder
    {
        private const string SELECT_ALL = "SELECT * FROM {0} WHERE \"Deleted\" = false";
        private const string SELECT_BY_ID = "SELECT * FROM {0} WHERE \"UniqueID\" = @UniqueID";
        private const string DELETE_BY_ID = "UPDATE {0} SET \"Deleted\" = true WHERE \"UniqueID\" = @UniqueID";

        private readonly string TABLE_NAME;

        public string SelectAll
        {
            get
            {
                return string.Format(SELECT_ALL, TABLE_NAME);
            }
        }

        public string SelectByID
        {
            get
            {
                return string.Format(SELECT_BY_ID, TABLE_NAME);
            }
        }
        public string DeleteByID
        {
            get
            {
                return string.Format(DELETE_BY_ID, TABLE_NAME);
            }
        }
        public QueryBuilder(string tableName)
        {
            TABLE_NAME = tableName;
        }
    }
}