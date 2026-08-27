using System;

namespace Unimake.Primitives.SwaggerEngine.Attributes
{
    /// <summary>
    /// Indica que a propriedade decorada não deve aparecer no "Example" gerado para o Swagger,
    /// mesmo que continue documentada normalmente no schema.
    /// </summary>
    /// <remarks>
    /// Útil para propriedades mantidas apenas por compatibilidade com integrações antigas,
    /// que não devem ser incentivadas em novos exemplos de uso da API.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class SwaggerExampleIgnoreAttribute : Attribute
    {
    }
}