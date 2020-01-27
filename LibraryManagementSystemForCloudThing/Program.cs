using LibraryRepository;
using System;

namespace LibraryManagementSystemForCloudThing
{
    public class Program
    {
        public static void Main(string[] args)
        {
            LibraryManager manager = new LibraryManager();

            manager.AddBook();
        }
    }
}