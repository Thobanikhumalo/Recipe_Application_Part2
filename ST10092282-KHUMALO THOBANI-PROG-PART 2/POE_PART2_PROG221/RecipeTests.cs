using System.Collections.Generic;
using Xunit;

namespace POE_PART2_PROG221
{
    public class RecipeTests
    {
        [Fact]
        public void GetTotalCalories_ReturnsCorrectTotalCalories()
        {
            // Arrange
            Dictionary<string, object> recipe = new Dictionary<string, object>();
            List<Dictionary<string, object>> ingredients = new List<Dictionary<string, object>>();

            // Create ingredient 1
            Dictionary<string, object> ingredient1 = new Dictionary<string, object>();
            ingredient1["calories"] = 100;
            ingredients.Add(ingredient1);

            // Create ingredient 2
            Dictionary<string, object> ingredient2 = new Dictionary<string, object>();
            ingredient2["calories"] = 200;
            ingredients.Add(ingredient2);

            recipe["ingredients"] = ingredients;

            // Act
            double totalCalories = GetTotalCalories(recipe);

            // Assert
            Assert.Equal(300, totalCalories);
        }

        static double GetTotalCalories(Dictionary<string, object> recipe)
        {
            double totalCalories = 0;

            // Retrieve the list of ingredients from the recipe
            List<Dictionary<string, object>> ingredients = (List<Dictionary<string, object>>)recipe["ingredients"];

            // Iterate over each ingredient and sum up the calories
            foreach (var ingredient in ingredients)
            {
                int calories = (int)ingredient["calories"];
                totalCalories += calories;
            }

            return totalCalories;
        }
    }
}
