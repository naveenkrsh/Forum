using Forum.Infrastructure.Contract;
using Forum.Infrastructure.Mongo;
using Forum.Services.Contract;
using Forum.Services;
using Forum.Core.Models;
using System;
using System.Threading.Tasks;

namespace Forum.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            
            IUnitOfWork _unitOfWork = new MongoUnitOfWork();
            var _tagService = new TagService(_unitOfWork);

            var tag = new Tag("Java");
            _tagService.SaveAsync(tag).Wait();
            Console.WriteLine(tag.ToString());
            Console.WriteLine("Hello Naveen World!");
            //Console.ReadKey();
        }
    }
}
