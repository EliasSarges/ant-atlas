namespace AntAtlas.Domain.ValueObjects;

public record Pheromone
{
    public int Strength { get; init; }
    public PheromoneType Type { get; init; }
    public bool HasEvaporatedCompletely => Strength == 0;

    public Pheromone(int strength, PheromoneType type)
    {
        if (strength < 0)
             throw new ArgumentOutOfRangeException(nameof(strength), "Strength must be non-negative.");

        Strength = strength;
        Type = type;
    }

    public Pheromone Evaporate(int evaporationRate)
    {
        var newStrength = Math.Max(0, Strength - evaporationRate);

        return this with { Strength = newStrength };
    }
}

public enum PheromoneType
{
    Food,
    Danger,
    Trail
}