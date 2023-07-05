using System;
using System.Collections.Generic;
using System.Linq;

namespace POE_PART2_PROG221
{
    internal class Program
    {
        // List to store recipes as dictionaries  //(Tutorials Points, 2020)
        static List<Dictionary<string, object>> recipes = new List<Dictionary<string, object>>();

        // Delegate for handling recipe calorie exceeding events  //(Tutorials Points, 2020)
        public delegate void RecipeCaloriesExceededDelegate(Dictionary<string, object> recipe);

        // Event for recipe calorie exceeding  //(Tutorials Points, 2020)
        public static event RecipeCaloriesExceededDelegate CaloriesExceededEvent;

        static void Main(string[] args)
        {
            // Subscribe the RecipeCaloriesExceededHandler method to the CaloriesExceededEvent  //(Tutorials Points, 2020)
            CaloriesExceededEvent += RecipeCaloriesExceededHandler;

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n╔═════════════════════════════════════════╗");
                Console.WriteLine("║               MAIN MENU                 ║");
                Console.WriteLine("╠═════════════════════════════════════════╣");
                Console.WriteLine("║  1: Add Recipe                          ║");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("║  2: CHECK CALORIES < exceed 300 or NOT> ║");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("║  3: Display Recipes Alphabetical        ║");
                Console.WriteLine("║  4: All Recipes AND Their Information   ║");
                Console.WriteLine("║  5: Scale Recipe [Ingredient]           ║");
                Console.WriteLine("║  6: Reset                               ║");
                Console.WriteLine("║  7: Clear Data                          ║");
                Console.WriteLine("╚═════════════════════════════════════════╝");
                Console.Write("Enter your choice: ");

                int choice;
                try
                {
                    Console.ForegroundColor = ConsoleColor.Yellow; //(Geeks for Geeks, 2021)
                    choice = int.Parse(Console.ReadLine());
                }
                catch (FormatException) //(Geeks for Geeks, 2021)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    continue;
                }
                Console.WriteLine();

                switch (choice)
                {
                    case 1:
                        AddRecipe(); // Invoke the AddRecipe method //(Geeks for Geeks, 2021)
                        break;
                    case 2:
                        if (recipes.Count == 0)
                        {
                            Console.WriteLine("No recipes found.");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan; //(Geeks for Geeks, 2021)
                            Console.WriteLine("Available Recipes:\n");
                            for (int i = 0; i < recipes.Count; i++)
                            {
                                Dictionary<string, object> recipe = recipes[i];
                                Console.WriteLine($"Recipe {i + 1}: {recipe["name"]}");
                            }

                            Console.Write("\nEnter the corresponding number for the recipe: "); //(Geeks for Geeks, 2021)
                            int recipeNumber;
                            try
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                recipeNumber = int.Parse(Console.ReadLine());
                            }
                            catch (FormatException) //(Geeks for Geeks, 2021)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid input. Please enter a valid number.");
                                continue;
                            }
                            Console.WriteLine();

                            CheckCaloriesExceeded(recipeNumber - 1); // Invoke the CheckCaloriesExceeded method //(Tutorials Points, 2020)
                        }
                        break;

                    case 3:
                        DisplayRecipesAlphabetical(); // Invoke the DisplayRecipesAlphabetical method //(Tutorials Points, 2020)
                        break;
                    case 4:
                        DisplayAllRecipes(); // Invoke the DisplayAllRecipes method
                        break;
                    case 5:
                        ScaleRecipe(); // Invoke the ScaleRecipe method //(Tutorials Points, 2020)
                        break;

