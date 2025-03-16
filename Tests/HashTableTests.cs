using DataStructures;

namespace Tests.DataStructures;

public class HashTableTests {
    [Fact]
    public void Add_ShouldAddKeyValuePair() {
        // Arrange
        var table = new HashTable<string, int>(capacity: 4);

        // Act
        table.Add("one", 1);

        // Assert
        Assert.True(table.TryGetValue("one", out var value));
        Assert.Equal(1, value);
    }

    [Fact]
    public void Add_ShouldResize_WhenCapacityIsReached() {
        // Arrange
        var table = new HashTable<string, int>(capacity: 2);
        table.Add("one", 1);
        table.Add("two", 2);

        // Act
        table.Add("three", 3); // Deve causar o resize

        // Assert
        Assert.Equal(3, table.Count);
        Assert.True(table.TryGetValue("one", out var v1) && v1 == 1);
        Assert.True(table.TryGetValue("two", out var v2) && v2 == 2);
        Assert.True(table.TryGetValue("three", out var v3) && v3 == 3);
    }

    [Fact]
    public void TryGetValue_ShouldReturnTrueAndCorrectValue_WhenKeyExists() {
        // Arrange
        var table = new HashTable<string, int>(capacity: 4);
        table.Add("one", 1);

        // Act
        var result = table.TryGetValue("one", out var value);

        // Assert
        Assert.True(result);
        Assert.Equal(1, value);
    }

    [Fact]
    public void TryGetValue_ShouldReturnFalse_WhenKeyDoesNotExist() {
        // Arrange
        var table = new HashTable<string, int>(capacity: 4);

        // Act
        var result = table.TryGetValue("nonexistent", out var value);

        // Assert
        Assert.False(result);
        Assert.Equal(0, value);
    }

    [Fact]
    public void TryGet_ShouldReturnValue_WhenKeyExists() {
        // Arrange
        var table = new HashTable<string, int>(capacity: 4);
        table.Add("test", 42);

        // Act
        var result = table.TryGet("test");

        // Assert
        Assert.Equal(42, result);
    }

    [Fact]
    public void TryGet_ShouldReturnDefault_WhenKeyDoesNotExist() {
        // Arrange
        var table = new HashTable<string, int>(capacity: 4);

        // Act
        var result = table.TryGet("missing");

        // Assert
        Assert.Equal(0, result);
    }

    [Fact]
    public void Add_ShouldThrowException_WhenCapacityIsNegative() {
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new HashTable<string, int>(capacity: -1));
    }

    [Fact]
    public void Add_ShouldThrowException_WhenCapacityIsLessThanTwo() {
        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => new HashTable<string, int>(capacity: 1));
    }
}
