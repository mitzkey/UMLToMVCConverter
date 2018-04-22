﻿namespace UMLToMVCConverter
{
    using System;
    using UMLToMVCConverter.Interfaces;

    public class Application
    {
        private readonly IDataModelGenerator dataModelGenerator;

        public Application(IDataModelGenerator dataModelGenerator)
        {
            this.dataModelGenerator = dataModelGenerator;
        }

        public void Run()
        {
            var result = this.dataModelGenerator.GenerateMvcFiles();
            Console.WriteLine(result);
            Console.ReadLine();
        }
    }
}
