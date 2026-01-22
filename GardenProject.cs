//Garden project - Zoe Parrish 

namespace GardenProject
{
    class Program
    {
    public static void Main(string[]args)
    {
        Game game1 = new Game();
        game1.start();
    }//Main
    }//class Program

    class Plant
    {
        //variables : name, age, sunLightReq, health, growthStages, currentStage
       public string name;
       public int age;
    
       public double health;
        public List<string> growthStages;
        public int currentStage;

        //Methods
        public Plant(string name)
        {
            this.name = name;
            age = 0;
            health = 100;
            growthStages = new List<string> {"seed", "sprout", "young plant", "mature plant"};
            currentStage = 0;
        }//plant default constructer 

        //grow() simulates plant growth by advancing stage if health and sunlight req are met
        public void grow()
        {
            if (health > 70)
            {
                if (currentStage < growthStages.Count - 1)
                {
                    currentStage++;
                    Console.WriteLine("{0} has grown to the {1} stage!",name, growthStages[currentStage]);
                }
                else
                {
                    Console.WriteLine("{0} is fully grown!", name);
                }
            }
            else
            {
                Console.WriteLine("{0} is too unhealthy to grow. Take care of it first!", name );
            }
        }//grow() method

        //assessWaterNeeds() evaluates if the plant needs water
        public void assessWaterNeeds()
        {
             if (health < 50)
            {
                Console.WriteLine("{0} needs water urgently!",name);
            }
            else
            {
                Console.WriteLine("{0} has sufficient water.", name);
            }
        }//assessWaterNeeds method

        //displayStatus() prints the plants current health, age, and growth stage

        public virtual void displayStatus()
        {
            Console.WriteLine("Plant: {0}",name);
            Console.WriteLine("Health: {0}", health);
            Console.WriteLine("Age: {0}",age);
            Console.WriteLine("Growth Stage: {0}",growthStages[currentStage]);  
        }//displayStatus Method
        //adjustHealth(double amount) modifies the plants health based on enviromment or user inputs
       public void adjustHealth(double amount)
       {
            health += amount;
           
       }//adjustHealth() method
        
    }//Plant class

    class Flower :Plant
    {
        //variables: bloom cycle, colorVariety, colors
        string bloomCycle;
        public string [] colorVariety =  {"pink","purple","blue", "red"};
        public bool isBlooming; //true if bloomed

        //methods
        public Flower (string name, string bloomCycle, List<string>colorVariety):base(name)
        {
            this.bloomCycle = bloomCycle;
            this.colorVariety= colorVariety.ToArray();
            this.isBlooming = false;

        }//default constructor Flower()

        public void CheckBloom()
        {
            if (currentStage == growthStages.Count - 1 && health > 70)
            {
                isBlooming = true;
                Console.WriteLine("{0} is blooming with vibrant colors: {1}!", name, string.Join(", ", colorVariety)); //string.join adds a comma in between colors!
            }
            else
            {
                isBlooming = false;
                Console.WriteLine("{0} is not ready to bloom yet. Keep taking care of it!", name);
            }
        }
        //overrides the parent method to include bloom cycle and color details
        public override void displayStatus()
        {
            base.displayStatus();
            Console.WriteLine("Bloom Cycle: {0}", bloomCycle);
            Console.WriteLine("Blooming: {0}", (isBlooming ? "Yes :)" : "Not yet")); //chatGPT helped with this: condition ? valueIfTrue : valueIfFalse
            Console.WriteLine("Colors: {0}", string.Join(", ", colorVariety));     

        }//displayStatus() method

    }//flower : Plant

    class Vegetable : Plant
    {
        //variables : growth stage, harvestReadiness
        // public string growthStages;
        bool harvestReadiness;
        bool isHarvested;
        public Vegetable (string name):base(name)
        {
            harvestReadiness = false;
            isHarvested = false;
        }//default constructor Vegetable()

        //harvest() marks the vegetable as harvested if ready
        public void checkHarvestReadiness()
        {
             if (currentStage == growthStages.Count - 1 && health > 60)
            {
                harvestReadiness = true;
                Console.WriteLine("{0} is ready for harvest!", name);
            }
            else
            {
                harvestReadiness = false;
                Console.WriteLine("{0} is not ready for harvest yet. Keep nurturing it!", name);
            }
        }//checkHarvestReadiness(() method

        public void Harvest()
        {
            if (harvestReadiness)
            {
                isHarvested = true;
                Console.WriteLine("You have harvested {0}! Enjoy the fruits of your labor. <3", name);
            }
            else
            {
                Console.WriteLine("{0} is not ready to harvest yet.", name);
            }
        }
        public override void displayStatus()
        {
        base.displayStatus();
        Console.WriteLine("Harvest Ready: {0}",(harvestReadiness ? "Yes" : "No"));
        Console.WriteLine("Harvested: {0}",(isHarvested ? "Yes" : "No"));
        }

    }// Vegetable : Plant

    class Herb : Plant
    {
        //variables: aromaStrength, cuttingFreq
        double aromaStrength;
        int cuttingFreq;

         public Herb(string name, double initialAromaStrength):base(name)
        {
            aromaStrength = initialAromaStrength;
            cuttingFreq = 0;
        }//default constructor Herb()

        //cutForUse() reduces aroma strength temporarily and inc cutting freq

        public void cutForUse()
        {
            if (aromaStrength > 20)
            {
            aromaStrength -= 20;
            cuttingFreq++;
            Console.WriteLine("You cut {0} for use. Its aroma strength is now {1}.", name, aromaStrength);
            }
            else
            {
            Console.WriteLine("{0} doesn't have enough aroma strength to be cut.", name);
            }
        }//cutForUse() method

