using AntAtlas.Domain.ValueObjects;

namespace AntAtlas.Domain.Entities;

public class Colony : Entity
{
    public FoodStock FoodStock { get; private set; }
    public int FoodCost { get; private set; } = 0;
    public int AntsAliveCount { get; private set; } = 0;

    public Colony(FoodStock foodStock, int foodCost)
    {
        if (foodCost <= 0)
        {
            throw new InvalidOperationException("food cost can not be negative or zero");
        }

        FoodStock = foodStock;
        FoodCost = foodCost;
    }

    public bool TrySpawnAnt(Coordinate position, int energy, out Ant? ant)
    {
        if (!FoodStock.TryConsumeFood(foodCost: FoodCost, out var foodStock))
        {
            ant = null;
            return false;
        }

        AntsAliveCount++;
        FoodStock = foodStock;
        ant = new Ant(position, energy);
        return true;
    }

    public void AddFood(int foodAmount)
    {
        FoodStock = FoodStock.AddFood(foodAmount);
    }

    public void DecreaseAntAlivesCounter()
    {
        if (AntsAliveCount == 0)
        {
            return;
        }

        AntsAliveCount -= 1;
    }
}