using CodeSquirrel.System;
using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CodeSquirrel.RecipeApp.DataProvider
{
    public class UnitRepository : IRepository<UnitDTO>
    {

        private const string TABLE_NAME = "\"CodeSquirrel\".\"Unit\"";
        private const string INSERT_QUERY = "INSERT INTO \"CodeSquirrel\".\"Unit\"" +
                                            "(\"UniqueID\", \"Type\", \"Value\", \"Deleted\")" +
                                            "VALUES (@UniqueID, @Type, @Value, @Deleted)";
        private const string UPDATE_QUERY = "UPDATE \"CodeSquirrel\".\"Unit\"" +
                                            "SET \"Type\" = @Type, \"Value\" = @Value, \"Deleted\" = @Deleted " +
                                            "WHERE \"UniqueID\" = @UniqueID";

        private readonly IDbConnection _connection;
        private readonly ILogger<UnitRepository> _logger;

        public QueryBuilder Builder { get; }

        public UnitRepository(IDbConnection connection, ILogger<UnitRepository> logger)
        {
            _connection = connection;
            _logger = logger;

            Builder = new QueryBuilder(TABLE_NAME);
        }

        private object[] GetParameters(IEnumerable<UnitDTO> entities) => entities.Select(item => new
        {
            item.UniqueID,
            item.Value,
            item.Type,
            item.Deleted
        }).ToArray();

        public bool Add(UnitDTO entity)
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
                _logger.LogError(ex, "And error occured while trying to add a new Unit", entity);

                return false;
            }
            finally
            {
                _connection.Close();
                _connection.Dispose();
            }
        }
        public bool AddRange(IEnumerable<UnitDTO> entities)
        {
            var transaction = _connection.BeginTransaction();

            try
            {
                var parameters = GetParameters(entities);

                _connection.Open();
                _connection.Execute(INSERT_QUERY, parameters);

                transaction.Commit();

                return true;

            }
            catch (Exception ex)
            {
                transaction.Rollback();

                _logger.LogError(ex, "An error occured while adding multiple units.", entities);
                _logger.LogInformation("Insertion of all new units has been cancelled.");

                return false;
            }
            finally
            {
                _connection.Close();
                _connection.Dispose();
                transaction.Dispose();
            }
        }
        public IList<UnitDTO> Get()
        {
            try
            {
                _connection.Open();

                return _connection.Query<UnitDTO>(Builder.SelectAll).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while retrieving all units");

                return null;
            }
            finally
            {
                _connection.Close();
                _connection.Dispose();
            }
        }
        public UnitDTO GetByID(Guid uniqueID)
        {
            try
            {
                var param = new { UniqueID = uniqueID };

                _connection.Open();

                return _connection.QueryFirstOrDefault<UnitDTO>(Builder.SelectByID, param);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occured while retrieving a Unit.", uniqueID);

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
                _logger.LogError(ex, "An error occurred while trying to remove a Unit", uniqueID);

                return false;
            }
            finally
            {
                _connection.Close();
                _connection.Dispose();
            }
        }
        public bool Update(UnitDTO entity)
        {
            try
            {
                _connection.Open();
                return _connection.Execute(UPDATE_QUERY, entity) > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while trying to update an Unit.", entity);
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
