using DataStructures;

namespace Tests.DataStructures;

public class ExpandableArrayTests {
    [Fact]
    public void Constructor_ShouldInitializeWithGivenCapacity() {
        // Arrange & Act
        var expandableArray = new ExpandableArray<int>(5);

        // Assert
        Assert.Empty(expandableArray); // Initially it must be empty
    }

    [Fact]
    public void Add_ShouldIncreaseCount() {
        // Arrange
        var expandableArray = new ExpandableArray<int>();

        // Act
        expandableArray.Add(1);
        expandableArray.Add(2);

        // Assert
        Assert.Equal(2, expandableArray.Count); // Count should increase with added items
    }

    [Fact]
    public void Add_ShouldResizeWhenCapacityIsFull() {
        // Arrange
        var expandableArray = new ExpandableArray<int>(2); // Starting with small capacity

        // Act
        expandableArray.Add(1); // Count becomes 1
        expandableArray.Add(2); // Count becomes 2
        expandableArray.Add(3); // Triggers resize

        // Assert
        Assert.Equal(3, expandableArray.Count); // Count should be 3 after resize
        Assert.Equal(3, expandableArray[2]); // The third element should be correctly added
    }

    [Fact]
    public void Indexer_ShouldThrowWhenAccessingOutOfRangeIndex() {
        // Arrange
        var expandableArray = new ExpandableArray<int>();
        expandableArray.Add(1);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => expandableArray[1]); // Index 1 should throw exception
    }

    [Fact]
    public void Indexer_ShouldReturnCorrectElement() {
        // Arrange
        var expandableArray = new ExpandableArray<int>();
        expandableArray.Add(10);
        expandableArray.Add(20);

        // Act & Assert
        Assert.Equal(10, expandableArray[0]);
        Assert.Equal(20, expandableArray[1]);
    }

    [Fact]
    public void Enumerator_ShouldIterateOverAllElements() {
        // Arrange
        var expandableArray = new ExpandableArray<int>();
        expandableArray.Add(10);
        expandableArray.Add(20);
        expandableArray.Add(30);

        // Act
        var items = new List<int>(expandableArray);

        // Assert
        Assert.Equal(3, items.Count); // There should be 3 elements
        Assert.Contains(10, items);
        Assert.Contains(20, items);
        Assert.Contains(30, items);
    }

    [Fact]
    public void Add_ShouldResizeAndMaintainOrder() {
        // Arrange
        var expandableArray = new ExpandableArray<int>(2);

        // Act
        expandableArray.Add(1);
        expandableArray.Add(2);
        expandableArray.Add(3); // Trigger resize

        // Assert
        Assert.Equal(3, expandableArray.Count);
        Assert.Equal(1, expandableArray[0]);
        Assert.Equal(2, expandableArray[1]);
        Assert.Equal(3, expandableArray[2]);
    }

    [Fact]
    public void Add_ShouldResizeMultipleTimes() {
        // Arrange
        var expandableArray = new ExpandableArray<int>(2);

        // Act
        expandableArray.Add(1);
        expandableArray.Add(2);
        expandableArray.Add(3); // Triggers resize to capacity 4
        expandableArray.Add(4);
        expandableArray.Add(5); // Triggers resize to capacity 8

        // Assert
        Assert.Equal(5, expandableArray.Count);
        Assert.Equal(1, expandableArray[0]);
        Assert.Equal(2, expandableArray[1]);
        Assert.Equal(3, expandableArray[2]);
        Assert.Equal(4, expandableArray[3]);
        Assert.Equal(5, expandableArray[4]);
    }
}
