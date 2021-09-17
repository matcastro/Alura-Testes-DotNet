using Alura.LeilaoOnline.Core;
using System;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LanceCtor
    {
        [Fact]
        public void LancaArgumentExceptionDadoValorNegativo()
        {
            // Arrange - Cenário
            var valorNegativo = -100;

            // Assert - Verificação da expectativa
            Assert.Throws<ArgumentException>(
                // Act - Metódo sob teste
                () => new Lance(null, valorNegativo)
            );
        }
    }
}
