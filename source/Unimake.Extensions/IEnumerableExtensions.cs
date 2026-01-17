using System.Linq;

namespace System.Collections.Generic
{
    /// <summary>
    /// Métodos de extensão para trabalhar com coleções que implementam <see cref="IEnumerable{T}"/>
    /// </summary>
    public static class IEnumerableExtensions
    {
        #region Public Methods

        /// <summary>
        /// Retorna elementos distintos de uma sequência usando uma função seletora de propriedade para comparação
        /// </summary>
        /// <typeparam name="TObject">O tipo dos elementos da sequência</typeparam>
        /// <param name="list">A sequência de valores da qual remover elementos duplicados</param>
        /// <param name="propertySelector">Função que seleciona a propriedade a ser usada para comparação de igualdade</param>
        /// <returns>Uma sequência contendo elementos distintos da sequência de origem</returns>
        public static IEnumerable<TObject> DistinctBy<TObject>(this IEnumerable<TObject> list, Func<TObject, object> propertySelector) => list.GroupBy(propertySelector).Select(matches => matches.First());

        /// <summary>
        /// Retorna uma lista de itens duplicados que foram encontrados na lista
        /// </summary>
        /// <typeparam name="T">Tipo de item que será percorrido</typeparam>
        /// <param name="values">Valores que serão percorridos</param>
        /// <param name="condition">Condição para saber se o item é duplicado</param>
        /// <returns>Lista com os itens duplicados</returns>
        public static IList<T> FindDuplicates<T>(this IEnumerable<T> values, Func<T, object> condition)
        {
            if(values.IsNullOrEmpty())
            {
                return new List<T>();
            }

            IList<T> result = values
                                  .GroupBy(i => condition(i))
                                  .SelectMany(g => g.Skip(1))
                                  .ToList<T>();
            return result;
        }

        /// <summary>
        /// Executa uma ação para cada elemento do array
        /// </summary>
        /// <typeparam name="T">O tipo dos elementos do array</typeparam>
        /// <param name="values">Array de valores que serão percorridos</param>
        /// <param name="action">Ação a ser executada em cada elemento</param>
        public static void ForEach<T>(this T[] values, Action<T> action)
        {
            if(values == null)
            {
                return;
            }

            foreach(var item in values)
            {
                action?.Invoke(item);
            }
        }

        /// <summary>
        /// Verifica se a sequência é nula ou vazia
        /// </summary>
        /// <typeparam name="TObject">O tipo dos elementos da sequência</typeparam>
        /// <param name="enumerable">Sequência a ser verificada</param>
        /// <returns>Verdadeiro se a sequência for nula ou não contiver elementos; caso contrário, falso</returns>
        public static bool IsNullOrEmpty<TObject>(this IEnumerable<TObject> enumerable) => enumerable == null || !enumerable.Any();

        /// <summary>
        /// Verifica se o array é nulo ou vazio
        /// </summary>
        /// <typeparam name="T">O tipo dos elementos do array</typeparam>
        /// <param name="arr">Array a ser verificado</param>
        /// <returns>Verdadeiro se o array for nulo ou não contiver elementos; caso contrário, falso</returns>
        public static bool IsNullOrEmpty<T>(this T[] arr) => arr == null || arr.Length == 0;

        /// <summary>
        /// Verifica se a lista é nula ou vazia
        /// </summary>
        /// <typeparam name="T">O tipo dos elementos da lista</typeparam>
        /// <param name="list">Lista a ser verificada</param>
        /// <returns>Verdadeiro se a lista for nula ou não contiver elementos; caso contrário, falso</returns>
        public static bool IsNullOrEmpty<T>(this IList<T> list) => list == null || list.Count == 0;

        /// <summary>
        /// Verifica se a coleção é nula ou vazia
        /// </summary>
        /// <param name="values">Coleção a ser verificada</param>
        /// <returns>Verdadeiro se a coleção for nula ou não contiver elementos; caso contrário, falso</returns>
        public static bool IsNullOrEmpty(this IEnumerable values) => values == null || !values.Cast<object>().Any();

        /// <summary>
        /// Concatena os valores da coleção utilizando vírgula como separador
        /// </summary>
        /// <param name="values">Valores a serem concatenados</param>
        /// <returns>String com os valores concatenados separados por vírgula</returns>
        public static string Join(this IEnumerable values) => Join(values, ',');

        /// <summary>
        /// Concatena os valores da coleção utilizando o separador especificado
        /// </summary>
        /// <param name="values">Valores a serem concatenados</param>
        /// <param name="separator">Caractere separador</param>
        /// <returns>String com os valores concatenados usando o separador especificado</returns>
        public static string Join(this IEnumerable values, char separator) => Join(values, separator.ToString());

        /// <summary>
        /// Concatena os valores da coleção utilizando o separador especificado
        /// </summary>
        /// <param name="values">Valores a serem concatenados</param>
        /// <param name="separator">String separadora</param>
        /// <returns>String com os valores concatenados usando o separador especificado</returns>
        public static string Join(this IEnumerable values, string separator)
        {
            if(values == null)
            {
                return "";
            }

            var result = "";

            foreach(var item in values)
            {
                result += string.Format("{0}{1}", item, separator);
            }

            if(result.Length > 0)
            {
                result = result.Substring(0, result.Length - separator.Length);
            }

            return result;
        }

        /// <summary>
        /// Remove todos os itens da lista onde o predicado retorna verdadeiro
        /// </summary>
        /// <typeparam name="T">Tipo de elemento da lista</typeparam>
        /// <param name="list">Lista que contém os itens a serem removidos</param>
        /// <param name="predicate">Função que determina se o item deve ser removido. Se retornar verdadeiro, o item é removido da lista</param>
        public static void RemoveAll<T>(this IList<T> list, Func<T, bool> predicate)
        {
            if(list.IsNullOrEmpty())
            {
                return;
            }

            var index = 0;
            while(index < list.Count)
            {
                if(predicate(list[index]))
                {
                    list.RemoveAt(index);
                }
                else
                {
                    index++;
                }
            }
        }

        /// <summary>
        /// Substitui todas as ocorrências de uma cadeia de caracteres por outra em cada elemento da sequência
        /// </summary>
        /// <param name="source">Sequência de strings de origem</param>
        /// <param name="oldValue">Texto a ser localizado e substituído em cada item</param>
        /// <param name="newValue">Texto que substituirá cada ocorrência de <paramref name="oldValue"/></param>
        /// <returns>Nova sequência de strings com as substituições aplicadas</returns>
        public static IEnumerable<string> Replace(this IEnumerable<string> source, string oldValue, string newValue)
        {
            foreach(var item in source)
            {
                yield return item.Replace(oldValue, newValue);
            }
        }

        /// <summary>
        /// Projeta cada elemento de uma sequência em uma tupla contendo o elemento e seu índice
        /// </summary>
        /// <typeparam name="TObject">O tipo dos elementos da sequência</typeparam>
        /// <param name="enumerable">Sequência de valores a ser indexada</param>
        /// <returns>Sequência de tuplas onde cada tupla contém o elemento original e seu índice na sequência</returns>
        public static IEnumerable<(TObject item, int index)> WithIndex<TObject>(this IEnumerable<TObject> enumerable) => enumerable.Select((item, index) => (item, index));

        #endregion Public Methods
    }
}