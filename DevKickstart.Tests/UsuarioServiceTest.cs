using DevKickstart.Api.Services;
using DevKickstart.Application.Interfaces;
using Moq;

namespace DevKickstart.Tests;

public class UsuarioServiceTest
{
    [Fact]
    public async Task CrearUsuario_DeberiaGuardarUsuario()
    {
        // Arrange
        var repositoryMock = new Mock<IUsuarioRepository>();
        var service = new UsuarioService(repositoryMock.Object);

        // Act
        var usuario = await service.CrearUsuario("Bruno");

        // Assert
        Assert.NotNull(usuario);
        Assert.Equal("Bruno", usuario.Nombre);
        repositoryMock.Verify(r => r.Guardar(It.IsAny<Domain.Entities.Usuario>()), Times.Once);
    }
}