         public void regenerateAroma()
        {
            aromaStrength += 15;
            if (aromaStrength > 100) aromaStrength = 100;
            Console.WriteLine("{0}'s aroma strength has regenerated to {1}.", name, aromaStrength);
        }

        public override void displayStatus()
        {
            base.displayStatus();
            Console.WriteLine("Aroma Strength: {0}", aromaStrength);
            Console.WriteLine("Cutting Frequency: {0}", cuttingFreq);
        }
    } //Herb : Plant

    class Weather
    {
        //variables : currentWeather
        public static string currentWeather;

        //methods:
        //generateWeather() randomly generates current weather
        public void generateWeather()
        {
            string[] weatherTypes = { "Sunny", "Rainy", "Cloudy", "Stormy" };
            Random r = new Random();
            currentWeather = weatherTypes[r.Next(weatherTypes.Length)];
        }//generateWeather() method

        //weather effect calculates how weather impacts plants
        public void getWeatherEffects(Plant p)
        {
            switch (currentWeather)
            {
                case "Sunny":
                    p.adjustHealth(10);
                    break;
                case "Rainy":
                    p.adjustHealth(20);
                    break;
                case "Cloudy":
                    p.adjustHealth(5);
                    break;
                case "Stormy":
                    p.adjustHealth(-20);
                    break;
            }
        }//getWeatherEffects() method

    } //Weather Class

  
    public class Game
    {
    private List<Plant> plants = new List<Plant>();
    private Weather weather = new Weather();

    public void start()
    {
        Console.WriteLine("Welcome to your new Garden! :)");
        
        // Add the first plant
        AddPlant();

        while (true)
        {
            try{
            Console.WriteLine("\nChoose an option:");
            Console.WriteLine("1: View plant status");
            Console.WriteLine("2: Water a plant");
            Console.WriteLine("3: Grow a plant");
            Console.WriteLine("4: Apply weather effects");
            Console.WriteLine("5: Add another plant");
            Console.WriteLine("6: View all plants");
            Console.WriteLine("7: Exit");

            string choice = Console.ReadLine();
            
            switch (choice)
            {
                case "1":
                    ChoosePlantAndPerform(p => p.displayStatus());
                    break;
                case "2":
                    ChoosePlantAndPerform(p => p.adjustHealth(15));
                    Console.WriteLine("You watered the plant! :)");
                    break;
                case "3":
                    ChoosePlantAndPerform(p => p.grow());
                    break;
                case "4":
                    applyWeatherEffects();
                    break;
                case "5":
                    AddPlant();
                    break;
                case "6":
                    DisplayAllPlants();
                    break;
                case "7":
                    Console.WriteLine("Thanks for playing the Garden Project!");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }//switch
            }//try
            catch 
            {
                Console.WriteLine("Please enter a number 1,2,3,4,5,6,or,7");
            }//catch
        }//while
    }//start()

    private void AddPlant()
    {

        Console.WriteLine("Add a new plant!");

        Console.Write("Enter the plant's name: ");
        string plantName = Console.ReadLine();
        try{
            Console.WriteLine("Choose a plant type (1: Flower, 2: Vegetable, 3: Herb): ");
            string typeChoice = Console.ReadLine();

            Plant p;

            switch (typeChoice)
            {
                case "1":
                    p = new Flower(plantName, "Annual", new List<string> { "Red", "Yellow", "Blue" });
                    break;
                case "2":
                    p = new Vegetable(plantName);
                    break;
                case "3":
                    p = new Herb(plantName, 70);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Defaulting to a generic plant.");
                    p = new Plant(plantName);
                    break;
            }

            plants.Add(p);
            Console.WriteLine("Successfully added {0}!",p.name);
        }
        catch 
        {
             Console.WriteLine("Please enter a number 1,2,or 3");
        }
    }

    private void DisplayAllPlants()
    {
        if (plants.Count == 0)
        {
            Console.WriteLine("You have no plants in your garden.");
            return;
        }

        Console.WriteLine("\nYour Plants:");
        for (int i = 0; i < plants.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {plants[i].name} - Health: {plants[i].health}");
        }
    }

    private void ChoosePlantAndPerform(Action<Plant> action)
    {
        if (plants.Count == 0)
        {
            Console.WriteLine("You have no plants to choose from.");
            return;
        }

        Console.WriteLine("\nChoose a plant:");
        for (int i = 0; i < plants.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {plants[i].name}");
        }

        if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= plants.Count)
        {
            action(plants[choice - 1]);
        }
        else
        {
            Console.WriteLine("Invalid choice.");
        }
    }

    private void applyWeatherEffects()
    {
        if (plants.Count == 0)
        {
            Console.WriteLine("You have no plants to apply weather effects to.");
            return;
        }

        weather.generateWeather();
        Console.WriteLine("\nCurrent weather is: {0}", Weather.currentWeather);

        Console.WriteLine("\nApplying weather effects...");
        foreach (Plant p in plants)
        {
            double initialHealth = p.health;
            weather.getWeatherEffects(p);
            double effect = p.health - initialHealth; // Calculate the effect on health
            Console.WriteLine($"{p.name} - Weather effect: {Weather.currentWeather} ({(effect >= 0 ? "+" : "")}{effect}) - Current Health: {p.health}");
        }

        Console.WriteLine("\nWeather effects applied to all plants.");

    }//Apply weather effects method()


    }// Game class 

}//namespace GardenProject
