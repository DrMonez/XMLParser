using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace XMLParser
{
    public static class XParser
    {
        public static T? GetXElementValueNullable<T>(XElement element) where T : struct
        {
            return element == null ? null : (T?)Convert.ChangeType(element.Value, typeof(T), CultureInfo.InvariantCulture);
        }

        public static T? GetXElementValueNullable<T>(XElement element, string[] tags) where T : struct
        {
            var res = GetXElementList(element, tags).FirstOrDefault();
            return res == null ? null : (T?)Convert.ChangeType(res.Value, typeof(T), CultureInfo.InvariantCulture);
        }

        public static T GetXElementValue<T>(XElement element)
        {
            return element == null ? default(T) : (T)Convert.ChangeType(element.Value, typeof(T), CultureInfo.InvariantCulture);
        }

        public static T GetXElementValue<T>(XElement element, string[] tags)
        {
            var res = GetXElementList(element, tags).FirstOrDefault();
            return res == null ? default(T) : (T)Convert.ChangeType(res.Value, typeof(T), CultureInfo.InvariantCulture);
        }

        public static IEnumerable<XElement> GetXElementList(XElement element, string[] tags)
        {
            IEnumerable<XElement> node = new XElement[] { element };
            foreach (var tag in tags)
                node = node.Elements().Where(x => x.Name.LocalName == tag);
            return node;
        }
    }
}
