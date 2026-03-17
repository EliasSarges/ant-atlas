namespace AntAtlas.Domain.ValueObjects;

public record FoodStock
{
    public int Amount { get; init; } = 0;

    public FoodStock(int amount)
    {
        if (amount < 0)
        {
            throw new InvalidOperationException("food stock can not be negative");
        }

        Amount = amount;
    }

    public FoodStock AddFood(int amount)
    {
        var newAmount = Amount + amount;

        return this with { Amount = newAmount };
    }

    public bool TryConsumeFood(int foodCost, out FoodStock foodStock)
    {
        if (foodCost > Amount)
        {
            foodStock = this;
            return false;
        }

        var newAmount = Amount - foodCost;
        foodStock = this with { Amount = newAmount };

        return true;
    }
}