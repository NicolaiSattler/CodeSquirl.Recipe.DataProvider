using CodeSquirrel.System;
using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CodeSquirrel.RecipeApp.DataProvider
{
    public class RecipeRepository : IRepository<RecipeDTO>
    {
        private const string TABLE_NAME = "\"CodeSquirrel\".\"Recipe\"";
        private const string INSERT = "INSERT INTO \"CodeSquirrel\".\"Recipe\"" +
                                      "(\"UniqueID\", \"Name\", \"Type\", \"Diet\", \"AllowRemnants\", \"UserID\", \"Duration\", \"Deleted\") " +
                                      "VALUES (@UniqueID, @Name, @Type, @Diet, @AllowRemnants, @UserID, @Duration, @Deleted) ";
        private const string UPDATE = "UPDATE\"CodeSquirrel\".\"Recipe\"" +
                                      "SET \"UniqueID\" = @UniqueID, \"Name\" = @Name, \"Type\" = @Type, \"Diet\" = @Diet, " +
                                      "\"AllowRemnants\" = @AllowRemnants, \"UserID\" = @UserID, \"Duration\" = @Duration, \"Deleted\" = @Deleted " +
                                      "WHERE \"UniqueID\" = @UniqueID";

        private readonly IDbConnection _connection;
        private readonly ILogger<RecipeDTO> _logger;

        protected QueryBuilder Builder { get; }

        public RecipeRepository(IDbConnection connection, ILogger<RecipeDTO> logger)
        {
            _connection = connection;
            _logger = logger;

            Builder = new QueryBuilder(TABLE_NAME);
        }

        private object[] GetParameters(IList<RecipeDTO> entities) => entities.Select(item => new
        {
            item.UniqueID,
            item.Name,
            item.Type,
            item.Diet,
            item.AllowRemnants,
            item.UserID,
            item.Duration,
            item.Deleted
        }).ToArray();

        public bool Add(RecipeDTO entity)
        {
            try
            {
                if (entity == null)
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                _connection.Open();

                return _connection.Execute(INSERT, entity) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while trying to add a new Recipe", entity);

                return false;
            }
            finally
            {
                _connection.Close();
                _connection.Dispose();
            }
        }

        public bool AddRange(IList<RecipeDTO> entities)
        {
            var transaction = _connection.BeginTransaction();

            try
            {
                var parameters = GetParameters(entities);

                _connection.Open();
                _connection.Execute(INSERT, parameters);

                transaction.Commit();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while adding multiple recipes.", entities);

                transaction.Rollback();

                _logger.LogError("Insertion of all new recipes has been cancelled.");

                return false;
            }
            finally
            {
                _connection.Close();
                _connection.Dispose();
                transaction.Dispose();
            }
        }
        public IList<RecipeDTO> Get()
        {
            try
            {
                _connection.Open();

                return _connection.Query<RecipeDTO>(Builder.SelectAll).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all recipes");

                return new List<RecipeDTO>();

            }
            finally
            {
                _connection.Close();
                _connection.Dispose();
            }
        }

        public RecipeDTO GetByID(Guid uniqueID)
        {
            try
            {
                var param = new { UniqueID = uniqueID };

                _connection.Open();
                return _connection.QueryFirstOrDefault<RecipeDTO>(Builder.SelectByID, param);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving a recipy.", uniqueID);

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
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while trying to remove a recipe", uniqueID);
                return false;
            }
            finally
            {
                _connection.Close();
                _connection.Dispose();
            }
        }

        public bool Update(RecipeDTO entity)
        {
            try
            {
                _connection.Open();
                return _connection.Execute(UPDATE, entity) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while trying to update a recipe", entity);
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
