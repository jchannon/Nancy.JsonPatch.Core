namespace Nancy.JsonPatch.Core.Demo
{
    using System.Collections.Generic;

    public class Contact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public List<string> Links { get; set; }
    }
}
