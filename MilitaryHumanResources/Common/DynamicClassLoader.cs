using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Linq.Expressions;

namespace MilitaryHumanResources.Common
{
    public class DynamicClassLoader
    {
        public static T LoadClassOldFunc<T>(DataRow dr, DynamicLoaderSettings settings = null)
        {
            settings = settings ?? new DynamicLoaderSettings();

            T Class = Activator.CreateInstance<T>();
            FieldInfo[] fields = typeof(T).GetFields();
            MemberInfo[] mem = typeof(T).GetMembers();
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (!dr.Table.Columns.Contains(property.Name)) continue; //Table not containes the field name
                if (string.IsNullOrEmpty(dr[property.Name].ToString().Trim())) continue; // The field is empty or null

                try
                {
                    if (property.PropertyType == typeof(string))
                        property.SetValue(Class, dr[property.Name].ToString());
                    else if (property.PropertyType == typeof(int) || property.PropertyType == typeof(int?))
                        property.SetValue(Class, int.Parse(dr[property.Name].ToString()));
                    else if (property.PropertyType == typeof(bool))
                        property.SetValue(Class, bool.Parse(dr[property.Name].ToString()));
                    else if (property.PropertyType == typeof(Boolean))
                        property.SetValue(Class, Boolean.Parse(dr[property.Name].ToString()));
                    else if (property.PropertyType == typeof(decimal) || property.PropertyType == typeof(decimal?))
                        property.SetValue(Class, decimal.Parse(dr[property.Name].ToString()));
                    else if (property.PropertyType == typeof(float) || property.PropertyType == typeof(float?))
                        property.SetValue(Class, float.Parse(dr[property.Name].ToString()));
                    else if (property.PropertyType == typeof(double) || property.PropertyType == typeof(double?))
                        property.SetValue(Class, double.Parse(dr[property.Name].ToString()));
                    else if (property.PropertyType == typeof(long) || property.PropertyType == typeof(long?))
                        property.SetValue(Class, long.Parse(dr[property.Name].ToString()));
                    else if (property.PropertyType == typeof(DateTime))
                    {
                        if (settings.ConvertDateTimeToString)
                            property.SetValue(Class, dr[property.Name].ToString());
                        else
                            property.SetValue(Class, DateTime.Parse(dr[property.Name].ToString()));
                    }
                    else if (property.PropertyType == typeof(DateTime?))
                    {
                        if (settings.ConvertDateTimeToString)
                            property.SetValue(Class, dr[property.Name].ToString());
                        else
                            property.SetValue(Class, dr.IsNull(property.Name) ? null : (DateTime?)DateTime.Parse(dr[property.Name].ToString()));
                    }
                    else if (property.PropertyType == typeof(Int16) || property.PropertyType == typeof(Int32) || property.PropertyType == typeof(Int64) || property.PropertyType == typeof(uint)
                        || property.PropertyType == typeof(UInt16) || property.PropertyType == typeof(UInt32) || property.PropertyType == typeof(UInt64)
                        || property.PropertyType == typeof(sbyte) || property.PropertyType == typeof(Single))
                        property.SetValue(Class, int.Parse(dr[property.Name].ToString()));
                    else property.SetValue(Class, dr[property.Name].ToString()); //Default set string
                }
                catch
                {
                    Console.WriteLine(string.Format("No casting for {0} type.", property.PropertyType));
                    if (property.PropertyType.IsValueType)
                    {
                        property.SetValue(Class, Activator.CreateInstance(property.PropertyType));
                    }
                }
            }
            foreach (FieldInfo field in fields)
            {
                if (!dr.Table.Columns.Contains(field.Name)) continue; //Table not containes the field name
                if (string.IsNullOrEmpty(dr[field.Name].ToString().Trim())) continue; // The field is empty or null

                try
                {
                    if (field.FieldType == typeof(string))
                        field.SetValue(Class, dr[field.Name].ToString());
                    else if (field.FieldType == typeof(int) || field.FieldType == typeof(int?))
                        field.SetValue(Class, int.Parse(dr[field.Name].ToString()));
                    else if (field.FieldType == typeof(bool))
                        field.SetValue(Class, bool.Parse(dr[field.Name].ToString()));
                    else if (field.FieldType == typeof(Boolean))
                        field.SetValue(Class, Boolean.Parse(dr[field.Name].ToString()));
                    else if (field.FieldType == typeof(decimal))
                        field.SetValue(Class, decimal.Parse(dr[field.Name].ToString()));
                    else if (field.FieldType == typeof(float))
                        field.SetValue(Class, float.Parse(dr[field.Name].ToString()));
                    else if (field.FieldType == typeof(double))
                        field.SetValue(Class, double.Parse(dr[field.Name].ToString()));
                    else if (field.FieldType == typeof(long))
                        field.SetValue(Class, long.Parse(dr[field.Name].ToString()));
                    else if (field.FieldType == typeof(DateTime))
                    {
                        if (settings.ConvertDateTimeToString)
                            field.SetValue(Class, dr[field.Name].ToString());
                        else
                            field.SetValue(Class, DateTime.Parse(dr[field.Name].ToString()));
                    }
                    else if (field.FieldType == typeof(DateTime?))
                    {
                        if (settings.ConvertDateTimeToString)
                            field.SetValue(Class, dr[field.Name].ToString());
                        else
                            field.SetValue(Class, dr.IsNull(field.Name) ? null : (DateTime?)DateTime.Parse(dr[field.Name].ToString()));
                    }
                    else if (field.FieldType == typeof(Int16) || field.FieldType == typeof(Int32) || field.FieldType == typeof(Int64) || field.FieldType == typeof(uint)
                        || field.FieldType == typeof(UInt16) || field.FieldType == typeof(UInt32) || field.FieldType == typeof(UInt64)
                        || field.FieldType == typeof(sbyte) || field.FieldType == typeof(Single))
                        field.SetValue(Class, int.Parse(dr[field.Name].ToString()));
                    else field.SetValue(Class, dr[field.Name].ToString()); //Default set string
                }
                catch
                {
                    Console.WriteLine(string.Format("No casting for {0} type.", field.FieldType));
                    if (field.FieldType.IsValueType)
                    {
                        field.SetValue(Class, Activator.CreateInstance(field.FieldType));
                    }
                }
            }
            return Class;
        }

