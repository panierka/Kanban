using Kanban.DataAccessLayer.Repositories;

namespace DatabaseConnectionAndRepositoryTest
{
    [TestClass]
    public class RepositoryTestTest
    {
        [TestMethod]
        public void SelectAllTest()
        {
            var tests = RepositoryTest.GetAllTests();

            var result = string.Join(", ", tests);
            var expected = "[1] hubert: 46 pts, [2] kuba: 47 pts";

            Assert.AreEqual(expected, result);
        }
    }
}