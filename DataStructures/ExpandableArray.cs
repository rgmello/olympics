using System.Collections;

namespace DataStructures;

public class ExpandableArray<T> : IReadOnlyCollection<T> {
    // Fields
    private T[] _array;
    private readonly int _capacityMultiplier;

    // Properties
    public int Count { get; private set; }
    private int Capacity => _array.Length;

    // Constructor
    public ExpandableArray(int initialCapacity = 0, int capacityMultiplier = 2) {
        ArgumentOutOfRangeException.ThrowIfNegative(initialCapacity);
        ArgumentOutOfRangeException.ThrowIfLessThan(capacityMultiplier, 2);

        _array = new T[initialCapacity];
        _capacityMultiplier = capacityMultiplier;
        Count = 0;
    }

    #region Public Methods

    public T this[int index] {
        get {
            ArgumentOutOfRangeException.ThrowIfNegative(index);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Count);
            return _array[index];
        }
        set {
            ArgumentOutOfRangeException.ThrowIfNegative(index);
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(index, Count);
            _array[index] = value;
        }
    }

    public void Add(T item) {
        if (Count == Capacity) Resize();
        this[Count++] = item;
    }

    #endregion

    #region Private Methods

    private void Resize() {
        var newArray = new T[Capacity > 0 ? Capacity * _capacityMultiplier : 1];
        for (var i = 0; i < Count; i++)
            newArray[i] = this[i];
        _array = newArray;
    }

    #endregion
    
    #region Interface Implementations

    public IEnumerator<T> GetEnumerator() {
        for (var i = 0; i < Count; i++)
            yield return this[i];
    }

    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();
    
    #endregion
}
