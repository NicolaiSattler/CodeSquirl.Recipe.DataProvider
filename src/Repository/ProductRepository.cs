using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CodeSquirl.System;
using Dapper;

namespace CodeSquirl.RecipeApp.DataProvider
{
    public class ProductRepository : IRepository<ProductDTO>
    {
        private const string SELECT_ALL = "SELECT * FROM \"CodeSquirl\".\"Product\"";
        private const string INSERT = "INSERT INTO \"CodeSquirl\".\"Product\"(\"UniqueID\", \"Name\", \"Type\", \"Perishable\", \"Deleted\") VALUES (@UniqueID, @Name, @Type, @Perishable, @Deleted)"; 

        private readonly IDbConnection _connection;

        public ProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public bool Add(ProductDTO entity)
        {
            try
            {
                _connection.Open();

                var result = _connection.Execute(INSERT, entity);
                return result > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                _connection.Close();
                _connection.Dispose();
            }
        }

        public void AddRange(IEnumerable<ProductDTO> entities)
        {
            throw new NotImplementedException();
        }

        public IList<ProductDTO> GetAll()
        {
            try 
            {
                _connection.Open();
                return _connection.Query<ProductDTO>(SELECT_ALL).ToList();
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                _connection.Close();
                _connection.Dispose();
            }
        }

        public bool Remove(ProductDTO entiy)
        {
            throw new NotImplementedException();
        }

        public bool Update(ProductDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
