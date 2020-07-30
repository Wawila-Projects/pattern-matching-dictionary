using System;
using System.Collections.Generic;

class PatternMatchingDictionary<TKey, TValue> 
{
    protected Dictionary<int, TValue> _dictionary;
    protected List<PatternMatch<TKey>> _conditions;
    protected TValue defaultValue;
    protected bool defaultValueAssigned = false;

    public PatternMatchingDictionary()
    {
        _dictionary = new Dictionary<int, TValue>();
        _conditions = new List<PatternMatch<TKey>>();
    }

    public PatternMatchingDictionary(IEnumerable<(TValue, PatternMatch<TKey>)> values) {
        _dictionary = new Dictionary<int, TValue>();
        _conditions = new List<PatternMatch<TKey>>();
        foreach (var (value, pattern) in values) {
            AddCondition(value, pattern);
        }
    }

    public void DefaultValue(TValue value, bool OverwriteIfSet = true) {
        if (!OverwriteIfSet && defaultValueAssigned)
            return;
        defaultValueAssigned = true;
        defaultValue = value;
    }
    
    public void AddCondition(TValue value, PatternMatch<TKey> condition) {
        _conditions.Add(condition);
        _dictionary[condition.GetHashCode()] = value;
    }

    public TValue this[TKey i] {
        get {
            
            foreach (var condition in _conditions) {
                if (condition.Evaluate(i, out int? hash)){
                    return _dictionary[hash ?? default(int)];
                }    
            }
            if (defaultValueAssigned) {
                return defaultValue;
            }
            throw new System.Exception(
                $"{i} matches no case and no defualt value assigned."
            );
        }
    }
    // 852 bathroom

}