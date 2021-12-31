namespace IbanNet.Benchmark
{
    public class ValidatorCase
    {
        public ValidatorCase(string name, Action<string> validate)
        {
            Name = name;
            Validate = validate;
        }

        public string Name { get; }
        public Action<string> Validate { get; }

        public override string ToString()
        {
            return Name;
        }
    }
}
