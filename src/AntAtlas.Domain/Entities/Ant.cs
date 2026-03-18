using AntAtlas.Domain.Events;
using AntAtlas.Domain.Exceptions;
using AntAtlas.Domain.ValueObjects;

namespace AntAtlas.Domain.Entities;

public class Ant : Entity
{
    public Coordinate Position { get; private set; }
    public int Energy { get; private set; }
    public bool IsDead => Energy <= 0;

    public Ant(Coordinate position, int energy)
    {
        if (energy <= 0)
        {
            throw new InvalidOperationException("energy can not be negative or zero");
        }

        Position = position;
        Energy = energy;
    }

    private void ConsumeEnergy(int energyCost)
    {
        if (IsDead)
        {
            throw new AntIsDeadException(Id);
        }

        Energy -= energyCost;

        if (IsDead)
        {
            AddDomainEvent(new AntDied(Id));
        }
    }

    public void MoveTo(Coordinate newPosition, int energyCost)
    {
        ConsumeEnergy(energyCost);

        Position = newPosition;
    }
}