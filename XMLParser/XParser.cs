using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace XMLParser
{
    /// <summary>
    /// Класс для работы с XML
    /// </summary>
    public static class XParser
    {
        /// <summary>
        /// Возвращет nullable значение для элемента
        /// </summary>
        /// <typeparam name="T">Тип возвращаемого значения</typeparam>
        /// <param name="element">Элемент, значение которого хотим получить</param>
        /// <returns>Возвращает либо значение элемента, либо null</returns>
        public static T? GetXElementValueNullable<T>(XElement element) where T : struct
        {
            return element == null ? null : (T?)Convert.ChangeType(element.Value, typeof(T), CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Возвращет nullable значение для элемента
        /// </summary>
        /// <typeparam name="T">Тип возвращаемого значения</typeparam>
        /// <param name="element">Элемент, внутри которого будет происходить поиск тегов</param>
        /// <param name="tags">Список тегов без namespace, по которым надо пройти для достижения искомого тега с искомым значением. Последний элемент последовательности - искомый тег</param>
        /// <returns>Возвращает либо значение элемента, либо null</returns>
        public static T? GetXElementValueNullable<T>(XElement element, string[] tags) where T : struct
        {
            var res = GetXElementList(element, tags).FirstOrDefault();
            return res == null ? null : (T?)Convert.ChangeType(res.Value, typeof(T), CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Возвращет значение для элемента
        /// </summary>
        /// <typeparam name="T">Тип возвращаемого значения</typeparam>
        /// <param name="element">Элемент, значение которого хотим получить</param>
        /// <returns>Возвращает либо значение элемента, либо дефолтное значение типа</returns>
        public static T GetXElementValue<T>(XElement element)
        {
            return element == null ? default(T) : (T)Convert.ChangeType(element.Value, typeof(T), CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Возвращет значение для элемента
        /// </summary>
        /// <typeparam name="T">Тип возвращаемого значения</typeparam>
        /// <param name="element">Элемент, внутри которого будет происходить поиск тегов</param>
        /// <param name="tags">Список тегов без namespace, по которым надо пройти для достижения искомого тега с искомым значением. Последний элемент последовательности - искомый тег</param>
        /// <returns>Возвращает либо значение элемента, либо дефолтное значение типа</returns>
        public static T GetXElementValue<T>(XElement element, string[] tags)
        {
            var res = GetXElementList(element, tags).FirstOrDefault();
            return res == null ? default(T) : (T)Convert.ChangeType(res.Value, typeof(T), CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Возвращает список искомых тегов
        /// </summary>
        /// <param name="element">Элемент, внутри которого будет происходить поиск тегов</param>
        /// <param name="tags">Список тегов без namespace, по которым надо пройти для достижения искомого тега. Последний элемент последовательности - искомый тег</param>
        /// <returns>Возвращает список искомых тегов</returns>
        public static IEnumerable<XElement> GetXElementList(XElement element, string[] tags)
        {
            IEnumerable<XElement> node = new XElement[] { element };
            foreach (var tag in tags)
                node = node.Elements().Where(x => x.Name.LocalName == tag);
            return node;
        }
    }
}
