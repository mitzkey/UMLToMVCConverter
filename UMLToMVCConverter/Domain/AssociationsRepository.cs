﻿namespace UMLToMVCConverter.Domain
{
    using System.Collections.Generic;
    using System.Linq;
    using UMLToMVCConverter.Common;

    public class AssociationsRepository : IAssociationsRepository
   {
       private readonly ILogger logger;

       public List<Association> Associations { get; }

        public AssociationsRepository(ILogger logger)
        {
            this.logger = logger;
            this.Associations = new List<Association>();
        }

        public IEnumerable<Association> GetAllAssociations()
        {
            return this.Associations;
        }

       public void Add(Association association)
       {
           if (this.Associations.Any(x => x.XmiID == association.XmiID))
           {
               this.logger.LogInfo($"Skipped adding association with ID:{association.XmiID}");
               return;
           }

           this.Associations.Add(association);
        }
   }
}