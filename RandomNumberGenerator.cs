using System;

public class RandomNumberGenerator : Singleton<RandomNumberGenerator>
{
    Random generator;
    Random Generator { 
        get { 
            if(generator == null)
                generator = new Random(Guid.NewGuid().GetHashCode());

            return generator;
        }
    }

    public int Next()
    {
        return Generator.Next();
    }

    public int Next(int max)
    {
        return Generator.Next(max);
    }
}
