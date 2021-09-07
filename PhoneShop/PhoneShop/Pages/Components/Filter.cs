using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PSAPI.Pages.Components
{
    public class Filter
    {
        public string Text { get; set; }
        public decimal?[] Price { get; set; } // 0 - nuo; 1 - iki
        public int?[] Number { get; set; } // 0 - nuo; 1 - iki
        public DateTime?[] Date { get; set; } // 0 - nuo; 1 - iki
        public bool? Bool { get; set; }
        public InputType Type { get; private set; }
        public int FieldIndex { get; private set; } // koki (kelinta) field/property atitinka sitas filtras FilterableSummableObject objekte (field'o indeksas)
        public bool IsDropDown { get; set; }
        public IEnumerable<object> DropDownValues { get; set; }

        public PropertyInfo FieldProterty { get; set; }
        public string FieldName { get; set; }

        public Filter(InputType type, int fieldIndex, bool isDropDown)
        {
            Type = type;
            FieldIndex = fieldIndex;
            IsDropDown = isDropDown;
            switch (type)
            {
                case InputType.Decimal:
                    Price = new decimal?[2] { null, null };
                    break;
                case InputType.Number:
                    Number = new int?[2] { null, null };
                    break;
                case InputType.DateTime:
                    Date = new DateTime?[2] { null, null };
                    break;
                case InputType.Text:
                    Text = string.Empty;
                    break;
            }
        }

        public Filter(PropertyInfo prop, bool isDropDown)
        {
            FieldProterty = prop;
            FieldName = prop.Name;
            IsDropDown = isDropDown;
            if (prop.PropertyType == typeof(decimal) || prop.PropertyType == typeof(decimal?))
            {
                Price = new decimal?[2] { null, null };
                Type = InputType.Decimal;
            }
            else if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?))
            {
                Number = new int?[2] { null, null };
                Type = InputType.Number;
            }
            else if (prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(DateTime?))
            {
                Date = new DateTime?[2] { null, null };
                Type = InputType.DateTime;
            }
            else if (prop.PropertyType == typeof(string))
            {
                Text = string.Empty;
                Type = InputType.Text;
            }
            else if (prop.PropertyType == typeof(bool) || prop.PropertyType == typeof(bool?))

                Type = InputType.Checkbox;
        }
        public bool IsSet() // Ar abu (nuo ir iki) laukai nustatyti
        {
            switch (Type)
            {
                case InputType.Text:
                    return (Text.Length > 0);
                case InputType.Decimal:
                    return (!Price.Any(x => !x.HasValue));
                case InputType.Number:
                    return (!Number.Any(x => !x.HasValue));
                case InputType.DateTime:
                    return (!Date.Any(x => x is null));
                case InputType.Checkbox:
                    return Bool.HasValue;
            }
            return false;
        }

        public bool IsPartiallySet() // Ar bent vienas (nuo arba iki) laukas nustatytas
        {
            if (IsDropDown)
                return DropDownValues != null;

            switch (Type)
            {
                case InputType.Text:
                    return !String.IsNullOrEmpty(Text);
                case InputType.Decimal:
                    return Price.Any(x => x.HasValue);
                case InputType.Number:
                    return Number.Any(x => x.HasValue);
                case InputType.DateTime:
                    return Date.Any(x => x != null);
                case InputType.Checkbox:
                    return Bool.HasValue;
            }
            return false;
        }

        public void Clear()
        {
            if (IsDropDown)
                DropDownValues = null;
            else
                switch (Type)
                {
                    case InputType.Text:
                        Text = string.Empty;
                        break;
                    case InputType.Decimal:
                        Price = new decimal?[2] { null, null };
                        break;
                    case InputType.Number:
                        Number = new int?[2] { null, null };
                        break;
                    case InputType.DateTime:
                        Date = new DateTime?[2] { null, null };
                        break;
                    case InputType.Checkbox:
                        Bool = null;
                        break;
                }
        }

        public void FixFromTo() // jei nuo > iki, tada apkeiciu nuo su iki vietomis (user error)
        {
            switch (Type)
            {
                case InputType.Decimal:
                    if (Price[0] > Price[1])
                    {
                        var temp = Price[0];
                        Price[0] = Price[1];
                        Price[1] = temp;
                    }
                    break;
                case InputType.Number:
                    if (Number[0] > Number[1])
                    {
                        var temp = Number[0];
                        Number[0] = Number[1];
                        Number[1] = temp;
                    }
                    break;
                case InputType.DateTime:
                    if (Date[0] > Date[1])
                    {
                        var temp = Date[0];
                        Date[0] = Date[1];
                        Date[1] = temp;
                    }
                    break;
            }
        }

        public bool SatisfiesFilter<TElement>(TElement item, bool filterStringCaseSensitive) // ar objektas "item" atitinka nustatyta filtra
        {
            if (IsDropDown)
            {
                if (DropDownValues != null && DropDownValues.Any())
                {
                    switch (Type)
                    {
                        case InputType.DateTime:
                            foreach (var val in DropDownValues)
                            {
                                if ((DateTime)val == (DateTime)FieldProterty.GetValue(item))
                                    return true;
                            }
                            return false;

                        case InputType.Decimal:
                            foreach (var val in DropDownValues)
                            {
                                if ((decimal)val == (decimal)FieldProterty.GetValue(item))
                                    return true;
                            }
                            return false;

                        case InputType.Number:
                            foreach (var val in DropDownValues)
                            {
                                if ((int)val == (int)FieldProterty.GetValue(item))
                                    return true;
                            }
                            return false;

                        case InputType.Text:
                            foreach (var val in DropDownValues)
                            {
                                if ((string)val == (string)FieldProterty.GetValue(item))
                                    return true;
                            }
                            return false;
                    }
                }
                else // jei DropDown bet jame nera nustatytu reiksmiu
                    return true;
            }
            else // jei ne DropDown
            {
                switch (Type)
                {
                    case InputType.Text:
                        var text = FieldProterty.GetValue(item);
                        if (text == null || (text.ToString() ?? "").Length == 0)
                            return false;

                        if (filterStringCaseSensitive)
                            return ((string)FieldProterty.GetValue(item)).Contains(Text);

                        return ((string)FieldProterty.GetValue(item)).ToLower().Contains(Text.ToLower());

                    case InputType.Number:
                        if (((int?)FieldProterty.GetValue(item)).HasValue)
                        {
                            if (Number[0].HasValue && Number[1].HasValue) // jei filtruojama nuo - iki 
                            {
                                FixFromTo(); // jei nuo > iki, tada apkeiciu nuo su iki vietomis (user error)
                                return (int)FieldProterty.GetValue(item) >= Number[0].Value && (int)FieldProterty.GetValue(item) <= Number[1].Value;
                            }
                            else if (Number[0] != null) // jei nustatytas tik "nuo" filtras
                            {
                                return (int)FieldProterty.GetValue(item) >= Number[0].Value;
                            }
                            else // jei nustatytas tik "iki" filtras
                            {
                                return (int)FieldProterty.GetValue(item) <= Number[1].Value;
                            }
                        }
                        else
                            return false;

                    case InputType.DateTime:
                        if (((DateTime?)FieldProterty.GetValue(item)).HasValue)
                        {
                            if (Date[0].HasValue && Date[1].HasValue) // jei filtruojama nuo - iki 
                            {
                                FixFromTo(); // jei nuo > iki, tada apkeiciu nuo su iki vietomis (user error)
                                return ((DateTime)FieldProterty.GetValue(item)).Date >= Date[0].Value.Date && ((DateTime)FieldProterty.GetValue(item)).Date <= Date[1].Value.Date;
                            }
                            else if (Date[0].HasValue) // jei nustatytas tik "nuo" filtras
                            {
                                return ((DateTime)FieldProterty.GetValue(item)).Date >= Date[0].Value.Date;
                            }
                            else // jei nustatytas tik "iki" filtras
                            {
                                return ((DateTime)FieldProterty.GetValue(item)).Date <= Date[1].Value.Date;
                            }
                        }
                        else
                            return false;

                    case InputType.Decimal:
                        if (((decimal?)FieldProterty.GetValue(item)).HasValue)
                        {
                            if (Price[0].HasValue && Price[1].HasValue) // jei filtruojama nuo - iki 
                            {
                                FixFromTo(); // jei nuo > iki, tada apkeiciu nuo su iki vietomis (user error)
                                return (decimal)FieldProterty.GetValue(item) >= Price[0].Value && (decimal)FieldProterty.GetValue(item) <= Price[1].Value;
                            }
                            else if (Price[0].HasValue) // jei nustatytas tik "nuo" filtras
                            {
                                return (decimal)FieldProterty.GetValue(item) >= Price[0].Value;
                            }
                            else // jei nustatytas tik "iki" filtras
                            {
                                return (decimal)FieldProterty.GetValue(item) <= Price[1].Value;
                            }
                        }
                        else
                            return false;

                    case InputType.Checkbox:
                        if (((bool?)FieldProterty.GetValue(item)).HasValue)
                        {
                            return ((bool)FieldProterty.GetValue(item) == Bool);
                        }
                        else
                            return false;
                }
            }
            return false;
        }
    }
}
