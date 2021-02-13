using CodeSquirrel.System;
using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CodeSquirrel.RecipeApp.DataProvider.src.Repository
{
    public class IngredientRepository : IRepository<IngredientDTO>
    {
        private const string TABLE_NAME = "Instruction";
        private const string INSERT_QUERY = "INSERT INTO \"CodeSquirrel\".\"Ingredient\"" +
                                            "(\"UniqueID\", \"ProductID\", \"UnitID\" \"Deleted\")" +
                                            "VALUES (@UniqueID, @ProductID, @UnitID, @Deleted)";
        private const string UPDATE_QUERY = "UPDATE \"CodeSquirrel\".\"Ingredient\"" +
                                            "SET \"ProductID\" = @ProductID, \"UnitID\" = @UnitID, \"Deleted\" = @Deleted " +
                                            "WHERE \"UnqiueID\" = @UniqueID";

        private readonly IDbConnection _connection;
        private readonly ILogger<IngredientRepository> _logger;

        protected QueryBuilder Builder { get; }

        public IngredientRepository(IDbConnection connection, ILogger<IngredientRepository> logger)
        {
            _connection = connection;
            _logger = logger;

            Builder = new QueryBuilder(TABLE_NAME);
        }
        
        private object[] GetParameters(IEnumerable<IngredientDTO> ingredients) => ingredients.Select(i => new
            { 
                i.UniqueID, 
                i.ProductID, 
                i.UnitID, 
                i.Deleted 
            }).ToArray();

        public bool Add(IngredientDTO entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }
                
                _connection.Open();

                return _connection.Execute(INSERT_QUERY, entity) > 0;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new Ingredient.", entity);

                return false;
            }
            finally
            {
                _connection.Close();
                _connection.Dispose();
            }
        }
        public bool AddRange(IEnumerable<IngredientDTO> entities)
        {
            var transaction = _connection.BeginTransaction();

            try
            {
                var parameters = GetParameters(entities);

                _connection.Execute(INSERT_QUERY, parameters);

                transaction.Commit();

                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();

                _logger.LogError(ex, "An error occurred while adding multiple ingredients", entities);
                _logger.LogInformation("Tranaction used: insertion of all new ingredients has been cancelled.");

                return false;
            }
            finally
            {
                _connection.Close();
                _connection.Dispose();
                transaction.Dispose();

            }
        }
        public IList<IngredientDTO> Get()
        {
            try
            {
                _connection.Open();
                return _connection.Query<IngredientDTO>(Builder.SelectAll).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all Ingredients.");

                return new List<IngredientDTO>();
            }
            finally
            {
                _connection.Close();
                _connection.Dispose();
            }
        }
        public IngredientDTO GetByID(Guid uniqueID)
        {
            try
            {
                _connection.Open();
                return _connection.QueryFirstOrDefault<IngredientDTO>(Builder.SelectByID, new { UniqueID = uniqueID });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving an ingredient.", uniqueID);

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

                var param = new { UniqueID = uniqueID };
                return _connection.Execute(Builder.DeleteByID, param) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while trying to delete an ingredient.", uniqueID);

                return false;
            }
            finally
            {
                _connection.Close();
                _connection.Dispose();
            }
        }
        public bool Update(IngredientDTO entity)
        {
            try
            {
                _connection.Open();

                return _connection.Execute(UPDATE_QUERY, entity) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while trying to update an ingredient.", entity);
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
