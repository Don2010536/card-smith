public interface IKeyValueEditor<T>
{
    public string Key { get; set; }
    public T OriginalValue { get; set; }
    public T Value { get; set; }

    public bool CanBeDeleted { get; set; }
}