                    case 6:
                        ResetQuantities(); // Invoke the ResetQuantities method
                        break;
                    case 7:
                        ClearData(); // Invoke the ClearData method //(Tutorials Points, 2020)
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        static void AddRecipe() //(Tutorials Points, 2020)
        {
            // Create a new recipe dictionary
            Dictionary<string, object> recipe = new Dictionary<string, object>();

            Console.ForegroundColor = ConsoleColor.Cyan; //(Tutorials Points, 2020)
            Console.WriteLine("╔═════════════════════════════════════════╗");
            Console.WriteLine("║             RECIPE CREATION             ║");
            Console.WriteLine("╚═════════════════════════════════════════╝\n");

            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("ENTER THE RECIPE NAME: ");
            string recipeName = Console.ReadLine();

            Console.Write("ENTER THE NUMBER OF INGREDIENTS: ");
            int numIngredients;
            try
            {
                Console.ForegroundColor = ConsoleColor.Yellow; //(Tutorials Points, 2020)
                numIngredients = int.Parse(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid input. Please enter a valid number.");
                return;
            }

            List<Dictionary<string, object>> ingredients = new List<Dictionary<string, object>>(); //(W3 school, 2022)

            for (int i = 0; i < numIngredients; i++)
            {
                // Create a new ingredient dictionary //(W3 school, 2022)
                Dictionary<string, object> ingredient = new Dictionary<string, object>();

                Console.Write($"Enter the INGREDIENT NAME for ingredient {i + 1}: ");
                string ingredientName = Console.ReadLine();

                Console.Write($"Enter the QUANTITY for ingredient {i + 1}: "); //(W3 school, 2022)
                double quantity;
                try
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    quantity = double.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red; //(W3 school, 2022)
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    return;
                }

                Console.Write($"Enter the UNIT OF MEASUREMENT for ingredient {i + 1}: "); //(W3 school, 2022)
                string unit = Console.ReadLine();

                Console.Write($"Enter the CALORIES for ingredient {i + 1}: ");
                int calories;
                try
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    calories = int.Parse(Console.ReadLine());
                }
                catch (FormatException) //(W3 school, 2022)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    return;
                }

