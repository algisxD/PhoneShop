using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PSAPI.Pages.Components
{
    public class ObjectComparer<TElement> : IComparer<TElement>
    {
        Dictionary<string, bool> SortedFields { get; set; } // string - field'o name, pagal kuri rikiuojama, bool - ascending?
        List<string> FieldKeys { get; set; } // tie patys field names (SortedFields keys), tik kad surikiuoti tokia tvarka, kokia buvo dedami
        IEnumerable<PropertyInfo> Properties;
        public bool SortingIsSet => SortedFields.Any();


        public ObjectComparer(IEnumerable<PropertyInfo> properties)
        {
            SortedFields = new Dictionary<string, bool>();
            FieldKeys = new List<string>();
            Properties = properties;
        }

        public int SortingForColumn(string columnId) // 0 jei nera sortinimo, 1 jei ascending, -1 jei descending
        {
            if (FieldKeys.Contains(columnId))
            {
                if (SortedFields[columnId])
                    return 1;
                else
                    return -1;
            }
            else
                return 0;
        }

        public void RemmoveSorting(string fieldId)
        {
            if (FieldKeys.Any(x => x == fieldId))
            {
                SortedFields.Remove(fieldId);
                FieldKeys.Remove(fieldId);
            }
        }

        public void ToggleSorting(string fieldId)
        {
            if (FieldKeys.Contains(fieldId))
            {
                if (SortedFields[fieldId])
                    SortedFields[fieldId] = false;
                else
                {
                    SortedFields.Remove(fieldId);
                    FieldKeys.Remove(fieldId);
                }
            }
            else
            {
                SortedFields.Add(fieldId, true);
                FieldKeys.Add(fieldId);
            }
        }

        public int Compare(TElement a, TElement b)
        {
            return CompareByColumn(FieldKeys.FirstOrDefault(), a, b);
        }

        private int CompareFields(string fieldId, TElement a, TElement b)
        {
            //switch (DataTemplate.ElementAt(fieldId).Type)
            //{
            //    case InputType.Checkbox:
            //        if ((bool)a.Values[fieldId] == (bool)b.Values[fieldId])
            //            return 0;
            //        else if ((bool)a.Values[fieldId])
            //            return (SortedFields[fieldId] ? 1 : (-1)) * (-1);
            //        else
            //            return (SortedFields[fieldId] ? 1 : (-1)) * 1;

            //    case InputType.DateTime:
            //        return (SortedFields[fieldId] ? 1 : (-1)) * DateTime.Compare((DateTime)(a.Values[fieldId] ?? DateTime.MinValue), (DateTime)(b.Values[fieldId] ?? DateTime.MinValue));

            //    case InputType.Decimal:
            //        return (SortedFields[fieldId] ? 1 : (-1)) * decimal.Compare((decimal)(a.Values[fieldId] ?? decimal.MinValue), (decimal)(b.Values[fieldId] ?? decimal.MinValue));

            //    case InputType.Number:
            //        return (SortedFields[fieldId] ? 1 : (-1)) * ((int)(a.Values[fieldId] ?? int.MinValue)).CompareTo((int)(b.Values[fieldId] ?? int.MinValue));

            //    case InputType.Text:
            //        return (SortedFields[fieldId] ? 1 : (-1)) * String.Compare((string)(a.Values[fieldId] ?? ""), (string)(b.Values[fieldId] ?? ""));
            //}
            //return 0;
            var prop = Properties.FirstOrDefault(e => e.Name == fieldId);

            if (prop.PropertyType == typeof(bool) || prop.PropertyType == typeof(bool?))
            {
                //if ((bool)a.Values[fieldId] == (bool)b.Values[fieldId])
                //    return 0;
                if ((bool)prop.GetValue(a) == (bool)prop.GetValue(b))
                    return 0;
                else if ((bool)prop.GetValue(a))
                    return (SortedFields[fieldId] ? 1 : (-1)) * (-1);
                else
                    return (SortedFields[fieldId] ? 1 : (-1)) * 1;
            }
            else if (prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTime?))
            {
                return (SortedFields[fieldId] ? 1 : (-1)) * DateTime.Compare((DateTime)(prop.GetValue(a) ?? DateTime.MinValue), (DateTime)(prop.GetValue(b) ?? DateTime.MinValue));
            }
            else if (prop.PropertyType == typeof(decimal) || prop.PropertyType == typeof(decimal?))
            {
                return (SortedFields[fieldId] ? 1 : (-1)) * decimal.Compare((decimal)(prop.GetValue(a) ?? decimal.MinValue), (decimal)(prop.GetValue(b) ?? decimal.MinValue));
            }
            else if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?))
            {
                return (SortedFields[fieldId] ? 1 : (-1)) * ((int)(prop.GetValue(a) ?? int.MinValue)).CompareTo((int)(prop.GetValue(b) ?? int.MinValue));
            }
            else if (prop.PropertyType == typeof(string))
            {
                return (SortedFields[fieldId] ? 1 : (-1)) * String.Compare((string)(prop.GetValue(a) ?? ""), (string)(prop.GetValue(b) ?? ""));
            }
            return 0;
        }

        private int CompareByColumn(string columnId, TElement a, TElement b)
        {
            int compared = CompareFields(columnId, a, b);
            if (compared == 0)
            {
                var keyIndex = FieldKeys.IndexOf(columnId) + 1;
                if (FieldKeys.Count > keyIndex)
                    return CompareByColumn(FieldKeys.ElementAt(keyIndex), a, b);
                else
                    return 0;
            }
            else
                return compared;
        }
    }
}
