namespace EgzaminoProjektas.Classes
{
    public class Person
    {
        protected string Name { get; set; }
        protected string Surname { get; set; }
        public Person() { }
        public Person(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }
        public virtual string GetFullName()
        {
            return $"{Name} {Surname}";
        }
    }
}
