using System;

namespace Assignment1ArraysMethods
{
    class Program
    {
        // ----------------------------
        // Exercise 1: Array Statistics
        // ----------------------------
        static void GetStatistics(int[] numbers, out int min, out int max, out double average)
        {
            if (numbers == null || numbers.Length == 0)
                throw new ArgumentException("numbers array must not be null or empty");

            min = numbers[0];
            max = numbers[0];
            long sum = 0; // long to avoid overflow with large sums
            for (int i = 0; i < numbers.Length; i++)
            {
                int n = numbers[i];
                if (n < min) min = n;
                if (n > max) max = n;
                sum += n;
            }
            average = sum / (double)numbers.Length;
        }

        // Overload for double[]
        static void GetStatistics(double[] numbers, out double min, out double max, out double average)
        {
            if (numbers == null || numbers.Length == 0)
                throw new ArgumentException("numbers array must not be null or empty");

            min = numbers[0];
            max = numbers[0];
            double sum = 0.0;
            for (int i = 0; i < numbers.Length; i++)
            {
                double n = numbers[i];
                if (n < min) min = n;
                if (n > max) max = n;
                sum += n;
            }
            average = sum / numbers.Length;
        }

        // ResizeArray: creates a new array of newSize, copies elements, and updates the reference
        static void ResizeArray(ref int[] array, int newSize)
        {
            if (newSize < 0) throw new ArgumentOutOfRangeException(nameof(newSize));
            int[] newArray = new int[newSize];
            if (array != null)
            {
                int countToCopy = Math.Min(array.Length, newSize);
                Array.Copy(array, newArray, countToCopy);
            }
            array = newArray; // update original reference
        }

        // Challenge: median with params
        static double FindMedian(params int[] numbers)
        {
            if (numbers == null || numbers.Length == 0)
                throw new ArgumentException("At least one number is required");

            int[] sorted = new int[numbers.Length];
            Array.Copy(numbers, sorted, numbers.Length);
            Array.Sort(sorted);

            int len = sorted.Length;
            if (len % 2 == 1)
            {
                return sorted[len / 2];
            }
            else
            {
                int midRight = len / 2;
                int midLeft = midRight - 1;
                return (sorted[midLeft] + sorted[midRight]) / 2.0;
            }
        }

        // ----------------------------
        // Exercise 2: Password Validation
        // ----------------------------
        static bool IsLongEnough(string password, int minLength)
        {
            return password != null && password.Length >= minLength;
        }

        static bool ContainsUppercase(string password)
        {
            if (string.IsNullOrEmpty(password)) return false;
            for (int i = 0; i < password.Length; i++)
            {
                if (char.IsUpper(password[i])) return true;
            }
            return false;
        }

        static bool ContainsDigit(string password)
        {
            if (string.IsNullOrEmpty(password)) return false;
            for (int i = 0; i < password.Length; i++)
            {
                if (char.IsDigit(password[i])) return true;
            }
            return false;
        }

        static bool ContainsSpecialCharacter(string password)
        {
            if (string.IsNullOrEmpty(password)) return false;
            // Only arrays + loops; no LINQ or List<T>
            char[] specials = new char[] { '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', ',', '.', '?', '_' };
            for (int i = 0; i < password.Length; i++)
            {
                char c = password[i];
                for (int j = 0; j < specials.Length; j++)
                {
                    if (c == specials[j]) return true;
                }
            }
            return false;
        }

        static int CountValidationErrors(string password)
        {
            int errors = 0;
            if (!IsLongEnough(password, 8)) errors++;
            if (!ContainsUppercase(password)) errors++;
            if (!ContainsDigit(password)) errors++;
            if (!ContainsSpecialCharacter(password)) errors++;
            return errors;
        }

        static string[] GetErrorMessages(string password)
        {
            int count = CountValidationErrors(password);
            string[] messages = new string[count];
            int index = 0;

            if (!IsLongEnough(password, 8))
                messages[index++] = "Password must be at least 8 characters long";
            if (!ContainsUppercase(password))
                messages[index++] = "Password must contain at least one uppercase letter";
            if (!ContainsDigit(password))
                messages[index++] = "Password must contain at least one digit";
            if (!ContainsSpecialCharacter(password))
                messages[index++] = "Password must contain at least one special character";

            return messages;
        }

        static bool ValidatePassword(string password)
        {
            return CountValidationErrors(password) == 0;
        }

