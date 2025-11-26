using System.Text;

namespace OOP_CustomString
{
    public class CustomString
    {

        private string value;

        public string Value
        {
            get  { return this.value;}
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value), "Value cannot be null.");
                this.value = value;
            }
        }

        public CustomString(string initialValue)
        {
            if (initialValue == null)
                throw new ArgumentNullException(nameof(initialValue), "Initial value cannot be null.");
            this.value = initialValue;
        }

        private int CustomStringLength()
        {
            return value?.Length ?? 0;
        }

        public static CustomString operator +(CustomString a, CustomString b)
        {
            if (a is null) throw new ArgumentNullException(nameof(a));
            if (b is null) throw new ArgumentNullException(nameof(b));
            return new CustomString(a.value + b.value);
        }

        public static CustomString operator *(CustomString s, int n)
        {
            if (s is null) throw new ArgumentNullException(nameof(s));
            if (n <= 0) return new CustomString(string.Empty);

            string result = ""; 
            for (int i = 0; i < n; i++)
            {
                result += s.value;
            }
            return new CustomString(result);
        }

        public static CustomString operator *(int n, CustomString s)
        {
            return s * n;
        }

        public override string ToString()
        {
            return $"CustomString: {value}, {CustomStringLength()}";
        }

        public override bool Equals(object obj)
        {
            if (obj is CustomString other)
            {
                return this.CustomStringLength() == other.CustomStringLength();
            }
            return false;
        }

        // public override int GetHashCode()
        // {
        //     return CustomStringLength().GetHashCode();
        // }
    }


    class Program
    {
        static void Main()
        {
            // создаём два экземпляра
            var cs1 = new CustomString("hello");
            var cs2 = new CustomString("world");

            Console.WriteLine(cs1);                 // CustomString: hello, 5
            Console.WriteLine(cs2);                 // CustomString: world, 5

            // проверяем Equals (сравниваются по длине — обе длины 5)
            Console.WriteLine("cs1.Equals(cs2): " + cs1.Equals(cs2)); // True

            // конкатенация
            var concat = cs1 + new CustomString(" ");
            concat = concat + cs2;
            Console.WriteLine("concat: " + concat); // CustomString: hello world, 11

            // повторение строки
            var repeat = cs1 * 3;
            Console.WriteLine("repeat: " + repeat); // CustomString: hellohellohello, 15

            // другое сочетание: int * CustomString
            var repeat2 = 2 * cs2;
            Console.WriteLine("repeat2: " + repeat2); // CustomString: worldworld, 10

            // установка нового значения через свойство (валидная операция)
            cs1.Value = "short";
            Console.WriteLine("cs1 now: " + cs1); // CustomString: short, 5

            // попытка присвоить null — покажем обработку исключения
            try
            {
                cs1.Value = null; // вызовет ArgumentNullException
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine("Caught expected exception when setting null: " + ex.Message);
            }

            // сравнение после изменений
            var cs3 = new CustomString("12345");
            Console.WriteLine("cs1.Equals(cs3): " + cs1.Equals(cs3)); // True (оба длины 5)

            // // Демонстрация GetHashCode соответствия Equals
            // Console.WriteLine($"hash(cs1)={cs1.GetHashCode()}, hash(cs3)={cs3.GetHashCode()}");
        }
    }
}
