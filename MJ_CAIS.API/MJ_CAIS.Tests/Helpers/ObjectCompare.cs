using Microsoft.EntityFrameworkCore;
using MJ_CAIS.DataAccess;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MJ_CAIS.Tests.Helpers
{
    internal class ObjectCompare<TClass>
        where TClass : class
    {
        private bool _isEquals = true;
        private readonly TClass _firstObj;
        private readonly TClass _secondObj;

        public ObjectCompare(TClass firsObj, TClass secondObj)
        {
            this._firstObj = firsObj;
            this._secondObj = secondObj;
        }

        public bool IsEquals(string pathToRoot = null)
        {
            var path = !string.IsNullOrEmpty(pathToRoot) ? pathToRoot : "root";

            var properties = typeof(TClass).GetProperties().ToList();
            foreach (var property in properties)
            {
                var pathWithChild = path + "." + property.Name;

                CompareChildren(property, _firstObj, _secondObj, pathWithChild);
                if (!_isEquals) return false;
            }

            return _isEquals;
        }

        public string GetPathOfDifference { get; private set; }

        private void CompareChildren(PropertyInfo currentProperty, object firstObj, object seconObj, string pathToRoot)
        {
            if (currentProperty.Name == nameof(IBaseIdEntity.Id) || 
                currentProperty.Name == nameof(EntityState)) return;

            var firsObjVal = currentProperty.GetValue(firstObj);
            var secondObjVal = currentProperty.GetValue(seconObj);

            if (firsObjVal == null && secondObjVal == null) return;

            var ifSomePropIsNullButOtherIsNot = (firsObjVal == null && secondObjVal != null) ||
                                                (secondObjVal == null && firsObjVal != null);

            if (ifSomePropIsNullButOtherIsNot)
            {
                this._isEquals = false;
                GetPathOfDifference = pathToRoot;
                return;
            }

            var objType = currentProperty.PropertyType;

            if (IsSimpleTypeOrString(objType))
            {
                if (firsObjVal.Equals(secondObjVal)) return;

                _isEquals = false;
                GetPathOfDifference = pathToRoot;
                return;
            }

            if (typeof(IEnumerable).IsAssignableFrom(objType))
            {
                var firstCollection = (IEnumerable)firsObjVal;
                var secondCollection = (IEnumerable)secondObjVal;

                var firstCollectionCount = GetEnumerableCount(firstCollection);
                var secondCollectionCount = GetEnumerableCount(secondCollection);

                if (firstCollectionCount != secondCollectionCount)
                {
                    _isEquals = false;
                    GetPathOfDifference = pathToRoot;
                    return;
                }

                var firstI = 0;
                foreach (var firstItem in firstCollection)
                {
                    var secondI = 0;
                    foreach (var secondItem in secondCollection)
                    {
                        if (firstI == secondI)
                        {
                            var childPathToRoot = $"{pathToRoot}[{secondI}]";

                            var itemType = firstItem.GetType();
                            var itemProperties = itemType.GetProperties().ToList();

                            foreach (var itemProperty in itemProperties)
                            {
                                CompareChildren(itemProperty, firstItem, secondItem, childPathToRoot + "." + itemProperty.Name);
                                if (!_isEquals) return;
                            }
                        }
                        secondI++;
                    }

                    firstI++;
                }

                return;
            }

            var properties = objType.GetProperties();

            foreach (var property in properties)
            {
                var path = pathToRoot + "." + property.Name;
                CompareChildren(property, firsObjVal, secondObjVal, path);
                if (!_isEquals) return;
            }
        }

        private static bool IsSimpleTypeOrString(Type type)
        {
            var isPrimitive = type.IsValueType || type.IsPrimitive || Convert.GetTypeCode(type) != TypeCode.Object;
            var simpleTypes = new Type[] { typeof(decimal), typeof(DateTime), typeof(DateTimeOffset), typeof(TimeSpan), typeof(Guid) };

            var result = isPrimitive || simpleTypes.Contains(type) || typeof(string) == type;
            return result;
        }

        private static int GetEnumerableCount(IEnumerable collection)
        {
            var count = 0;
            foreach (var item in collection)
            {
                count++;
            }

            return count;
        }
    }
}