        static void PrintPasswordValidation(string password)
        {
            Console.WriteLine($"Testing password: {password}");
            bool isValid = ValidatePassword(password);
            if (isValid)
            {
                Console.WriteLine("✅ Password is VALID\n");
            }
            else
            {
                Console.WriteLine("❌ Password is INVALID");
                string[] errors = GetErrorMessages(password);
                for (int i = 0; i < errors.Length; i++)
                {
                    Console.WriteLine(" - " + errors[i]);
                }
                Console.WriteLine();
            }
        }

        // ----------------------------
        // Exercise 3: Random Numbers
        // ----------------------------
        static void RandomNumbersAnalysis()
        {
            int[] values = new int[25];
            Random rnd = new Random();
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = rnd.Next(1, 51); // 1..50 inclusive
            }

            int divisibleBy3 = 0;
            int divisibleBy5 = 0;
            int[] both = new int[25];
            int bothCount = 0;

            for (int i = 0; i < values.Length; i++)
            {
                int n = values[i];
                if (n % 3 == 0) divisibleBy3++;
                if (n % 5 == 0) divisibleBy5++;
                if (n % 3 == 0 && n % 5 == 0)
                {
                    both[bothCount++] = n;
                }
            }

            Console.WriteLine("Generated numbers (25):");
            for (int i = 0; i < values.Length; i++)
            {
                Console.Write(values[i] + (i < values.Length - 1 ? ", " : "\n"));
            }
            Console.WriteLine($"Count divisible by 3: {divisibleBy3}");
            Console.WriteLine($"Count divisible by 5: {divisibleBy5}");

            if (bothCount == 0)
            {
                Console.WriteLine("Numbers divisible by both 3 and 5: None");
            }
            else
            {
                Console.Write("Numbers divisible by both 3 and 5: ");
                for (int i = 0; i < bothCount; i++)
                {
                    Console.Write(both[i] + (i < bothCount - 1 ? ", " : "\n"));
                }
            }
            Console.WriteLine();
        }

        // ----------------------------
        // Demo / Main
        // ----------------------------
        static void Exercise1Demo()
        {
            Console.WriteLine("==== Exercise 1: Array Statistics Manager ====");

            int[] nums = new int[] { 10, -3, 5, 22, 7 };
            GetStatistics(nums, out int minI, out int maxI, out double avgI);
            Console.WriteLine($"Int stats -> min: {minI}, max: {maxI}, average: {avgI:0.00}");

            double[] numsD = new double[] { 1.5, -2.0, 7.25, 4.5 };
            GetStatistics(numsD, out double minD, out double maxD, out double avgD);
            Console.WriteLine($"Double stats -> min: {minD:0.00}, max: {maxD:0.00}, average: {avgD:0.00}");

            int[] resizeMe = new int[] { 1, 2, 3, 4 };
            ResizeArray(ref resizeMe, 6);
            Console.WriteLine("Resized array to 6 -> [" + string.Join(", ", resizeMe) + "]");

            double medianOdd = FindMedian(5, 1, 3, 4, 2);
            double medianEven = FindMedian(4, 1, 2, 3, 10, 20);
            Console.WriteLine($"Median(5,1,3,4,2) = {medianOdd}");
            Console.WriteLine($"Median(4,1,2,3,10,20) = {medianEven}");
            Console.WriteLine();
        }

        static void Exercise2Demo()
        {
            Console.WriteLine("==== Exercise 2: Password Validation System ====");
            string[] tests = new string[] {
                "weak",
                "weakpassword",
                "WeakPassword",
                "WeakPassword123",
                "StrongPassword123!"
            };
            for (int i = 0; i < tests.Length; i++)
            {
                PrintPasswordValidation(tests[i]);
            }

            // Challenge: interactive testing
            while (true)
            {
                Console.Write("Enter a password (or press ENTER to quit): ");
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                    break;
                PrintPasswordValidation(input);
            }
            Console.WriteLine();
        }

        static void Exercise3Demo()
        {
            Console.WriteLine("==== Exercise 3: Random Numbers ====");
            RandomNumbersAnalysis();
        }

        static void Main(string[] args)
        {
            Exercise1Demo();
            Exercise3Demo();
            Exercise2Demo();

            Console.WriteLine("\nAll exercises completed. Press any key to exit...");
            Console.ReadKey();
        }
    }
}