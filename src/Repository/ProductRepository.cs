using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CodeSquirl.System;
using Dapper;

namespace CodeSquirl.RecipeApp.DataProvider
{
    public class ProductRepository : IRepository<ProductDTO>
    {
        private const string TABLE_NAME = "\"CodeSquirl\".\"Product\"";

        private const string INSERT = "INSERT INTO \"CodeSquirl\".\"Product\"" +
                                      "(\"UniqueID\", \"Name\", \"Type\", \"Perishable\", \"Deleted\") " +
                                      "VALUES (@UniqueID, @Name, @Type, @Perishable, @Deleted)";

        private const string UPDATE = "UPDATE \"CodeSquirl\".\"Product\" " +
                                      "SET \"Name\" = @Name, \"Type\" = @Type, \"Perishable\" = @Perishable " +
                                      "WHERE \"UniqueID\" = @UniqueID";
        
        private readonly IDbConnection _connection;
        protected QueryBuilder Builder { get; }

        public ProductRepository(IDbConnection connection)
        {
            _connection = connection;

            Builder = new QueryBuilder(TABLE_NAME);
        }

        private object[] AddRangeParameters(IEnumerable<ProductDTO> entities)
        {
            var index = 0;
            var length = entities.Count();
            var result = new object[length];

            foreach(var item in entities)
            {
                result[index] = new 
                {
                    item.UniqueID,
                    item.Name,
                    Type = (int)item.Type,
                    item.Perishable, 
                    Deleted = false
                };
            }

            return result;
        }

        public bool Add(ProductDTO entity)
        {
            try
            {
                _connection.Open();

                var result = _connection.Execute(INSERT, entity);
                return result > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                _connection.Close();
                _connection.Dispose();
            }
        }

        public bool AddRange(IEnumerable<ProductDTO> entities)
        {
            try
            {
                _connection.Open();

                var parameters = AddRangeParameters(entities);
                return _connection.Execute(INSERT, parameters) > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                _connection.Close();
                _connection.Dispose();
            }
        }

        public IList<ProductDTO> Get()
        {
            try 
            {
                _connection.Open();
                return _connection.Query<ProductDTO>(Builder.SelectAll).ToList();
            }
            catch(Exception ex)
            {
                return null;
            }
            finally
            {
                _connection.Close();
                _connection.Dispose();
            }
        }

        public ProductDTO GetByID(Guid uniqueID)
        {
            try
            {
                _connection.Open();
                return _connection.QueryFirstOrDefault<ProductDTO>(Builder.SelectByID, new { UniqueID = uniqueID });
            }
            catch(Exception ex)
            {
                return null;
            }
            finally
            {
                _connection.Close();
                _connection.Dispose();
            }
        }

        public bool Remove(Guid uniqueID)
        {
            try
            {
                _connection.Open();
                return _connection.Execute(Builder.DeleteByID, new { UniqueID = uniqueID }) > 0;
            }
            catch(Exception ex)
            {
                return false;
            }
            finally
            {
                _connection.Close();
                _connection.Dispose();
            }
        }

        public bool Update(ProductDTO entity)
        {
            try
            {
                _connection.Open();
                return _connection.Execute(UPDATE, entity) > 0;
            }
            catch(Exception ex)
            {
                return false;
            }
            finally
            {
                _connection.Close();
                _connection.Dispose(); 
            }
        }
    }
}
