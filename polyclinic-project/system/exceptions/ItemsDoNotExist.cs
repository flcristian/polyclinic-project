namespace polyclinic_project.system.interfaces.exceptions;

public class ItemsDoNotExist : Exception
{
    public ItemsDoNotExist(string? message) : base(message) { }
}