        public static T LoadClass<T>(DataRow dr, DynamicLoaderSettings settings = null)
        {
            settings = settings ?? new DynamicLoaderSettings();

            T Class = Activator.CreateInstance<T>();
            FieldInfo[] fields = typeof(T).GetFields();
            MemberInfo[] mem = typeof(T).GetMembers();
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                if (!dr.Table.Columns.Contains(property.Name)) continue; //Table not containes the field name
                if (string.IsNullOrEmpty(dr[property.Name].ToString().Trim())) continue; // The field is empty or null

                try
                {
                    Type sourceType = dr[property.Name].GetType();
                    Type targetType = property.PropertyType;
                    MethodInfo method = typeof(DynamicClassLoader).GetMethod("ConvertValue", BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public |
                                                                                             BindingFlags.NonPublic);
                    MethodInfo genericMethod = method?.MakeGenericMethod(sourceType, targetType);
                    var result = genericMethod?.Invoke(null, new[] { dr[property.Name] });
                    property.SetValue(Class, result);                   
                }
                catch
                {
                    Console.WriteLine($"No casting for {property.PropertyType} type.");
                    if (property.PropertyType.IsValueType)
                    {
                        property.SetValue(Class, Activator.CreateInstance(property.PropertyType));
                    }
                }
            }

            foreach (FieldInfo field in fields)
            {
                if (!dr.Table.Columns.Contains(field.Name)) continue; //Table not containes the field name
                if (string.IsNullOrEmpty(dr[field.Name].ToString().Trim())) continue; // The field is empty or null

                try
                {
                    Type sourceType = dr[field.Name].GetType();
                    Type targetType = field.FieldType;
                    MethodInfo method = typeof(DynamicClassLoader).GetMethod("ConvertValue", BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public |
                                                                             BindingFlags.NonPublic);
                    MethodInfo genericMethod = method.MakeGenericMethod(sourceType, targetType);
                    var result = genericMethod.Invoke(null, new[]{ dr[field.Name] });
                    field.SetValue(Class, result);
                }
                catch
                {
                    Console.WriteLine($"No casting for {field.FieldType} type.");
                    if (field.FieldType.IsValueType)
                    {
                        field.SetValue(Class, Activator.CreateInstance(field.FieldType));
                    }
                }
            }
            return Class;
        }

        /// <summary>
        /// Set data in array of objects from DataTable.
        /// Example of call this function:
        ///       List<PackageType> res = (FillObjectArrayFromDataTable(dt, new PackageType())).Cast<PackageType>().ToList();
        /// </summary>
        /// <param name="table">DataTable containes data.</param>
        /// <param name="objType">The object class to create array from.</param>
        /// <returns>List of the object type</returns>
        public static List<T> LoadObjectList<T>(DataTable table, DynamicLoaderSettings settings = null)
        {
            // Create dynamic list
            List<T> list = new List<T>();
            if (table == null)
                return list;
            foreach (DataRow dr in table.Rows)
            {
                // Create new instanse of this object, must do this otherwise the object send by reference and all the list contains the last object in the DataTable
                var obj = Activator.CreateInstance(typeof(T));
                obj = LoadClass<T>(dr, settings);
                list.Add((T)obj);
            }
            return list;
        }

        /// <summary>
        /// Tries to convert value type from U to T.
        /// </summary>
        /// <typeparam name="T">Target Type</typeparam>
        /// <typeparam name="U">Source Type</typeparam>
        /// <param name="value">Source Value</param>
        /// <returns>Target Value</returns>
        /// <exception cref="InvalidCastException"></exception>
        /// <exception cref="FormatException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static T ConvertValue<T, U>(U value) where U : IConvertible
        {
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }

    public class DynamicLoaderSettings
    {
        public bool ConvertDateTimeToString = false;


    }
}
