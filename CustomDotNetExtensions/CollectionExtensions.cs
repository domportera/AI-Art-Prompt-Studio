using Godot;
using System;
using System.Collections;
using System.Collections.Generic;

public static class CollectionExtensions
{
    /// <summary>
    /// An optimized way to call foreach on a list. Uses a normal for loop.
    /// Note that just like a regular foreach loop, you cannot modify this collection from within your action,
    /// i.e. by assigning a new instance or value to your action's parameter.
    /// This function won't warn you about it, but you may get exceptions.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="perItemAction">Action to be called on each item in the list</param>
    public static void ForEach<T>(this List<T> list, Action<T> perItemAction) where T : class
    {
        for(int i = 0; i < list.Count; i++)
        {
            perItemAction(list[i]);
        }
    }

    /// <summary>
    /// An optimized way to call foreach on a list. Uses a normal for loop.
    /// Note that just like a regular foreach loop, you cannot modify this collection from within your action,
    /// i.e. by assigning a new instance or value to your action's parameter, or by modifying this list manually using the provided index.
    /// This function won't warn you about it, but you may get exceptions.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="perItemActionWithIndex">Action to be called on each item in the list. The integer parameter is the index in the for loop.</param>
    public static void ForEach<T>(this List<T> list, Action<T, int> perItemActionWithIndex) where T : class
    {
        for (int i = 0; i < list.Count; i++)
        {
            perItemActionWithIndex(list[i], i);
        }
    }

    /// <summary>
    /// Ever hate writing ".Count - 1"?
    /// </summary>
    public static int LastIndex<T>(this List<T> list)
    {
        return list.Count - 1;
    }

    /// <summary>
    /// Ever hate writing ".Length - 1"?
    /// </summary>
    public static int LastIndex(this Array array)
    {
        return array.Length - 1;
    }
}
