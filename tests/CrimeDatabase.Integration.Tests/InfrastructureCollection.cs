using Xunit;

namespace CrimeDatabase.Integration.Tests;

[CollectionDefinition("Infrastructure")]
public class InfrastructureCollection : ICollectionFixture<InfrastructureFixture> { }
