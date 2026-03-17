using AntAtlas.Domain.ValueObjects;

namespace AntAtlas.Domain.Tests.ValueObjects;

public class FoodStockTests
{
    [Fact]
    public void FoodStockAmount_WhenIsCreatedWithNegativeValue_ShouldThrowException()
    {
        Assert.Throws<InvalidOperationException>(() => new FoodStock(-100));
    }

    [Fact]
    public void AddFood_WithValidArgument_ShouldIncreaseAmount()
    {
        var foodStock = new FoodStock(0).AddFood(10);

        Assert.Equal(10, foodStock.Amount);
    }

    [Fact]
    public void TryConsumeFood_WhenAmountIsEqualArgument_ShouldResultZeroAmountAndReturnTrue()
    {
        var foodStock = new FoodStock(10);

        var result = foodStock.TryConsumeFood(10, out var newFoodStock);

        Assert.True(result);
        Assert.Equal(0, newFoodStock.Amount);
    }

    [Fact]
    public void TryConsumeFood_HasNotEnoughAmount_ShouldNotDecreaseAndReturnFalse()
    {
        var foodStock = new FoodStock(0);

        var result = foodStock.TryConsumeFood(10, out var newFoodStock);

        Assert.False(result);
        Assert.Equal(0, newFoodStock.Amount);
    }

    [Fact]
    public void TryConsumeFood_WhenHasEnoughAmount_ShouldDecreaseAndReturnTrue()
    {
        var foodStock = new FoodStock(10);

        var result = foodStock.TryConsumeFood(5, out var newFoodStock);

        Assert.True(result);
        Assert.Equal(5, newFoodStock.Amount);
    }
}