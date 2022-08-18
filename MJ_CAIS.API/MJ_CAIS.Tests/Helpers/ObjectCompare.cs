using System;
using System.Collections;
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
                var firstCollection = (IList)firsObjVal;
                var secondCollection = (IList)secondObjVal;

                var firstCollectionCount = firstCollection.Count;
                var secondCollectionCount = secondCollection.Count;
                if (firstCollectionCount != secondCollectionCount)
                {
                    _isEquals = false;
                    GetPathOfDifference = pathToRoot;
                    return;
                }

                var i = 0;
                foreach (var listItem in firstCollection)
                {
                    var childPathToRoot = $"{pathToRoot}[{i}]";

                    var itemType = listItem.GetType();
                    var itemProperties = itemType.GetProperties().ToList();

                    foreach (var itemProperty in itemProperties)
                    {
                        CompareChildren(itemProperty, listItem, secondCollection[i], childPathToRoot + "." + itemProperty.Name);
                        if (!_isEquals) return;
                    }
                    i++;
                }
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
    }
}
