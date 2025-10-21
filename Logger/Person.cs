namespace Logger;

public abstract record Person : EntityBase
{
    public FullName FullName { get; init; }

    // This is implemented explicitly because it is clearer and more consistent with the IEntity contract that Name is a string 
    public override string Name => FullName.ToString();

    public Person(FullName fullName)
    {
        FullName = fullName;
    }
}