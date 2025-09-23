using System.Reflection;

public class Fizzbuzz
{

    public void CountTo(int lastNumber)
    {
        for (int aktualniCislo = 1; aktualniCislo < lastNumber; aktualniCislo++)
        {
            if (aktualniCislo % 3 == 0 && aktualniCislo % 5 == 0)
            {
                System.Console.WriteLine("FizzBuzz");
            }

            else if (aktualniCislo % 3 == 0)
            {
                System.Console.WriteLine("Fizz");
            }

            else if (aktualniCislo % 5 == 0)
            {
                System.Console.WriteLine("Buzz");
            }
            System.Console.WriteLine(aktualniCislo);
        }
    }
}
