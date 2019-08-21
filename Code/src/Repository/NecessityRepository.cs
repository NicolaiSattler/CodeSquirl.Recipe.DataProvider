using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using CodeSquirrel.System;
using Dapper;
using Microsoft.Extensions.Logging;

namespace CodeSquirrel.RecipeApp.DataProvider
{
    public class NecessityRepository : IRepository<NecessityDTO>
    {
        private const string TABLE_NAME = "\"CodeSquirrel\".\"Necessity\"";

        private const string INSERT = "INSERT INTO \"CodeSquirrel\".\"Necessity\" " + 
                                      "(\"UniqueID\", \"Name\", \"Description\", \"Electrical\", \"Deleted\")" + 
                                      "VALUES (@UniqueID, @Name, @Description, @Electrical, @Deleted)";
        private const string UPDATE = "UPDATE \"CodeSquirl\".\"Necessity\" SET (\"Name\" = @Name," +
                                      "\"Description\" = @Description, \"Electrical\" = @Electrical, \"Deleted\" = @Deleted) " + 
                                      "WHERE \"UniqueID\" = @UniqueID";

        private readonly IDbConnection _connection;
        private readonly ILogger _logger;

        protected QueryBuilder Builder { get; }

        public NecessityRepository(IDbConnection connection, ILogger<NecessityRepository> logger)
        {
            _connection = connection;
            _logger = logger;

            Builder = new QueryBuilder(TABLE_NAME);
        }

        private object[] GetParameters(IList<NecessityDTO> entities)
        {
            var length = entities?.Count() ?? 0;
            var result = new object[length];

            for (var i = 0; i < length; i ++)
            {
                var item = entities[i];
                result[i] = new
                {
                    item.UniqueID,
                    item.Name,
                    item.Description,
                    item.Electrical,
                    item.Deleted
                };
            }

            return result;
        }

        public bool Add(NecessityDTO entity)
        {
            try
            {
                if (entity == null) 
                {
                    throw new ArgumentNullException(nameof(entity));
                }

                _connection.Open();

                var result = _connection.Execute(INSERT, entity);
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "And error occurred while trying to add a new necessity.", entity);
                
                return false;
            }
            finally{
                _connection.Close();
                _connection.Dispose();
            }
        }
        public bool AddRange(IList<NecessityDTO> entities)
        {
            var transaction = _connection.BeginTransaction();

            try
            {
                var parameters = GetParameters(entities);
                
                if (parameters.Length == 0)
                {
                    throw new ArgumentException($"{nameof(entities)} is null or empty.");
                }

                _connection.Open();

                var result = _connection.Execute(INSERT, parameters, transaction);

                if (result > 0)
                {
                    transaction.Commit();
                    
                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding multiple necessities.");
                
                transaction.Rollback();

                _logger.LogInformation("Insertion of all new necessities have been cancelled.");

            }
            finally
            {
                _connection.Close();
                _connection.Dispose();
                transaction.Dispose();
            }
            
            return false;
        }
        public async Task<bool> AddRangeAsync(IList<NecessityDTO> entities)
        {
            var transaction = _connection.BeginTransaction();

            try
            {
                var parameters = GetParameters(entities);
                
                if (parameters.Length == 0)
                {
                    throw new ArgumentException($"{nameof(entities)} is null or empty.");
                }

                var result = await _connection.ExecuteAsync(INSERT, parameters, transaction);

                if (result > 0)
                {
                    transaction.Commit();

                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding multiple necessities asynchronously");
                
                transaction.Rollback();

                _logger.LogInformation("Insertion of all new necessities have been cancelled.");
            }
            finally
            {
                _connection.Close();
                _connection.Dispose();
                transaction.Dispose();
            }
            
            return false;
        }
        public IList<NecessityDTO> Get()
        {
            try 
            {
                _connection.Open();
                
                return _connection.Query<NecessityDTO>(Builder.SelectAll).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all necessities.");
                
                return new List<NecessityDTO>();
            }
            finally
            {
                _connection.Close();
                _connection.Dispose();
            }

        }
        public async Task<IEnumerable<NecessityDTO>> GetAsync()
        {
            try 
            {
                _connection.Open();
                
                return await _connection.QueryAsync<NecessityDTO>(Builder.SelectAll);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all necessities.");
                
                return null;
            }
            finally
            {
                _connection.Close();
                _connection.Dispose();
            }
        }
        public NecessityDTO GetByID(Guid uniqueID)
        {
            try
            {
                var param = new { UniqueID = uniqueID };
                
                _connection.Open();

                return _connection.QueryFirstOrDefault<NecessityDTO>(Builder.SelectByID, param);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving a necessity.", uniqueID);

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
                var param =  new { UniqueID = uniqueID };

                _connection.Open();

                return _connection.Execute(Builder.DeleteByID, param) > 0;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while trying to remove a necessity", uniqueID);
                throw;
            }
            finally
            {
                _connection.Close();
                _connection.Dispose();
            }
        }
        public bool Update(NecessityDTO entity)
        {
            try
            {
                _connection.Open();
                
                return _connection.Execute(UPDATE, entity) > 0;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while trying to update a product", entity);
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