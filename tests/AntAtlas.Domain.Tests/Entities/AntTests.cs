using AntAtlas.Domain.Entities;
using AntAtlas.Domain.Exceptions;
using AntAtlas.Domain.ValueObjects;

namespace AntAtlas.Domain.Tests.Entities;

public class AntTests
{
    [Fact]
    public void MoveTo_WhenAntIsDead_ShouldThrowAntIsDeadException()
    {
        var startPosition = new Coordinate(0, 0);
        var ant = new Ant(startPosition, 0);

        var destination = new Coordinate(1, 1);
        var energyCost = 1;

        Assert.Throws<AntIsDeadException>(() => ant.MoveTo(destination, energyCost));
    }
}