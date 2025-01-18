using MBKLibrary;

namespace MBKLibrary.Extensions;

public static class ListExtension
{
    public static T GetRandomFromList<T>(this List<T> list)
    {
        if (list == null || list.Count == 0)
        {
            throw new ArgumentException("List cannot be null or empty");
        }

        int index = Utility.Random.Next(list.Count);
        return list[index];
    }
}
