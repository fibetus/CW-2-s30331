using ContainerManager;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("1. CREATING SHIPS");
        var ship1 = new ContainerShip(maxContainers: 10, maxWeight: 100, speed: 30);
        Console.WriteLine("Created Ship 1 with max 10 containers, 100000 kg capacity, speed 30 knots");
            
        var ship2 = new ContainerShip(maxContainers: 5, maxWeight: 50, speed: 35);
        Console.WriteLine("Created Ship 2 with max 5 containers, 50000 kg capacity, speed 35 knots");
        Console.WriteLine();
        
        Console.WriteLine("2. CREATING CONTAINERS OF DIFFERENT TYPES");
            
        var containerC = new ContainerC(
            productType: "Meat",
            temperature: -15,
            height: 2.5,
            conatinerWeight: 2000,
            depth: 6.0,
            maxLoadWeight: 20000
        );
        Console.WriteLine($"Created refrigerated container: {containerC.SerialNumber}");
        
            
        var containerL = new ContainerL(
            isHazardous: true,
            height: 2.5,
            conatinerWeight: 3000,
            depth: 6.0,
            maxLoadWeight: 15000
        );
        Console.WriteLine($"Created liquid container: {containerL.SerialNumber}");
            
        var containerG = new ContainerG(
            pressure: 10.5,
            height: 2.5,
            conatinerWeight: 2500,
            depth: 6.0,
            maxLoadWeight: 18000
        );
        Console.WriteLine($"Created gas container: {containerG.SerialNumber}");
        Console.WriteLine();
        
        Console.WriteLine("2.1 CREATING CONTAINERC WITH TOO LOW TEMPERATURE");

        try
        {
            var containerCWithException = new ContainerC(
                productType: "Meat",
                temperature: -19,
                height: 2.5,
                conatinerWeight: 2000,
                depth: 6.0,
                maxLoadWeight: 20000
            );
            Console.WriteLine($"Created refrigerated container: {containerCWithException.SerialNumber}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception caught: {ex.Message}");
        }
        Console.WriteLine();
        
        Console.WriteLine("3. LOADING CARGO INTO CONTAINERS");
        try
        {
            
            containerC.LoadContainer(10000);
            Console.WriteLine($"Loaded 10000 kg into {containerC.SerialNumber}, current weight: {containerC.CurrentWeight} kg");
                
            containerL.LoadContainer(5000);
            Console.WriteLine($"Loaded 5000 kg into {containerL.SerialNumber}, current weight: {containerL.CurrentWeight} kg");
                
            containerG.LoadContainer(8000);
            Console.WriteLine($"Loaded 8000 kg into {containerG.SerialNumber}, current weight: {containerG.CurrentWeight} kg");
                
            // Try to overfill a container
            // Console.WriteLine("Attempting to overfill container C...");
            // containerC.LoadContainer(15000); // This should throw OverfillException
            
            Console.WriteLine("Attempting to overfill container G...");
            containerG.LoadContainer(15000); // This should throw OverfillException
        }
        catch (OverfillException ex)
        {
            Console.WriteLine($"Exception caught: {ex.Message}");
        }
        Console.WriteLine();
        
        Console.WriteLine("4. LOADING A CONTAINER ONTO A SHIP");
        try
        {
            ship1.LoadContainerOntoShip(containerC);
            Console.WriteLine($"Loaded {containerC.SerialNumber} onto Ship 1");
            
            //trying to load same container multiple times
            ship1.LoadContainerOntoShip(containerC);
            Console.WriteLine($"Loaded {containerC.SerialNumber} onto Ship 1");
            ship1.LoadContainerOntoShip(containerC);
            Console.WriteLine($"Loaded {containerC.SerialNumber} onto Ship 1");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading container: {ex.Message}");
        }
        Console.WriteLine();
        ship1.GetShipInfo();
        Console.WriteLine();
        
        
        Console.WriteLine("5. LOADING A LIST OF CONTAINERS ONTO A SHIP");
        try
        {
            var containerList = new List<Container> { containerL, containerG };
            ship1.LoadContainerOntoShip(containerList);
            Console.WriteLine($"Loaded {containerList.Count} containers onto Ship 1");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading containers: {ex.Message}");
        }
        Console.WriteLine();
        
        Console.WriteLine("6. SHIP 1 INFORMATION");
        ship1.GetShipInfo();
        Console.WriteLine();
        
        Console.WriteLine("7. REMOVING A CONTAINER FROM A SHIP");
        try
        {
            ship1.UnloadContainerFromShip(containerL.SerialNumber);
            Console.WriteLine($"Removed {containerL.SerialNumber} from Ship 1");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error unloading container: {ex.Message}");
        }
        Console.WriteLine();
        ship1.GetShipInfo();
        Console.WriteLine();
        
        Console.WriteLine("8. UNLOADING A CONTAINER'S CARGO");
        try
        {
            
            containerC.UnloadContainer();
            Console.WriteLine($"Unloaded cargo from {containerC.SerialNumber}, current weight: {containerC.CurrentWeight} kg");
                
            // Special unloading for gas container which keeps 5% of cargo
            containerG.UnloadContainer();
            Console.WriteLine($"Unloaded cargo from {containerG.SerialNumber}, current weight: {containerG.CurrentWeight} kg (5% remains)");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error unloading container cargo: {ex.Message}");
        }
        Console.WriteLine();
        
        Console.WriteLine("9. REPLACING A CONTAINER ON THE SHIP");
        try
        {
            containerC.LoadContainer(5000);
            Console.WriteLine($"Reloaded {containerC.SerialNumber} with 5000 kg");
                
            // Create a new container to replace containerC
            var newContainer = new ContainerC(
                productType: "Fresh Vegetables",
                temperature: 4.0,
                height: 2.5,
                conatinerWeight: 1800,
                depth: 6.0,
                maxLoadWeight: 22000
            );
            newContainer.LoadContainer(7000);
            Console.WriteLine($"Created new container {newContainer.SerialNumber} with 7000 kg load");
                
            // Replace containerC with the new container
            ship1.ReplaceContainer(containerC.SerialNumber, newContainer);
            Console.WriteLine($"Replaced {containerC.SerialNumber} with {newContainer.SerialNumber} on Ship 1");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error replacing container: {ex.Message}");
        }
        Console.WriteLine();
        ship1.GetShipInfo();
        Console.WriteLine();
        
        // 10. Transfer a container between ships
        Console.WriteLine("10. TRANSFERRING A CONTAINER BETWEEN SHIPS");
        try
        {
            ship1.MoveContainerOntoAnotherShip(ship2, containerG.SerialNumber);
            Console.WriteLine($"Transferred {containerG.SerialNumber} from Ship 1 to Ship 2");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error transferring container: {ex.Message}");
        }
        Console.WriteLine();
        ship1.GetShipInfo();
        Console.WriteLine();
        ship2.GetShipInfo();
        Console.WriteLine();
    }
}