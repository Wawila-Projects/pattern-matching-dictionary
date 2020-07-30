using System;
public class PatternMatch<T> {
    protected readonly Predicate<T> _condition;
    public PatternMatch(Predicate<T> condition) {
        _condition = condition;
    }
    public bool Evaluate(object value, out int? hash) {
        if (!(value is T))  {
            hash = null;
            return false;
        }
        var result = _condition((T)value);
        if (!result) {
            hash = null;
        } else {
            hash = GetHashCode();
        }
        return result;
    }

    public static implicit operator PatternMatch<T>(Predicate<T> predicate) {
        return new PatternMatch<T>(predicate);
    }
}