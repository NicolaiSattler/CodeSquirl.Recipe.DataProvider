using CodeSquirrel.RecipeApp.DataProvider;
using Dapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Logging;
using Moq;
using Moq.Dapper;
using System;
using System.Data;

namespace DataProvider.Test
{
    [TestClass]
    public class ProductRepositoryTest
    {
        protected Mock<IDbConnection> Connection { get; private set; }
        protected Mock<ILogger<ProductRepository>> Logger { get; private set; }
        public ProductRepository Repository { get; set; }

        [TestInitialize]
        public void Initialize()
        {
            Connection = new Mock<IDbConnection>();
            Logger = new Mock<ILogger<ProductRepository>>();
            Repository = new ProductRepository(Connection.Object, Logger.Object);
        }

        [TestMethod]
        public void Add_Success()
        {
            var product = new ProductDTO
            {
                ID = 1,
                UniqueID = Guid.NewGuid(),
                Name = "Example Name",
                Type = 1,
                Perishable = true,
                Deleted = false
            };

            Connection.Setup(c => c.Open());
            Connection.Setup(c => c.State)
                      .Returns(ConnectionState.Open);

            Connection.SetupDapper(c => c.Execute(It.IsAny<string>(), product, null, null, null))
                      .Returns(1);

            var result = Repository.Add(product);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Add_Failed()
        {
            Connection.Setup(c => c.Open());
            Connection.Setup(c => c.State).Returns(ConnectionState.Open);
            Connection.SetupDapper(c => c.Execute(It.IsAny<string>(), null, null, null, null))
                      .Returns(1);

            var result = Repository.Add(null);

            Assert.IsFalse(result);
        }
    }
}
