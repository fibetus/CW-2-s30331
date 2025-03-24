namespace ContainerManager;

public class ContainerShip
{
    private HashSet<Container> containers = new HashSet<Container>();
    
    public int MaxContainers { get; }
    public double MaxWeight { get; }
    public double Speed { get; }

    public ContainerShip(int maxContainers, double maxWeight, double speed)
    {
        MaxContainers = maxContainers;
        MaxWeight = maxWeight * 1000;
        Speed = speed;
    }

    public double GetLoadedContainersWeight()
    {
        return containers.Sum(c => c.CurrentWeight);
    }

    public void LoadContainerOntoShip(Container container)
    {
        if (containers.Count >= MaxContainers)
        {
            throw new Exception("Containers count cannot be more than max containers");
        }

        if (GetLoadedContainersWeight() + container.CurrentWeight > MaxWeight)
        {
            throw new Exception("Containers weight cannot be more than max ship load weight");
        }
        
        containers.Add(container);
    }

    public void LoadContainerOntoShip(List<Container> containers)
    {
        this.containers.UnionWith(containers);
    }

    public void UnloadContainerFromShip(String containerSerialNumber)
    {
        try
        {
            Container container = containers.FirstOrDefault(c => c.SerialNumber == containerSerialNumber);
            if (container != null)
            {
                containers.Remove(container);
            }
            else
            {
                throw new Exception($"Container {containerSerialNumber} not found");
            }
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine("Ship has not been loaded with any containers");
        }
    }

    public void ReplaceContainer(string oldContainerSerialNumber, Container newContainer)
    {
        UnloadContainerFromShip(oldContainerSerialNumber);
        LoadContainerOntoShip(newContainer);
    }

    public void MoveContainerOntoAnotherShip(ContainerShip anotherShip, string containerSerialNumber)
    {
        try
        {
            var tempContainer = containers.FirstOrDefault(c => c.SerialNumber == containerSerialNumber);
            UnloadContainerFromShip(containerSerialNumber);
            anotherShip.LoadContainerOntoShip(tempContainer);
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine("Container not found");
        }
    }

    public void GetShipInfo()
    {
        Console.WriteLine($"Speed: {Speed}");
        foreach (Container container in containers)
        {
            Console.WriteLine($"Container: {container}");
        }
    }
}