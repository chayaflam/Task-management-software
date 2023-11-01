﻿using Dal;
using DalApi;

namespace DalTest
{
    internal class Program
    {

        private static IDependency? s_dalDependency = new DependencyImplementation(); //stage 1
        private static IEngineer? s_dalEngineer = new EngineerImplementation(); //stage 1
        private static ITask? s_dalTask = new TaskImplementation(); //stage 1
       
       
        static void Main(string[] args)
        {
            Console.WriteLine("ghgg");
        }
    }
   
  
}