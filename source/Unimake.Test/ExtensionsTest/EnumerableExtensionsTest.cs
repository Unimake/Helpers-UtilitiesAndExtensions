using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Unimake.Helpers_UtilitiesAndExtensions.Test.ExtensionsTest
{
    public class EnumerableExtensionsTest
    {
        #region Private Classes

        private class Person
        {
            #region Public Properties

            public int Age { get; set; }
            public string Name { get; set; }

            #endregion Public Properties
        }

        #endregion Private Classes

        #region Public Methods

        [Fact]
        public void DistinctBy_DeveRetornarElementosDistintos_QuandoPropriedadeForEspecificada()
        {
            var pessoas = new List<Person>
            {
                new Person { Name = "João", Age = 30 },
                new Person { Name = "Maria", Age = 25 },
                new Person { Name = "João", Age = 35 },
                new Person { Name = "Pedro", Age = 40 }
            };

            var resultado = pessoas.DistinctBy(p => p.Name).ToList();

            Assert.Equal(3, resultado.Count);
            Assert.Contains(resultado, p => p.Name == "João" && p.Age == 30);
            Assert.Contains(resultado, p => p.Name == "Maria");
            Assert.Contains(resultado, p => p.Name == "Pedro");
        }

        [Fact]
        public void DistinctBy_DeveRetornarListaVazia_QuandoListaForVazia()
        {
            var lista = new List<Person>();
            var resultado = lista.DistinctBy(p => p.Name).ToList();

            Assert.Empty(resultado);
        }

        [Fact]
        public void FindDuplicates_DeveRetornarDuplicados_QuandoExistiremItemsDuplicados()
        {
            var numeros = new List<int> { 1, 2, 3, 2, 4, 3, 5 };

            var duplicados = numeros.FindDuplicates(n => n);

            Assert.Equal(2, duplicados.Count);
            Assert.Contains(2, duplicados);
            Assert.Contains(3, duplicados);
        }

        [Fact]
        public void FindDuplicates_DeveRetornarDuplicadosComPropriedade_QuandoUsarObjetos()
        {
            var pessoas = new List<Person>
            {
                new Person { Name = "João", Age = 30 },
                new Person { Name = "Maria", Age = 25 },
                new Person { Name = "João", Age = 35 },
                new Person { Name = "João", Age = 40 }
            };

            var duplicados = pessoas.FindDuplicates(p => p.Name);

            Assert.Equal(2, duplicados.Count);
            Assert.All(duplicados, p => Assert.Equal("João", p.Name));
        }

        [Fact]
        public void FindDuplicates_DeveRetornarListaVazia_QuandoListaForNula()
        {
            List<int> numeros = null;

            var duplicados = numeros.FindDuplicates(n => n);

            Assert.Empty(duplicados);
        }

        [Fact]
        public void FindDuplicates_DeveRetornarListaVazia_QuandoNaoHouverDuplicados()
        {
            var numeros = new List<int> { 1, 2, 3, 4, 5 };

            var duplicados = numeros.FindDuplicates(n => n);

            Assert.Empty(duplicados);
        }

        [Fact]
        public void ForEach_DeveExecutarAcaoParaCadaElemento()
        {
            var numeros = new[] { 1, 2, 3, 4, 5 };
            var soma = 0;

            numeros.ForEach(n => soma += n);

            Assert.Equal(15, soma);
        }

        [Fact]
        public void ForEach_NaoDeveLancarExcecao_QuandoAcaoForNula()
        {
            var numeros = new[] { 1, 2, 3 };

            var exception = Record.Exception(() => numeros.ForEach(null));

            Assert.Null(exception);
        }

        [Fact]
        public void ForEach_NaoDeveLancarExcecao_QuandoArrayForNulo()
        {
            int[] numeros = null;
            var soma = 0;

            numeros.ForEach(n => soma += n);

            Assert.Equal(0, soma);
        }

        [Fact]
        public void IsNullOrEmpty_Array_DeveRetornarFalse_QuandoArrayTiverElementos()
        {
            var array = new[] { 1, 2, 3 };

            Assert.False(array.IsNullOrEmpty());
        }

        [Fact]
        public void IsNullOrEmpty_Array_DeveRetornarTrue_QuandoArrayForNulo()
        {
            int[] array = null;

            Assert.True(array.IsNullOrEmpty());
        }

        [Fact]
        public void IsNullOrEmpty_Array_DeveRetornarTrue_QuandoArrayForVazio()
        {
            var array = new int[0];

            Assert.True(array.IsNullOrEmpty());
        }

        [Fact]
        public void IsNullOrEmpty_DeveRetornarFalse_QuandoIEnumerableTiverElementos()
        {
            var lista = new List<int> { 1, 2, 3 };

            Assert.False(lista.IsNullOrEmpty());
        }

        [Fact]
        public void IsNullOrEmpty_DeveRetornarTrue_QuandoIEnumerableForNulo()
        {
            IEnumerable<int> lista = null;

            Assert.True(lista.IsNullOrEmpty());
        }

        [Fact]
        public void IsNullOrEmpty_DeveRetornarTrue_QuandoIEnumerableForVazio()
        {
            var lista = new List<int>();

            Assert.True(lista.IsNullOrEmpty());
        }

        [Fact]
        public void IsNullOrEmpty_IEnumerable_DeveRetornarFalse_QuandoColecaoTiverElementos()
        {
            IEnumerable colecao = new ArrayList { 1, 2, 3 };

            Assert.False(colecao.IsNullOrEmpty());
        }

        [Fact]
        public void IsNullOrEmpty_IEnumerable_DeveRetornarTrue_QuandoColecaoForNula()
        {
            IEnumerable colecao = null;

            Assert.True(colecao.IsNullOrEmpty());
        }

        [Fact]
        public void IsNullOrEmpty_IEnumerable_DeveRetornarTrue_QuandoColecaoForVazia()
        {
            IEnumerable colecao = new ArrayList();

            Assert.True(colecao.IsNullOrEmpty());
        }

        [Fact]
        public void IsNullOrEmpty_IList_DeveRetornarFalse_QuandoListaTiverElementos()
        {
            IList<string> lista = new List<string> { "a", "b", "c" };

            Assert.False(lista.IsNullOrEmpty());
        }

        [Fact]
        public void IsNullOrEmpty_IList_DeveRetornarTrue_QuandoListaForNula()
        {
            IList<string> lista = null;

            Assert.True(lista.IsNullOrEmpty());
        }

        [Fact]
        public void IsNullOrEmpty_IList_DeveRetornarTrue_QuandoListaForVazia()
        {
            IList<string> lista = new List<string>();

            Assert.True(lista.IsNullOrEmpty());
        }

        [Fact]
        public void Join_DeveConcatenarComSeparadorChar()
        {
            var palavras = new[] { "um", "dois", "três" };

            var resultado = palavras.Join(';');

            Assert.Equal("um;dois;três", resultado);
        }

        [Fact]
        public void Join_DeveConcatenarComSeparadorString()
        {
            var palavras = new[] { "um", "dois", "três" };

            var resultado = palavras.Join(" - ");

            Assert.Equal("um - dois - três", resultado);
        }

        [Fact]
        public void Join_DeveConcatenarComVirgula_QuandoSeparadorNaoForInformado()
        {
            var numeros = new[] { 1, 2, 3, 4, 5 };

            var resultado = numeros.Join();

            Assert.Equal("1,2,3,4,5", resultado);
        }

        [Fact]
        public void Join_DeveRetornarStringVazia_QuandoColecaoForNula()
        {
            IEnumerable colecao = null;

            var resultado = colecao.Join();

            Assert.Equal("", resultado);
        }

        [Fact]
        public void Join_DeveRetornarStringVazia_QuandoColecaoForVazia()
        {
            var colecao = new ArrayList();

            var resultado = colecao.Join();

            Assert.Equal("", resultado);
        }

        [Fact]
        public void RemoveAll_DeveRemoverItensQueAtendemPredicado()
        {
            var numeros = new List<int> { 1, 2, 3, 4, 5, 6 };

            IEnumerableExtensions.RemoveAll(numeros, n => n % 2 == 0);

            Assert.Equal(3, numeros.Count);
            Assert.Contains(1, numeros);
            Assert.Contains(3, numeros);
            Assert.Contains(5, numeros);
        }

        [Fact]
        public void RemoveAll_NaoDeveLancarExcecao_QuandoListaForNula()
        {
            List<int> numeros = null;

            var exception = Record.Exception(() => IEnumerableExtensions.RemoveAll(numeros, n => n % 2 == 0));

            Assert.Null(exception);
        }

        [Fact]
        public void RemoveAll_NaoDeveLancarExcecao_QuandoListaForVazia()
        {
            var numeros = new List<int>();

            var exception = Record.Exception(() => IEnumerableExtensions.RemoveAll(numeros, n => n % 2 == 0));

            Assert.Null(exception);
        }

        [Fact]
        public void RemoveAll_NaoDeveRemoverNada_QuandoNenhumItemAtenderPredicado()
        {
            var numeros = new List<int> { 1, 3, 5, 7 };

            IEnumerableExtensions.RemoveAll(numeros, n => n % 2 == 0);

            Assert.Equal(4, numeros.Count);
        }

        [Fact]
        public void Replace_DeveRetornarSequenciaVazia_QuandoFonteForVazia()
        {
            var palavras = new string[0];

            var resultado = palavras.Replace("a", "b").ToList();

            Assert.Empty(resultado);
        }

        [Fact]
        public void Replace_DeveSubstituirTextoEmTodosElementos()
        {
            var palavras = new[] { "casa", "casaco", "casamento" };

            var resultado = palavras.Replace("casa", "CASA").ToList();

            Assert.Equal("CASA", resultado[0]);
            Assert.Equal("CASAco", resultado[1]);
            Assert.Equal("CASAmento", resultado[2]);
        }

        [Fact]
        public void Replace_NaoDeveAlterarElementos_QuandoTextoNaoForEncontrado()
        {
            var palavras = new[] { "um", "dois", "três" };

            var resultado = palavras.Replace("quatro", "cinco").ToList();

            Assert.Equal("um", resultado[0]);
            Assert.Equal("dois", resultado[1]);
            Assert.Equal("três", resultado[2]);
        }

        [Fact]
        public void WithIndex_DevePermitirAcessoSimultaneoAElementoEIndice()
        {
            var numeros = new[] { 10, 20, 30, 40 };

            foreach(var (valor, indice) in numeros.WithIndex())
            {
                Assert.Equal(numeros[indice], valor);
            }
        }

        [Fact]
        public void WithIndex_DeveRetornarElementosComIndice()
        {
            var palavras = new[] { "um", "dois", "três" };

            var resultado = palavras.WithIndex().ToList();

            Assert.Equal(3, resultado.Count);
            Assert.Equal(("um", 0), resultado[0]);
            Assert.Equal(("dois", 1), resultado[1]);
            Assert.Equal(("três", 2), resultado[2]);
        }

        [Fact]
        public void WithIndex_DeveRetornarSequenciaVazia_QuandoFonteForVazia()
        {
            var palavras = new string[0];

            var resultado = palavras.WithIndex().ToList();

            Assert.Empty(resultado);
        }

        #endregion Public Methods
    }
}