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
    public class ProductRepository : IRepository<ProductDTO>
    {
        private const string TABLE_NAME = "\"CodeSquirrel\".\"Product\"";

        private const string INSERT = "INSERT INTO \"CodeSquirrel\".\"Product\"" +
                                      "(\"UniqueID\", \"Name\", \"Type\", \"Perishable\", \"Deleted\") " +
                                      "VALUES (@UniqueID, @Name, @Type, @Perishable, @Deleted)";

        private const string UPDATE = "UPDATE \"CodeSquirrel\".\"Product\" " +
                                      "SET \"Name\" = @Name, \"Type\" = @Type, \"Perishable\" = @Perishable " +
                                      "WHERE \"UniqueID\" = @UniqueID";
        
        private readonly IDbConnection _connection;
        private readonly ILogger<ProductRepository> _logger;

        protected QueryBuilder Builder { get; }

        public ProductRepository(IDbConnection connection, ILogger<ProductRepository> logger)
        {
            _connection = connection;
            _logger = logger;

            Builder = new QueryBuilder(TABLE_NAME);
        }

        private object[] GetParameters(IEnumerable<ProductDTO> entities) => entities.Select(item => new
        {
            item.UniqueID,
            item.Name,
            item.Type,
            item.Perishable,
            item.Deleted
        }).ToArray();

        public bool Add(ProductDTO entity)
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
                _logger.LogError(ex, "An error occured while trying to add a new Product", entity);
                
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
                transaction.Rollback();

                _logger.LogError(ex, "An error occurred while adding multiple products.", entities);
                _logger.LogInformation("Insertion of all new products has been cancelled.");    

                return false;            
            }
            finally
            {
                _connection.Close();
                _connection.Dispose();
                transaction.Dispose();
            }
        }
        public async Task<bool> AddRangeAsync(IList<ProductDTO> entities)
        {
            var transaction = _connection.BeginTransaction();

            try
            {
                var parameters = GetParameters(entities);
                var result = await _connection.ExecuteAsync(INSERT, parameters, transaction);

                if (result > 0)
                {
                    transaction.Commit();

                    return true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding multiple products asynchronously");

                transaction.Rollback();

                _logger.LogInformation("Insertion of all new products have been cancelled.");
            }
            finally
            {
                _connection.Close();
                _connection.Dispose();
                transaction.Dispose();
            }

            return false;
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
                _logger.LogError(ex, "An error occurred while retrieving all products.");

                return null;
            }
            finally
            {
                _connection.Close();
                _connection.Dispose();
            }
        }
        public async Task<IEnumerable<ProductDTO>> GetAsync()
        {
            try
            {
                _connection.Open();

                return await _connection.QueryAsync<ProductDTO>(Builder.SelectAll);
            }
            catch (Exception ex)
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
        public ProductDTO GetByID(Guid uniqueID)
        {
            try
            {
                var param = new { UniqueID = uniqueID };

                _connection.Open();
                return _connection.QueryFirstOrDefault<ProductDTO>(Builder.SelectByID, param);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving a product.", uniqueID);

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
                _logger.LogError(ex, "An error occurred while trying to remove a product", uniqueID);
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
