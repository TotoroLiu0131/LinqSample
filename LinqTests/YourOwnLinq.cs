using System;
using System.Collections.Generic;
using LinqTests;

internal static class YourOwnLinq
{
    public static IEnumerable<TResult> MySelect<TSource, TResult>(this IEnumerable<TSource> urls, Func<TSource, TResult> selector)
    {
        foreach (var url in urls)
        {
            yield return selector(url);
        }
    }

    public static bool MyAny<TSource>(this IEnumerable<TSource> sources)
    {
        return sources.GetEnumerator().MoveNext();
    }

    public static IEnumerable<TSource> MyDistinct<TSource>(this IEnumerable<TSource> sources)
    {
        //var tSourceLookup = new Dictionary<TSource, string>();
        var hashSet = new HashSet<TSource>();
        var enumerator = sources.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (hashSet.Add(enumerator.Current))
            {
                yield return enumerator.Current;
            }
            //if (!tSourceLookup.ContainsKey(enumerator.Current))
            //{
            //    tSourceLookup.Add(enumerator.Current,"");
            //    yield return enumerator.Current;
            //}
        }
    }

    public static bool MyAll<TSource>(this IEnumerable<TSource> sources, Func<TSource, bool> predicate)
    {
        var enumerator = sources.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (!predicate(enumerator.Current))
            {
                return false;
            }
        }
        return true;
    }

    public static IEnumerable<int> MyGroupSum<TSource>(this IEnumerable<TSource> sources, int group,
        Func<TSource, int> selector)
    {
        var counter = 0;
        var result = 0;

        foreach (var source in sources)
        {
            result += selector(source);
            counter++;
            if (counter == @group)
            {
                yield return result;
                counter = 0;
                result = 0;
            }
        }
        yield return result;
    }

    public static int MySum<TSource>(this IEnumerable<TSource> source, Func<TSource, int> selector)
    {
        var enumerator = source.GetEnumerator();
        var result = 0;
        while (enumerator.MoveNext())
        {
            result += selector(enumerator.Current);
        }

        return result;
    }

    public static IEnumerable<TResult> MySelect<TSource, TResult>(this IEnumerable<TSource> urls, Func<TSource, int, TResult> selector)
    {
        var index = 0;
        foreach (var url in urls)
        {
            yield return selector(url, index++);
        }
    }

    public static IEnumerable<TSource> MyWhere<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        var enumerator = source.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (predicate(enumerator.Current))
            {
                yield return enumerator.Current;
            }
        }
    }

    public static IEnumerable<TSource> MyWhere<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
    {
        var index = 0;
        var enumerator = source.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (predicate(enumerator.Current, index))
            {
                yield return enumerator.Current;
            }

            index++;
        }
    }

    public static IEnumerable<TSource> MyTake<TSource>(this IEnumerable<TSource> source, int count)
    {
        var counter = 0;
        var enumerator = source.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (counter++ == count)
            {
                yield break;
            }
            yield return enumerator.Current;
        }
    }

    public static IEnumerable<TSource> MyTakeWhile<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, int count)
    {
        var counter = 0;
        var enumerator = source.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (predicate(enumerator.Current) && counter < count)
            {
                yield return enumerator.Current;
                counter++;
            }
        }
    }

    public static IEnumerable<TSource> MyRealTakeWhile<TSource>(this IEnumerable<TSource> sources,
        Func<TSource, bool> predicate)
    {
        var enumerator = sources.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (predicate(enumerator.Current))
            {
                yield return enumerator.Current;
            }
            else
            {
                yield break;
            }
        }
    }

    public static IEnumerable<TSource> MySkip<TSource>(this IEnumerable<TSource> sources, int i)
    {
        var counter = 0;
        var enumerator = sources.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (counter++ >= i)
            {
                yield return enumerator.Current;
            }
        }
    }

    public static IEnumerable<TSource> MySkipWhile<TSource>(this IEnumerable<TSource> sources, int i, Func<TSource, bool> predicate)
    {
        var counter = 0;
        var enumerator = sources.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (!predicate(enumerator.Current) || counter >= i)
            {
                yield return enumerator.Current;
            }
            else
            {
                counter++;
            }
        }
    }

    public static bool MyAny<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        var enumerator = source.GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (predicate(enumerator.Current))
            {
                return true;
            }
        }
        return false;
        //foreach (var item in source)
        //{
        //    if (predicate(item))
        //    {
        //        return true;
        //    }
        //}
        //return false;
    }

    public static bool BenAll<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        var enumerator = source.GetEnumerator();

        while (enumerator.MoveNext())
        {
            var current = enumerator.Current;
            if (!(predicate(current)))
            {
                return false;
            }
        }

        return true;
    }

    public static TSource BenFirst<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        return BenFirst(source, predicate, default(TSource));
    }

    public static TSource BenFirst<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, TSource employee)
    {
        var enumerator = source.GetEnumerator();
        while (enumerator.MoveNext())
        {
            var current = enumerator.Current;
            if (predicate(current))
            {
                return current;
            }
        }
        return employee;
    }

    public static TSource OneAndOnlyOne<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
    {
        var enumerator = source.GetEnumerator();
        TSource ball = default(TSource);
        bool isMatch = false;
        while (enumerator.MoveNext())
        {
            var current = enumerator.Current;
            if (predicate(current))
            {
                if (isMatch)
                {
                    throw new InvalidOperationException();
                }
                ball = current;
                isMatch = true;
            }
        }

        return isMatch ? ball : throw new InvalidOperationException();
    }

    public static TSource MyLast<TSource>(this IEnumerable<TSource> balls, Func<TSource, bool> predicate)
    {
        var enumerator = balls.GetEnumerator();
        var last = default(TSource);
        while (enumerator.MoveNext())
        {
            var current = enumerator.Current;
            if (predicate(current))
            {
                last = current;
            }
        }

        return last;
    }

    public static bool BenIsMatch(IEnumerable<ColorBall> balls, ColorBall compare, BenCompare benCompare)
    {
        var enumerator = balls.GetEnumerator();
        while (enumerator.MoveNext())
        {
            var current = enumerator.Current;
            if (benCompare.Equals(current, compare))
            {
                return true;
            }
        }

        return false;
    }

    public static bool EmployeeMatch(List<Employee> first, IEnumerable<Employee> second, EmployeeCompare employeeCompare)
    {
        var firstEnumerator = first.GetEnumerator();
        var secondEnumerator = second.GetEnumerator();

        while (true)
        {
            var firstMoveNext = firstEnumerator.MoveNext();
            var secondMoveNext = secondEnumerator.MoveNext();

            if (secondMoveNext != firstMoveNext)
            {
                return false;
            }

            if (!firstMoveNext)
            {
                return true;
            }

            if (!employeeCompare.Equals(firstEnumerator.Current, secondEnumerator.Current))
            {
                return false;
            }
        }
    }
}