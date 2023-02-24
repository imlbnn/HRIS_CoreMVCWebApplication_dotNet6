using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRIS.Application.Common.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<TResult> LeftOuterJoin<TLeft, TRight, TKey, TResult>(this IEnumerable<TLeft> left, IEnumerable<TRight> right, Func<TLeft, TKey> leftKey, Func<TRight, TKey> rightKey,
        Func<TLeft, TRight, TResult> result)
        {
            return left.GroupJoin(right, leftKey, rightKey, (l, r) => new { l, r })
                 .SelectMany(
                     o => o.r.DefaultIfEmpty(),
                     (l, r) => new { lft = l.l, rght = r })
                 .Select(o => result.Invoke(o.lft, o.rght));
        }
    }
}
