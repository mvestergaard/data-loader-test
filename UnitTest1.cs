using GreenDonut;

namespace DataLoaderTest;

public class UnitTest1
{
    [Fact]
    public async Task ResultShouldBeEmptyWhenAllKeysAreMissing()
    {
        var loader = new TestDataLoader(new Dictionary<int, string>(), new AutoBatchScheduler(), new DataLoaderOptions());
        var result = await loader.LoadRequiredAsync([1]);
        Assert.Empty(result);
    }

    [Fact]
    public async Task ResultShouldNotContainMissingItems()
    {
        var loader = new TestDataLoader(new Dictionary<int, string> { { 1, "foo" }, { 3, "bar" } },
            new AutoBatchScheduler(), new DataLoaderOptions());
        
        var result = await loader.LoadRequiredAsync([1, 2, 3]);
        Assert.NotNull(result[1]);
        Assert.Equal(2, result.Count);
    }

    private class TestDataLoader : BatchDataLoader<int, string>
    {
        private readonly Dictionary<int, string> _dictionary;

        public TestDataLoader(Dictionary<int, string> dictionary, IBatchScheduler batchScheduler, DataLoaderOptions options) : base(batchScheduler, options)
        {
            _dictionary = dictionary;
        }

        protected override async Task<IReadOnlyDictionary<int, string>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_dictionary.AsReadOnly());
        }
    }
}