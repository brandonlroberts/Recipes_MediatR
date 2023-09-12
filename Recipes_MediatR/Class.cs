namespace Recipes_MediatR
{

    public class Rootobject
    {
        public Class1[] Property1 { get; set; }
    }

    public class Class1
    {
        public int id { get; set; }
        public string title { get; set; }
        public DateTime createdDate { get; set; }
        public string createdby { get; set; }
        public Ingredient[] ingredients { get; set; }
        public Instruction[] instructions { get; set; }
    }

    public class Ingredient
    {
        public int id { get; set; }
        public string name { get; set; }
        public string measurements { get; set; }
    }

    public class Instruction
    {
        public int id { get; set; }
        public int sequence { get; set; }
        public string summary { get; set; }
    }

}
