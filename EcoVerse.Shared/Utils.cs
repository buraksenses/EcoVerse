namespace EcoVerse.Shared;

public static class Utils
{
    public static bool BeAValidGuid(Guid accountId)
    {
        return accountId != Guid.Empty && accountId.ToString().Length == 36;
    }
}