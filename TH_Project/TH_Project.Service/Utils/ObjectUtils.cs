﻿using System;

namespace SscKiosk.Kitchen.Api.Data.Utils
{
    public static class ObjectUtils
    {
        public static TValue Clone<TValue>(this object src)
        {
            TValue clone = (TValue)Activator.CreateInstance(typeof(TValue));

            var sourceProps = src.GetType().GetProperties();
            foreach (var sourceProp in sourceProps)
            {
                try
                {
                    var cloneProp = clone.GetType().GetProperty(sourceProp.Name);

                    // check if clone has current prop
                    if (cloneProp == null)
                    {
                        continue;
                    }

                    var sourcePropValue = sourceProp.GetValue(src, null);

                    cloneProp.SetValue(clone, sourcePropValue, null);
                }
                catch { }
            }

            return clone;
        }
    }
}
