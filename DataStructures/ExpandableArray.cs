using System.Collections;

namespace DataStructures;

public class ExpandableArray<T> : IEnumerable<T> {
    // Fields
    private T[] _array;
    private readonly int _capacityMultiplier;

    // Properties
    public int Count { get; private set; }

    // Constructor
    public ExpandableArray(int initialCapacity = 1, int capacityMultiplier = 2) {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(initialCapacity);
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
        if (Count == _array.Length) Resize();
        this[Count++] = item;
    }

    #endregion

    #region Private Methods

    private void Resize() {
        var newArray = new T[_array.Length * _capacityMultiplier];
        for (var i = 0; i < _array.Length; i++)
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
