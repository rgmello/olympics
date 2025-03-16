using System.Collections;

namespace DataStructures;

public class HashTable<TKey, TValue> : IReadOnlyCollection<KeyValuePair<TKey, TValue>>
    where TKey : IEquatable<TKey> {
    // Fields
    private readonly int _capacityMultiplier;
    private LinkedList<KeyValuePair<TKey, TValue>>?[] _buckets;

    // Properties
    public int Count { get; private set; }
    private int Capacity => _buckets.Length;

    // Constructor
    public HashTable(int capacity = 0, int capacityMultiplier = 2) {
        ArgumentOutOfRangeException.ThrowIfNegative(capacity);
        ArgumentOutOfRangeException.ThrowIfLessThan(capacity, 2);

        _capacityMultiplier = capacityMultiplier;
        _buckets = new LinkedList<KeyValuePair<TKey, TValue>>[capacity];
        Count = 0;
    }

    #region Public Methods

    public void Add(TKey key, TValue value) {
        if (Count == Capacity) Resize();
        var index = GetKeyIndex(key);
        AddAtBucket(ref _buckets[index], new KeyValuePair<TKey, TValue>(key, value));
        Count++;
    }

    public bool TryGetValue(TKey key, out TValue? value) {
        var index = GetKeyIndex(key);
        var bucket = _buckets[index];

        if (bucket is null) {
            value = default;
            return false;
        }

        foreach (var pair in bucket) {
            if (!pair.Key.Equals(key)) continue;
            value = pair.Value;
            return true;
        }

        value = default;
        return false;
    }

    public TValue? TryGet(TKey key) {
        TryGetValue(key, out var value);
        return value;
    }

    #endregion

    #region Private Methods

    private int GetKeyIndex(TKey key, int? divisor = null) =>
        Math.Abs(key.GetHashCode()) % (divisor ?? Capacity);

    private static void AddAtBucket(ref LinkedList<KeyValuePair<TKey, TValue>>? bucket, KeyValuePair<TKey, TValue> pair) {
        bucket ??= [];
        bucket.AddLast(pair);
    }

    private void Resize() {
        var newBuckets = new LinkedList<KeyValuePair<TKey, TValue>>?[Capacity * _capacityMultiplier];
        foreach (var pair in this) {
            var newIndex = GetKeyIndex(pair.Key, newBuckets.Length);
            AddAtBucket(ref newBuckets[newIndex], pair);
        }

        _buckets = newBuckets;
    }

    #endregion

    #region Interface Implementations

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() {
        foreach (var linkedList in _buckets) {
            if (linkedList is null) continue;
            foreach (var pair in linkedList) {
                yield return pair;
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator() =>
        GetEnumerator();

    #endregion
}