                List<string> steps = new List<string>();
                Console.Write("Enter the NUMBER OF STEPS: ");
                int numSteps;
                try
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    numSteps = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    return;
                }

                for (int j = 0; j < numSteps; j++)
                {
                    Console.Write($"Step {j + 1} DESCRIPTION: ");
                    string stepDescription = Console.ReadLine();
                    steps.Add(stepDescription);
                }

                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("╔═════════════════════════════════════════╗");
                Console.WriteLine("║           CHOOSE THE FOOD GROUP         ║");
                Console.WriteLine("╠═════════════════════════════════════════╣");
                Console.WriteLine("║ 1. Starchy Food                         ║");
                Console.WriteLine("║ 2. Vegetables and Fruits                ║");
                Console.WriteLine("║ 3. Dry Beans, Peas, Lentils, and Soya   ║");
                Console.WriteLine("║ 4. Chicken, Fish, Meat, and Eggs        ║");
                Console.WriteLine("║ 5. Milk and Dairy Products              ║");
                Console.WriteLine("║ 6. Fats and Oil                         ║");
                Console.WriteLine("║ 7. Water                                ║");
                Console.WriteLine("╚═════════════════════════════════════════╝");

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Enter the corresponding number for the food group: ");
                int foodGroup;
                try
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    foodGroup = int.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    return;
                }

                // Add ingredient details to the ingredient dictionary
                ingredient.Add("name", ingredientName);
                ingredient.Add("original_quantity", quantity);
                ingredient.Add("quantity", quantity);
                ingredient.Add("unit", unit);
                ingredient.Add("calories", calories);
                ingredient.Add("food_group", foodGroup);
                ingredient.Add("steps", steps);

                // Add the ingredient dictionary to the list of ingredients
                ingredients.Add(ingredient);
            }

            // Add recipe details to the recipe dictionary
            recipe.Add("name", recipeName);
            recipe.Add("ingredients", ingredients);

            // Add the recipe dictionary to the list of recipes
            recipes.Add(recipe);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nRecipe '{recipeName}' has been added successfully!\n");
        }




        static void DisplayRecipe(Dictionary<string, object> recipe)
        {
            // Set the console foreground color to magenta
            Console.ForegroundColor = ConsoleColor.Magenta;

            // Display the recipe name section
            Console.WriteLine($"\n╔═════════════════════════════════════════╗");
            Console.WriteLine($"║          RECIPE: {recipe["name"]}          ║");
            Console.WriteLine($"╚═════════════════════════════════════════╝\n");

            // Reset the console color to default
            Console.ResetColor();

            // Set the console foreground color to cyan
            Console.ForegroundColor = ConsoleColor.Cyan;

            // Display the ingredients section
            Console.WriteLine("INGREDIENTS:");

            // Reset the console color to default
            Console.ResetColor();

            // Retrieve the list of ingredients from the recipe
            List<Dictionary<string, object>> ingredients = (List<Dictionary<string, object>>)recipe["ingredients"];

            // Iterate over each ingredient and display its details
            for (int i = 0; i < ingredients.Count; i++)
            {
                Dictionary<string, object> ingredient = ingredients[i];

                // Retrieve the food group number
                int foodGroup = (int)ingredient["food_group"];

                // Convert the food group number to its corresponding string representation
                string foodGroupString = GetFoodGroupString(foodGroup);

                // Display the ingredient details
                Console.WriteLine($"- Ingredient {i + 1}: {ingredient["name"]} ({ingredient["quantity"]} {ingredient["unit"]}): " +
                    $"{ingredient["calories"]} calories, Food Group: {foodGroupString}");

                // Retrieve the list of steps for the ingredient
                List<string> steps = (List<string>)ingredient["steps"];

                // Display the description of the ingredient's steps
                Console.WriteLine("  Description: " + string.Join(", ", steps));
            }

            Console.WriteLine();
        }

        static string GetFoodGroupString(int foodGroup)
        {
            switch (foodGroup)
            {
                case 1:
                    return "Starchy Food";
                case 2:
                    return "Vegetables and Fruits";
                case 3:
                    return "Dry Beans, Peas, Lentils, and Soya";
                case 4:
                    return "Chicken, Fish, Meat, and Eggs";
                case 5:
                    return "Milk and Dairy Products";
                case 6:
                    return "Fats and Oil";
                case 7:
                    return "Water";
                default:
                    return "Unknown";
            }
        }


        static void DisplayAllRecipes()
        {
            Console.WriteLine("\nDISPLAYING ALL RECIPES AND THEIR INFORMATION:\n");

            // Iterate over each recipe and display its name
            for (int i = 0; i < recipes.Count; i++)
            {
                Dictionary<string, object> recipe = recipes[i];
                Console.WriteLine($"Recipe {i + 1}: {recipe["name"]}");
            }

            // Check if there are no recipes
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes found.");
            }
            else
            {
                int recipeNumber = -1; // Initialize with a default value
                bool validInput = false;

                // Prompt the user to enter a recipe number until valid input is provided
                do
                {
                    Console.Write("Enter the corresponding number: ");
                    string input = Console.ReadLine();

                    // Attempt to parse the input as an integer
                    if (int.TryParse(input, out recipeNumber))
                    {
                        recipeNumber--;

                        // Check if the entered recipe number is within the valid range
                        if (recipeNumber >= 0 && recipeNumber < recipes.Count)
                        {
                            validInput = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid recipe number.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a valid number.");
                    }
                } while (!validInput);

                // Retrieve the selected recipe based on the entered recipe number //(Tutorials Points, 2020)
                Dictionary<string, object> selectedRecipe = recipes[recipeNumber];

                // Display the details of the selected recipe
                DisplayRecipe(selectedRecipe);
            }
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


        static void RecipeCaloriesExceededHandler(Dictionary<string, object> recipe)
        {
            // Set the console foreground color to red
            Console.ForegroundColor = ConsoleColor.Red;

            // Display a warning message for exceeding total calories
            Console.WriteLine("╔═════════════════════════════════════════╗");
            Console.WriteLine("║            WARNING MESSAGE              ║");
            Console.WriteLine("╠═════════════════════════════════════════╣");
            Console.WriteLine("║  WARNING: Total calories exceed 300!    ║");
            Console.WriteLine("╚═════════════════════════════════════════╝");

            // Reset the console color to default //(Tutorials Points, 2020)
            Console.ResetColor();
        }


        static void CheckCaloriesExceeded(int recipeNumber)
        {
            try
            {
                if (recipeNumber >= 0 && recipeNumber < recipes.Count)
                {
                    // Retrieve the recipe based on the provided recipe number
                    Dictionary<string, object> recipe = recipes[recipeNumber];

                    // Calculate the total calories of the recipe
                    double totalCalories = GetTotalCalories(recipe);

                    // Set the console foreground color to yellow
                    Console.ForegroundColor = ConsoleColor.Yellow;

                    // Display the calorie information section
                    Console.WriteLine("╔═════════════════════════════════════════╗");
                    Console.WriteLine("║            CALORIE INFORMATION          ║");
                    Console.WriteLine("╠═════════════════════════════════════════╣");

                    // Reset the console color to default
                    Console.ResetColor();

                    // Set the console foreground color to green
                    Console.ForegroundColor = ConsoleColor.Green;

                    // Display the total calories of the recipe
                    Console.WriteLine("║  Total Calories: " + totalCalories + "  ║");

                    // Reset the console color to default
                    Console.ResetColor();

                    Console.WriteLine("╚═════════════════════════════════════════╝");

                    if (totalCalories > 300)
                    {
                        // Invoke the event for exceeded calories
                        CaloriesExceededEvent?.Invoke(recipe);
                    }
                    else
                    {
                        // Set the console foreground color to yellow
                        Console.ForegroundColor = ConsoleColor.Yellow;

                        // Display the calorie information section for non-exceeded calories
                        Console.WriteLine("╔═════════════════════════════════════════╗");
                        Console.WriteLine("║             CALORIE INFORMATION         ║");
                        Console.WriteLine("╠═════════════════════════════════════════╣");

                        // Set the console foreground color to green
                        Console.ForegroundColor = ConsoleColor.Green;

                        // Display the total calories of the recipe
                        Console.WriteLine("║  Total Calories: " + totalCalories + "  ║");

                        // Display a message indicating non-exceeded calories
                        Console.WriteLine("║ This recipe does not exceed 300 calories║");

                        // Reset the console color to default
                        Console.ResetColor();

                        Console.WriteLine("╚═════════════════════════════════════════╝");
                    }
                }
                else
                {
                    // Set the console foreground color to red
                    Console.ForegroundColor = ConsoleColor.Red;

                    // Display an error message for an invalid recipe number
                    Console.WriteLine("Invalid recipe number.");

                    // Reset the console color to default
                    Console.ResetColor();
                }
            }
            catch (Exception e)
            {
                // Set the console foreground color to red
                Console.ForegroundColor = ConsoleColor.Red;

                // Display an error message for any exceptions occurred
                Console.WriteLine("An error occurred: " + e.Message);

                // Reset the console color to default
                Console.ResetColor();
            }
        }

        static void DisplayRecipe(Dictionary<string, object> recipe, double scalingFactor)
        {
            try
            {
                // Set console foreground color to magenta
                Console.ForegroundColor = ConsoleColor.Magenta;

                // Display recipe header
                Console.WriteLine($"\n╔═════════════════════════════════════════╗");
                Console.WriteLine($"║          RECIPE: {recipe["name"]}          ║");
                Console.WriteLine($"╚═════════════════════════════════════════╝\n");

                // Reset console color to default
                Console.ResetColor();

                // Set console foreground color to cyan
                Console.ForegroundColor = ConsoleColor.Cyan;

                // Display ingredients section header
                Console.WriteLine("INGREDIENTS:");

                // Reset console color to default
                Console.ResetColor();

                // Retrieve the list of ingredients from the recipe
                List<Dictionary<string, object>> ingredients = (List<Dictionary<string, object>>)recipe["ingredients"];

                // Iterate over each ingredient
                for (int i = 0; i < ingredients.Count; i++)
                {
                    // Retrieve ingredient details
                    Dictionary<string, object> ingredient = ingredients[i];
                    string name = (string)ingredient["name"];
                    double quantity = (double)ingredient["quantity"]; // Change to double
                    string unit = (string)ingredient["unit"];
                    int calories = (int)ingredient["calories"];
                    int foodGroup = (int)ingredient["food_group"];
                    List<string> steps = (List<string>)ingredient["steps"];

                    // Scale the quantity based on the provided scaling factor
                    quantity *= scalingFactor;

                    // Display ingredient information
                    Console.WriteLine($"- Ingredient {i + 1}: {name} ({quantity} {unit}): " +
                        $"{calories} calories, Food Group: {foodGroup}");

                    // Display ingredient description
                    Console.WriteLine("  Description: " + string.Join(", ", steps));
                }

                Console.WriteLine();
            }
            catch (Exception)
            {
                Console.WriteLine("An error occurred while displaying the recipe. Please try again.");
                DisplayRecipe(recipe, scalingFactor); // Retry displaying the recipe
            }
        }


        static void ResetQuantities()
        {
            try
            {
                // Iterate over each recipe
                foreach (var recipe in recipes)
                {
                    // Retrieve the list of ingredients from the recipe
                    List<Dictionary<string, object>> ingredients = (List<Dictionary<string, object>>)recipe["ingredients"];

                    // Iterate over each ingredient
                    foreach (var ingredient in ingredients)
                    {
                        // Retrieve the original quantity of the ingredient
                        double originalQuantity = (double)ingredient["original_quantity"];

                        // Reset the quantity of the ingredient to the original value
                        ingredient["quantity"] = originalQuantity;
                    }
                }

                // Display a success message
                Console.WriteLine("Quantities have been reset to their original values.");
            }
            catch (Exception)
            {
                // Display an error message if an exception occurs
                Console.WriteLine("An error occurred while resetting quantities. Please try again.");

                // Retry resetting the quantities
                ResetQuantities();
            }
        }


        static void ClearData()
        {
            try
            {
                // Clear the list of recipes
                recipes.Clear();

                // Display a success message
                Console.WriteLine("All data has been cleared.");
            }
            catch (Exception)
            {
                // Display an error message if an exception occurs
                Console.WriteLine("An error occurred while clearing data. Please try again.");

                // Retry clearing the data
                ClearData();
            }
        }
        static void DisplayRecipesAlphabetical()
        {
            try
            {
                // Display a heading for the alphabetical recipe list
                Console.WriteLine("\nDISPLAYING ALL RECIPES IN ALPHABETICAL ORDER:\n");

                // Sort the recipes list alphabetically by recipe name
                List<Dictionary<string, object>> sortedRecipes = recipes.OrderBy(r => r["name"].ToString()).ToList();

                // Iterate over each recipe in the sorted list and display its name
                for (int i = 0; i < sortedRecipes.Count; i++)
                {
                    Dictionary<string, object> recipe = sortedRecipes[i];
                    Console.WriteLine($"Recipe {i + 1}: {recipe["name"]}");
                }

                // Check if any recipes are available
                if (sortedRecipes.Count == 0)
                {
                    Console.WriteLine("No recipes found.");
                }
                else
                {
                    bool validInput = false;
                    int recipeNumber = 0;

                    // Prompt the user to enter the corresponding number for the selected recipe
                    while (!validInput)
                    {
                        try
                        {
                            Console.Write("Enter the corresponding number: ");
                            recipeNumber = int.Parse(Console.ReadLine()) - 1;
                            validInput = true;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Invalid input. Please enter a valid number.");
                        }
                    }

                    // Check if the entered recipe number is within the valid range
                    if (recipeNumber >= 0 && recipeNumber < sortedRecipes.Count)
                    {
                        // Retrieve the selected recipe based on the entered number
                        Dictionary<string, object> selectedRecipe = sortedRecipes[recipeNumber];

                        // Display the details of the selected recipe
                        DisplayRecipe(selectedRecipe);
                    }
                    else
                    {
                        Console.WriteLine("Invalid recipe number.");
                    }
                }
            }
            catch (Exception)
            {
                // Display an error message if an exception occurs
                Console.WriteLine("An error occurred while displaying recipes. Please try again.");

                // Retry displaying the recipes
                DisplayRecipesAlphabetical();
            }
        }

        static void ScaleRecipe()
        {
            try
            {
                // Check if any recipes are available
                if (recipes.Count == 0)
                {
                    Console.WriteLine("No recipes found.");
                }
                else
                {
                    Console.WriteLine("Choose a recipe to scale:\n");

                    // Iterate over each recipe and display its name
                    for (int i = 0; i < recipes.Count; i++)
                    {
                        Dictionary<string, object> recipe = recipes[i];
                        Console.WriteLine($"Recipe {i + 1}: {recipe["name"]}");
                    }

                    bool validRecipeInput = false;
                    int recipeNumber = 0;

                    // Prompt the user to enter the corresponding number for the selected recipe
                    while (!validRecipeInput)
                    {
                        try
                        {
                            Console.Write("\nEnter the corresponding number for the recipe: ");
                            recipeNumber = int.Parse(Console.ReadLine()) - 1;
                            validRecipeInput = true;
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Invalid input. Please enter a valid number.");
                        }
                    }

                    Console.WriteLine();

                    // Check if the entered recipe number is within the valid range
                    if (recipeNumber >= 0 && recipeNumber < recipes.Count)
                    {
                        // Retrieve the selected recipe based on the entered number
                        Dictionary<string, object> selectedRecipe = recipes[recipeNumber];

                        Console.WriteLine("Choose an ingredient to scale:\n");

                        // Get the list of ingredients for the selected recipe
                        List<Dictionary<string, object>> ingredients = (List<Dictionary<string, object>>)selectedRecipe["ingredients"];

                        // Iterate over each ingredient and display its name
                        for (int i = 0; i < ingredients.Count; i++)
                        {
                            Dictionary<string, object> ingredient = ingredients[i];
                            Console.WriteLine($"Ingredient {i + 1}: {ingredient["name"]}");
                        }

                        bool validIngredientInput = false;
                        int ingredientNumber = 0;

                        // Prompt the user to enter the corresponding number for the selected ingredient
                        while (!validIngredientInput)
                        {
                            try
                            {
                                Console.Write("\nEnter the corresponding number for the ingredient: ");
                                ingredientNumber = int.Parse(Console.ReadLine()) - 1;
                                validIngredientInput = true;
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Invalid input. Please enter a valid number.");
                            }
                        }

                        Console.WriteLine();

                        // Check if the entered ingredient number is within the valid range
                        if (ingredientNumber >= 0 && ingredientNumber < ingredients.Count)
                        {
                            // Retrieve the selected ingredient based on the entered number
                            Dictionary<string, object> selectedIngredient = ingredients[ingredientNumber];

                            Console.WriteLine("Choose a scaling factor:");
                            Console.WriteLine("1. 0.5 (half)");
                            Console.WriteLine("2. 2 (double)");
                            Console.WriteLine("3. 3 (triple)");

                            bool validScalingInput = false;
                            int scalingOption = 0;

                            // Prompt the user to enter the corresponding number for the scaling factor
                            while (!validScalingInput)
                            {
                                try
                                {
                                    Console.Write("Enter the corresponding number: ");
                                    scalingOption = int.Parse(Console.ReadLine());
                                    validScalingInput = true;
                                }
                                catch (Exception)
                                {
                                    Console.WriteLine("Invalid input. Please enter a valid number.");
                                }
                            }

                            Console.WriteLine();

                            double scalingFactor = 1.0;

                            // Set the scaling factor based on the selected option
                            if (scalingOption == 1)
                            {
                                scalingFactor = 0.5;
                            }
                            else if (scalingOption == 2)
                            {
                                scalingFactor = 2.0;
                            }
                            else if (scalingOption == 3)
                            {
                                scalingFactor = 3.0;
                            }

                            // Retrieve the original quantity of the selected ingredient
                            double originalQuantity = (double)selectedIngredient["quantity"];

                            // Calculate the scaled quantity
                            double scaledQuantity = originalQuantity * scalingFactor;

                            // Display the original quantity, scaling factor, and scaled quantity
                            Console.WriteLine($"Original Quantity: {originalQuantity}");
                            Console.WriteLine($"Scaling Factor: {scalingFactor}");
                            Console.WriteLine($"Scaled Quantity: {scaledQuantity}");

                            // Update the selected ingredient's quantity with the scaled quantity
                            selectedIngredient["quantity"] = scaledQuantity;

                            Console.WriteLine("\nIngredient quantity has been scaled.");
                        }
                        else
                        {
                            Console.WriteLine("Invalid ingredient number.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid recipe number.");
                    }
                }
            }
            catch (Exception)
            {
                // Display an error message if an exception occurs
                Console.WriteLine("An error occurred while scaling the recipe. Please try again.");

                // Retry scaling the recipe
                ScaleRecipe();
            }
        }




        static void ScaleIngredient(Dictionary<string, object> ingredient, double scalingFactor)
        {
            try
            {
                // Check if the ingredient contains the "original_quantity" key
                if (ingredient.ContainsKey("original_quantity"))
                {
                    // Check if the value of "original_quantity" is of type double
                    if (ingredient["original_quantity"] is double originalQuantity)
                    {
                        // Calculate the scaled quantity
                        double scaledQuantity = originalQuantity * scalingFactor;

                        // Update the quantity of the ingredient
                        ingredient["quantity"] = scaledQuantity;

                        // Display the message with the scaled quantity
                        Console.WriteLine($"The quantity of ingredient '{ingredient["name"]}' has been scaled to {scaledQuantity} {ingredient["unit"]}.");
                    }
                    else
                    {
                        // Display an error message for an invalid original quantity value
                        Console.WriteLine("Invalid original quantity value.");
                    }
                }
                else
                {
                    // Display an error message if the original quantity key is not found
                    Console.WriteLine("Original quantity not found.");
                }
            }
            catch (Exception)
            {
                // Display a generic error message if an exception occurs during scaling
                Console.WriteLine("An error occurred while scaling the ingredient. Please try again.");

                // Retry scaling the ingredient by recursively calling the ScaleIngredient method
                ScaleIngredient(ingredient, scalingFactor);
            }
        }
    }
}




