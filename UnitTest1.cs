using GreenDonut;

namespace DataLoaderTest;

public class UnitTest1
{
    [Fact]
    public async Task Test1()
    {
        var loader = new TestDataLoader(new AutoBatchScheduler(), new DataLoaderOptions());
        var result = await loader.LoadRequiredAsync([1]);
        Assert.Empty(result);
    }

    private class TestDataLoader : BatchDataLoader<int, string>
    {
        public TestDataLoader(IBatchScheduler batchScheduler, DataLoaderOptions options) : base(batchScheduler, options)
        {
        }

        protected override async Task<IReadOnlyDictionary<int, string>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new Dictionary<int, string>().AsReadOnly());
        }
    }
}