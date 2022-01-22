namespace Banks.DTO
{
    public class FullName
    {
        private string _name;
        private string _surname;

        public FullName(string name, string surname)
        {
            _name = name;
            _surname = surname;
        }

        public FullName DeepCopy() => new FullName(_name, _surname);

        public string GetName() => _name;
        public string GetSurname() => _surname;
    }
}