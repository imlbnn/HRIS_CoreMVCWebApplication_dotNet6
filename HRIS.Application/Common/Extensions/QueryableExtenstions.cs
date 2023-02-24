using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HRIS.Application.Common.Extensions
{
    public static class QueryableExtenstions
    {
        public static string ConvertToNullableNested(string expression, string result = "", int index = 0)
        {
            //Transforms => "a.b.c" to "(a != null ? (a.b != null ? a.b.c : null) : null)"
            if (string.IsNullOrEmpty(expression))
                return null;
            if (string.IsNullOrEmpty(result))
                result = expression;
            var properties = expression.Split(".");
            if (properties.Length == 0 || properties.Length - 1 == index)
                return result;
            var property = string.Join(".", properties.Take(index + 1));
            if (string.IsNullOrEmpty(property))
                return result;
            result = result.Replace(expression, $"{property} == null ? null : {expression}");
            return ConvertToNullableNested(expression, result, index + 1);
        }

        public static string HandleNullableOrderBy(this string orderBy)
        {
            if (string.IsNullOrEmpty(orderBy))
                return null;

            var _orderBys = orderBy.Split(",", StringSplitOptions.RemoveEmptyEntries);

            if (!_orderBys.Any())
                return null;

            var _transformedOrderBy = "";
            foreach (var _orderby in _orderBys)
            {
                _transformedOrderBy = _transformedOrderBy + $"{ConvertToNullableNested($"{_orderby}")}, ";
            }
            _transformedOrderBy = _transformedOrderBy.TrimEnd(' ').TrimEnd(',');
            return _transformedOrderBy;
        }
    }
}