//REFERENCES 

/* W3 school.2022.C# for loop, 21 december 2022.[Online].Available at:
* https://www.w3schools.com/cs/cs_for_loop.php
* [Accessed 20 march 2023]
*/

/*TutorialsTeacher.2020.C# for Loops, 17 june 2020.[Online].Available at:
 * https://www.tutorialsteacher.com/csharp/csharp-for-loop
 * [Accessed 25 march 2023]
 */

/*Geek for Geeks.2021.Switch statements in c#, 05 April 2021.[Online].Available at:
 * https://www.geeksforgeeks.org/switch-statement-in-c-sharp/
 * [Accessed 01 April 2023]
 */

/*C# station.2017.Multiplication and division in C#, 22January 2017.[Online].Available At:
 * https://csharp-station.com/multiplication-and-division-in-c/#:~:text=In%20C%23%2C%20the%20multiplication%20symbol,x%202%2C%20which%20equals%2012.
 * [Accessed 04 april 2023]
 */

/* Tutorial Point.2020.C# variables, 06 february 2020.[Online].Available at:
 * https://www.tutorialspoint.com/csharp/csharp_variables.htm
 * [Accessed 04 April 2023]
 */

/*Menu in c#.2019.How to create a menu to c# console app, 27 may 2019.[Online].Available at:
 * https://wellsb.com/csharp/beginners/create-menu-csharp-console-application#:~:text=The%20MainMenu()%20method%20should%20first%20print%20the%20menu%20options,will%20call%20the%20appropriate%20method.
 * [Accessed 15 april 2023]
 */


